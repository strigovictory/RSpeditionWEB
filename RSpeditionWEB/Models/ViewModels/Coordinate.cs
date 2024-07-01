namespace RSpeditionWEB.Models.ViewModels
{
    public class CoordinateClass<T> : Coordinate
    {
        public CoordinateClass() : base()
        { }

        public CoordinateClass(T item, double x, double y) : base(x, y)
        {
            this.ItemSelected = item;
        }
        public T ItemSelected { get; set; }
    }

    public class Coordinate
    {
        public Coordinate()
        { }

        public Coordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
