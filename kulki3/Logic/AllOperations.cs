using Data;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata;
using System.Threading;


namespace Logic
{
    public class AllOperations : IAllOperations
    {
        private IKulkodom _Kulkodom;
        public event NotifyDelegateAllOperations.NotifyEllipseController? OnChange;
        private double _width;
        private double _length;
        Barrier Wall;
       

        public AllOperations(int x, int y)
        {
            this._Kulkodom = new Kulkodom(x, y);
            this._length = x;
            this._width = y;
          
            Wall = new Barrier(0, (b) =>
            {     
                OnChange?.Invoke();
                Thread.Sleep(10);
            });
        }


        public void NewEllipse() {
            {
                
                lock (_Kulkodom.Lock)
                {

                    int id = _Kulkodom.getRepository().Count();
                    int width = _Kulkodom.Width;
                    int lenght = _Kulkodom.Length;

                    var rand = new Random();

                    int diameter = rand.Next(50, 75);

                    double x = rand.Next(0, width - diameter);
                    double y = rand.Next(0, lenght - diameter);
                    double sX, sY;

                    do
                    {
                        sX = rand.NextDouble() * 2 - 1;
                        sY = rand.NextDouble() * 2 - 1;
                    } while (sX == 0 && sY == 0);

                    Ellipse Ellipse = new Ellipse(
                        id,
                        diameter / 10,
                        diameter,
                        x,
                        y,
                        sX,
                        sY
                        );

                    bool flag = false;
                    do
                    {
                        flag = false;
                        foreach (Ellipse Ellipse2 in _Kulkodom.getRepository())
                        {
                            if (CheckCollision(Ellipse.X, Ellipse.Y, diameter, Ellipse2))
                            {
                                flag = true;
                                Ellipse.X = rand.Next(0, width - diameter);
                                Ellipse.Y = rand.Next(0, lenght - diameter);
                                break;

                            }

                        }


                    } while (flag);

                    Ellipse.OnChange += ResolveCollision;
                    Ellipse.MoveAsync(Wall);
                    _Kulkodom.addEllipse(Ellipse);
                }
                
            }
        }
        public void DeleteEllipse(int id)
        {
            lock (_Kulkodom.Lock)
            {
            IEllipse Ellipse = _Kulkodom.getRepository().FirstOrDefault(Ellipse => Ellipse.Id == id);
                if (Ellipse != null)
                {
                    Ellipse.IsMoving = false;
                    Ellipse.OnChange -= ResolveCollision;
                    _Kulkodom.removeEllipse(Ellipse);

                }

            }
        }

        private bool CheckCollision(double x, double y, double d, IEllipse Ellipse2)
        {
            double dx = x - Ellipse2.X;
            double dy = y - Ellipse2.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            
            return distance <= ((d / 2) + (Ellipse2.Diameter / 2));
        }

        public void ResolveCollision(IEllipse Ellipse){
            {
                lock (Ellipse.Lock)
                {
                    double n_X = Ellipse.X + Ellipse.velocityX;
                    double n_Y = Ellipse.Y + Ellipse.velocityY;
                    
                    foreach (IEllipse Ellipse2 in _Kulkodom.getRepository())
                    {
                        if (Ellipse.Id == Ellipse2.Id) continue;
                        if (CheckCollision(n_X, n_Y, Ellipse.Diameter, Ellipse2))
                           if(Monitor.TryEnter(Ellipse2.Lock)) 
                           {
                                try
                                {
                                    double dx = n_X - Ellipse2.X;
                                    double dy = n_Y - Ellipse2.Y;
                                    double distance = Math.Sqrt((dx * dx) + (dy * dy));
                                    double angle = Math.Atan2(dy, dx);
                                    if (distance == 0) continue;
                                    dx /= distance;
                                    dy /= distance;
                                    double tx = -dy;
                                    double ty = dx;

                                    double v1n = dx * Ellipse.velocityX + dy * Ellipse.velocityY;
                                    double v1t = tx * Ellipse.velocityX + ty * Ellipse.velocityY;
                                    double v2n = dx * Ellipse2.velocityX + dy * Ellipse2.velocityY;
                                    double v2t = tx * Ellipse2.velocityX + ty * Ellipse2.velocityY;

                                    double v1nAfter = (v1n * (Ellipse.Mass - Ellipse2.Mass) + 2 * Ellipse2.Mass * v2n) / (Ellipse.Mass + Ellipse2.Mass);
                                    double v2nAfter = (v2n * (Ellipse2.Mass - Ellipse.Mass) + 2 * Ellipse.Mass * v1n) / (Ellipse.Mass + Ellipse2.Mass);

                                    Ellipse.velocityX = v1nAfter * dx + v1t * tx;
                                    Ellipse.velocityY = v1nAfter * dy + v1t * ty;
                                    Ellipse2.velocityX = v2nAfter * dx + v2t * tx;
                                    Ellipse2.velocityY = v2nAfter * dy + v2t * ty;

                                    double diff = ((Ellipse.Diameter / 2 + Ellipse2.Diameter / 2) - distance) / 2;
                                    n_X += diff * dx;
                                    n_Y += diff * dy;
                                    Ellipse2.X -= diff * dx;
                                    Ellipse2.Y -= diff * dy;
                                }
                                finally
                                {   Monitor.Exit(Ellipse2.Lock);
                                }   
                            }
                            else
                            {
                                return;
                            }       
                    }

                    if (n_X < 0)
                    {   n_X = 0;
                        Ellipse.velocityX *= -1;
                    }
                    if (n_X > _width - Ellipse.Diameter)
                    {   n_X = _width - Ellipse.Diameter;
                        Ellipse.velocityX *= -1;
                    }
                    if (n_Y < 0)
                    {   n_Y = 0;
                        Ellipse.velocityY *= -1;
                    }
                    if (n_Y > _length - Ellipse.Diameter)
                    {   n_Y = _length - Ellipse.Diameter;
                        Ellipse.velocityY *= -1;
                    }
                    Ellipse.X = n_X;
                    Ellipse.Y = n_Y;

                }
            }     
        }
   
    public void ClearKulkodom()
        {
            lock(_Kulkodom.Lock)
            {
                foreach (IEllipse Ellipse in _Kulkodom.getRepository())
                {
                    Ellipse.IsMoving = false;
                    Ellipse.OnChange -= ResolveCollision;
                }
                _Kulkodom.getRepository().Clear();
            }
            
        }

        public int GetSize() => _Kulkodom.getSize();

        public int GetLength() => _Kulkodom.Length;

        public int GetWidth() => _Kulkodom.Width;
        public ConcurrentBag<IEllipse> GetEllipseRepo() => _Kulkodom.getRepository();
        
    }
}
