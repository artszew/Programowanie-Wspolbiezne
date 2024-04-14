using kulki3.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace kulki3.MVVM.ViewModel 
{
    class KulkomaniaViewModel : INotifyPropertyChanged
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
                    X = random.Next(0+50, 800-50), // Bounds for X coordinate
                    Y = random.Next(0+50, 450-50), // Bounds for Y coordinate
                    Color = Brushes.Blue // Color
                };
                Ellipses.Add(ellipse);
            }
        }

        private void MoveEllipses()
        {
            Random random = new Random();
            while (_isMoving)
            {
                // Update the positions of all ellipses
                foreach (var ellipse in Ellipses)
                {
                    // Adjust position randomly for smooth movement
                    ellipse.X += random.Next(-5, 6);
                    ellipse.Y += random.Next(-5, 6);

                    // Ensure ellipses stay within the bounds
                    if (ellipse.X < 0) ellipse.X = 0;
                    if (ellipse.X > 800-50) ellipse.X = 800-50;
                    if (ellipse.Y < 0) ellipse.Y = 0;
                    if (ellipse.Y > 450-50) ellipse.Y = 450-50;
                }

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
