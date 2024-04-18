using kulki3.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace kulki3.MVVM.ViewModel 
{
    public class KulkomaniaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EllipseModel> _ellipses;
        public ObservableCollection<EllipseModel> Ellipses
        {
            get { return _ellipses; }
            set { _ellipses = value; OnPropertyChanged(nameof(Ellipses)); }
        }

        private Thread _movementThread;
        private bool _isMoving;

        public KulkomaniaViewModel()
        {
            // Initialize the collection and add ellipses
            Ellipses = new ObservableCollection<EllipseModel>();
            InitializeEllipses();
            _isMoving = true;

            // Start the movement thread
            _movementThread = new Thread(MoveEllipses);
            _movementThread.Start();
        }

        private void InitializeEllipses()
        {
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                EllipseModel ellipse = new EllipseModel
                {
                    X = random.Next(300, 500), // Bounds for X coordinate
                    Y = random.Next(50, 200), // Bounds for Y coordinate
                    Color = Brushes.Blue // Color
                };
                Ellipses.Add(ellipse);
            }
        }

        public void MoveEllipses()
        {
            Random random = new Random();
            double _x;
            double _y;
            while (_isMoving)
            {
                // Update the positions of all ellipses
                foreach (var ellipse in Ellipses)
                {
                    // Adjust position randomly for smooth movement
                    _x = random.Next(-20, 20);
                    _y = random.Next(-20, 20);

                    // Ensure ellipses stay within the bounds
                    if (((ellipse.X + _x) < 50) || ((ellipse.X + _x) > 650)) { _x = 0; }
                    if (((ellipse.Y + _y) < 0) || ((ellipse.Y + _y) > 250)) { _y = 0; }

                    ellipse.X += _x;
                    ellipse.Y += _y;
                }

                Thread.Sleep(30); // Adjust sleep time as needed for desired speed
            }
        }

        public void MoveEllipsesForTest(int iterationNumber) //same function as normal move method but ends after specified amount of iterations for testing purposes
        {
            Random random = new Random();
            double _x;
            double _y;
            int counter = 0;
            while (counter < iterationNumber)
            {
                // Update the positions of all ellipses
                foreach (var ellipse in Ellipses)
                {
                    // Adjust position randomly for smooth movement
                    _x = random.Next(-2, 2);
                    _y = random.Next(-2, 2);

                    // Ensure ellipses stay within the bounds
                    if (((ellipse.X + _x) < 50) || ((ellipse.X + _x) > 550)) { _x = 0; }
                    if (((ellipse.Y + _y) < 0) || ((ellipse.Y + _y) > 400)) { _y = 0; }

                    ellipse.X += _x;
                    ellipse.Y += _y;
                }
                counter++;
                Thread.Sleep(20); // Adjust sleep time as needed for desired speed
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
