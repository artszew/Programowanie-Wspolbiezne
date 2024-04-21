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

            foreach (var ellipse in Ellipses)
            {
                Thread ellipseThread = new Thread(() =>
                {
                    MoveEllipse(ellipse);
                });
                ellipseThread.Start();
            }
        }

        private void InitializeEllipses()
        {
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                EllipseModel ellipse = new EllipseModel
                {
                    X = random.Next(12, 665), // Bounds for X coordinate
                    Y = random.Next(12, 290), // Bounds for Y coordinate
                    Color = Brushes.Blue // Color
                };
                Ellipses.Add(ellipse);
            }
        }
       

        private void MoveEllipse(EllipseModel ellipse)
        {
            Random random = new Random();
            double _x;
            double _y;

            while (true) // Each ellipse thread runs indefinitely
            {
                // Adjust position randomly for smooth movement
                _x = random.Next(-10, 11);
                _y = random.Next(-10, 11);

                // Ensure ellipses stay within the bounds
                if ((ellipse.X + _x) < 12 || (ellipse.X + _x) > 665) { _x *= -1; } //okienko ma 700 px na x
                if ((ellipse.Y + _y) < 12 || (ellipse.Y + _y) > 290) { _y *= -1; }

                ellipse.X += _x;
                ellipse.Y += _y;

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

                    if (_x == 0) _x += 1;
                    if (_y == 0) _y += 1;

                    // Ensure ellipses stay within the bounds
                    if (((ellipse.X + _x) < 50) || ((ellipse.X + _x) > 550)) { _x *= -1; }
                    if (((ellipse.Y + _y) < 0) || ((ellipse.Y + _y) > 400)) { _y *= -1; }

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
/*
using kulki3.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
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

        private int _numberOfEllipses;
        public int NumberOfEllipses
        {
            get { return _numberOfEllipses; }
            set
            {
                if (_numberOfEllipses != value)
                {
                    _numberOfEllipses = value;
                    InitializeEllipses();
                    OnPropertyChanged(nameof(Ellipses));
                    OnPropertyChanged(nameof(NumberOfEllipses));
                }
            }
        }

        public KulkomaniaViewModel()
        {
            NumberOfEllipses = 5; // Default number of ellipses
            _movementThread = new Thread(MoveAllEllipses);
            _movementThread.Start();
        }

        private void InitializeEllipses()
        {
            Random random = new Random();
            Ellipses = new ObservableCollection<EllipseModel>();
            for (int i = 0; i < NumberOfEllipses; i++)
            {
                EllipseModel ellipse = new EllipseModel
                {
                    X = random.Next(12, 665), // Bounds for X coordinate
                    Y = random.Next(12, 290), // Bounds for Y coordinate
                    Color = Brushes.Blue // Color
                };
                Ellipses.Add(ellipse);
            }
        }

        private void MoveAllEllipses()
        {
            while (true)
            {
                foreach (var ellipse in Ellipses)
                {
                    MoveEllipse(ellipse);
                }
                Thread.Sleep(30); // Adjust sleep time as needed for desired speed
            }
        }

        private void MoveEllipse(EllipseModel ellipse)
        {
            Random random = new Random();
            double _x;
            double _y;

            // Adjust position randomly for smooth movement
            _x = random.Next(-10, 11);
            _y = random.Next(-10, 11);

            // Ensure ellipses stay within the bounds
            if ((ellipse.X + _x) < 12 || (ellipse.X + _x) > 665) { _x *= -1; } //okienko ma 700 px na x
            if ((ellipse.Y + _y) < 12 || (ellipse.Y + _y) > 290) { _y *= -1; }

            ellipse.X += _x;
            ellipse.Y += _y;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
*/