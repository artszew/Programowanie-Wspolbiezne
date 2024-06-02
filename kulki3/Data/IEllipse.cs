namespace Data
{
    public interface IEllipse
    {
        int Id { get; set; }
        string Color { get; }
        double Mass { get; set; }
        double Diameter { get; set; }
        double X { get; set; }
        double Y { get; set; }   
        double velocityX { get; set; }
        double velocityY { get; set; }
        bool IsMoving { get; set; }

        event NotifyDelegateEllipse.NotifyEllipse? OnChange;
        void MoveAsync(Barrier Wall);
        public object Lock { get; }
    }
}
