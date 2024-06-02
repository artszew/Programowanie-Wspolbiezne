using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Resolvers;
using Data;
using System.Windows.Threading;
using Model;
using System.Collections;
using System.Windows.Controls;

namespace ViewModel
{
    public class KulkomaniaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            {
               PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }


        public ICommand _add { get; }
        public ICommand _delete { get; }
        public ICommand _clear { get; }
        
        private KulkomaniaModel _KulkomaniaModel;
 
        private int _length = 500;
        private int _width = 800;

        private bool _isAddEnable;
        private bool _isDeleteEnable;
        private bool _isClearEnable;
   
        

        public KulkomaniaViewModel()
        {
            _add = new RelayCommand(Add, ()=>IsAddEnable);
            _delete = new RelayCommand(Delete, ()=>IsDeleteEnable);
            _clear = new RelayCommand(ClearAll, ()=>IsClearEnable);
           
            _KulkomaniaModel = new KulkomaniaModel(_length, _width);
            _isAddEnable = true;
            _isDeleteEnable = false;
            _isClearEnable = false;
            _KulkomaniaModel.GetController.OnChange += Update;
            
        }

        public void Update()
        {
            OnPropertyChanged("GetSize");
            OnPropertyChanged("Ellipses");

        }

        public void Add()
        {
            _KulkomaniaModel.AddEllipse();
            IsDeleteEnable = _KulkomaniaModel.GetSize()>0;
            IsClearEnable = _KulkomaniaModel.GetSize() > 0;
            OnPropertyChanged("Ellipses");
            OnPropertyChanged("GetSize");
        }

        public void Delete()
        {
            if (_KulkomaniaModel.GetSize() > 1)
            {
                _KulkomaniaModel.DeleteEllipse();
            }
            else if(_KulkomaniaModel.GetSize() == 1)
            {
                IsClearEnable = false;
                IsDeleteEnable = false;
                _KulkomaniaModel.DeleteEllipse();
                
            }
            else
            {
                return;
            }
                OnPropertyChanged("Ellipses");
            OnPropertyChanged("GetSize");
            
        }

        public void ClearAll()
        {
            if (_KulkomaniaModel.GetSize() > 0) 
            {
                _KulkomaniaModel.ClearKulkodom();
                IsClearEnable = false;
                IsDeleteEnable = false;
            }
            else
            {
                return;
            };
            
                OnPropertyChanged("Ellipses");
            OnPropertyChanged("GetSize");

        }

        public IEllipse[]? Ellipses
        {
            get => _KulkomaniaModel.GetEllipses().ToArray();
        }

        public int GetKulkodomLength
        {
            get => _KulkomaniaModel.GetLength();
        }

        public int GetKulkodomWidth
        {
            get => _KulkomaniaModel.GetWidth();
        }

        public int GetSize
        {
            get => _KulkomaniaModel.GetSize();
        }

        public bool IsAddEnable
        {
            get => _isAddEnable;
            set {
                _isAddEnable = value;
                OnPropertyChanged();
            } 


        }
        public bool IsDeleteEnable
        {
            get => _isDeleteEnable;
            set
            {
                _isDeleteEnable = value;
                OnPropertyChanged();
            }

        }


        public bool IsClearEnable
        {
            get => _isClearEnable;
            set
            {
                _isClearEnable = value;
                OnPropertyChanged();
            }

        }
    }
}
