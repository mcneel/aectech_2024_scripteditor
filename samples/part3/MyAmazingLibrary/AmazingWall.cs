using Rhino.Geometry;

namespace MyAmazingLibrary
{
    public class AmazingWall
    {
        public Guid Id { get; set; }
        public string Property { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Depth { get; set; }

        public AmazingWall() { }
        public AmazingWall(string property, double width, double height, double depth)
        {
            Id = Guid.NewGuid();
            Property = property;
            Width = width;
            Height = height;
            Depth = depth;
        }

        public void CreateWall()
        {
            var box = new Box(Plane.WorldXY, new Interval(0, Width), new Interval(0, Height), new Interval(0, Depth));
            Rhino.RhinoDoc.ActiveDoc.Objects.AddBox(box);
        }

        public void PrintInfo() => Console.WriteLine($"Created element with id: {Id}");
    }
}
