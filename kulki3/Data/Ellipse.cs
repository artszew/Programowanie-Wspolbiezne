using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Data
{
    public class Ellipse : IEllipse
    {
        private int _id;
        private double _Mass;
        private double _diameter;
        private double _x;
        private double _y;
        private double _velocityX;
        private double _velocityY;
        private readonly object _lock = new object();
        private string _color;
        public event NotifyDelegateEllipse.NotifyEllipse? OnChange;
        private bool _isMoving = true;
        private static string RandomColor(int id)
        {
            switch (id % 5)
            {   case 0:
                    return "#ffb3e6";
                case 1:
                    return "#ffc299"; 
                case 2:
                    return "#b3ffd9"; 
                case 3:
                    return "#ffff99";
                case 4:
                    return "#ffffff"; 
                default:
                    return "#000000"; 
            }
        }

        public Ellipse(int id, double Mass, int diameter, double x, double y, double velocityX, double velocityY)
        {
            this._id = id;
            this._diameter = diameter;
            this._Mass = Mass;
            this._x = x;
            this._y = y;
            this._velocityX = velocityX;
            this._velocityY = velocityY;
            _color = RandomColor(id);
        }
        public int Id
        {   get => _id;
            set => _id = value;
        }
        public double Mass
        {
            get => _Mass;
            set
            {   if (value < 0) throw new InvalidDataException();
                this._Mass = value;
            }
        }
        public string Color
        {   get => _color;
            set
            {   if (_color != value)
                {   _color = value;
                    OnChange?.Invoke(this);
                }
            }
        }

        public double Diameter
        {   get => this._diameter;
            set 
            {   if (value < 0) throw new InvalidDataException();
                this._diameter = value;
            } 

        }
        public double X
        {   get => (double)_x;
            set => _x = value;

        }
        public double Y
        {   get => (double)_y;
            set => _y = value;
        }
        public double velocityX
        {   get => _velocityX;
            set => _velocityX = value;
        }
        public double velocityY
        {   get => _velocityY;
            set => _velocityY = value;
        }
        public bool IsMoving
        {   get=> _isMoving;
            set => _isMoving = value;
        }
        public void MoveAsync(Barrier Wall)
        {   Wall.AddParticipant();
            Task.Run(() => Move(Wall));
        }
        private void Move(Barrier Wall)
        {   while(IsMoving)
            {   OnChange?.Invoke(this);
                Wall.SignalAndWait();
            }
           Wall.RemoveParticipant();
        }
        public object Lock => _lock;
    }
}
