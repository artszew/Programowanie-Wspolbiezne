using kulki3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kulki3.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand AutorzyViewCommand { get; set; }
        public RelayCommand KulkomaniaViewCommand { get; set; }

        public AutorzyViewModel AutorzyVM { get; set; }
        public KulkomaniaViewModel KulkomaniaVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel() 
        {
            AutorzyVM = new AutorzyViewModel();
            KulkomaniaVM = new KulkomaniaViewModel();

            CurrentView = AutorzyVM;

            AutorzyViewCommand = new RelayCommand(o => 
            {
                CurrentView = AutorzyVM;
            });

            KulkomaniaViewCommand = new RelayCommand(o =>
            {
                CurrentView = KulkomaniaVM;
            });
        }
    }
}
