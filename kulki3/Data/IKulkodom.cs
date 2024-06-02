using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Data
{
    public interface IKulkodom
    {
        int Length { get; set; }
        int Width { get; set; }
        ConcurrentBag<IEllipse> getRepository();
        void addEllipse(IEllipse Ellipse);
        void removeEllipse(IEllipse Ellipse);
        int getSize();
        void setRepository(ConcurrentBag<IEllipse> repository);

        public object Lock { get; }
    }
}
