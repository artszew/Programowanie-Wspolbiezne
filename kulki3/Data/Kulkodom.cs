using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Kulkodom: IKulkodom
    {
        private int _length;
        private int _width;
        private ConcurrentBag<IEllipse> _repository;
        private readonly object _lock = new object();

        public Kulkodom(int length, int width)
        {
            this._length = length;
            this._width = width;
            _repository = new ConcurrentBag<IEllipse>();
        }
        public int Length
        {
            get => _length;
            set => _length = value;
        }
        public int Width
        {
            get => this._width;
            set => this._width = value;
        }
        public ConcurrentBag<IEllipse> getRepository() => _repository;
        public void setRepository(ConcurrentBag<IEllipse> repo)
        {
            _repository = repo;
        }
        public void addEllipse(IEllipse Ellipse) { _repository.Add(Ellipse); }
        public void removeEllipse(IEllipse Ellipse) {
            if (Ellipse == null) throw new NullReferenceException();
            ConcurrentBag<IEllipse> newBag = new ConcurrentBag<IEllipse>(_repository.Except(new[] { Ellipse }));
            _repository = newBag;
        }
        public int getSize() {  return _repository.Count(); }
        public object Lock => _lock;
    }
}
