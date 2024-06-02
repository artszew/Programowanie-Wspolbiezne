
using Data;
using Logic;
using System.Collections.Concurrent;

namespace Model
{
    public class KulkomaniaModel
    {
        private IAllOperations _controller;

        public KulkomaniaModel(int x, int y)
        {
             this._controller = new AllOperations(x, y);
        }

        public void AddEllipse()
        {
            _controller.NewEllipse();
        }

        public void DeleteEllipse()
        {
            _controller.DeleteEllipse(_controller.GetSize()-1);
        }

        public void ClearKulkodom()
        {
            _controller.ClearKulkodom();
        }
        public IAllOperations GetController {
            get => _controller;
        }

        public ConcurrentBag<IEllipse> GetEllipses() => _controller.GetEllipseRepo();

        public int GetLength() => _controller.GetLength();

        public int GetWidth() => _controller.GetWidth();

        public int GetSize() => _controller.GetSize();
    }

}
