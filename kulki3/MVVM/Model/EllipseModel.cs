using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace kulki3.MVVM.Model
{
    public class EllipseModel : INotifyPropertyChanged
    {
        private double _x;
        public double X
        {
            get { return _x; }
            set { _x = value; OnPropertyChanged(nameof(X)); }
        }

        private double _y;
        public double Y
        {
            get { return _y; }
            set { _y = value; OnPropertyChanged(nameof(Y)); }
        }

        private Brush _color;
        public Brush Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged(nameof(Color)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
