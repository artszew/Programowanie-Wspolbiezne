using Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IAllOperations
    {
        public event NotifyDelegateAllOperations.NotifyEllipseController? OnChange;
        void NewEllipse();
        void DeleteEllipse(int id);
        void ResolveCollision(IEllipse Ellipse);
        void ClearKulkodom();
        int GetSize();
        int GetLength();
        int GetWidth();
        ConcurrentBag<IEllipse> GetEllipseRepo();
    }
}
