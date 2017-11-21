namespace Graph.Core.Library
{
    public class Edge : Position, IEdge
    {

        public IVertex Start { get; set; }

        public IVertex End { get; set; }

        public double Weight { get; set; }

        public Edge(string id) : base(id)
        {
            Weight = 1;
        }

        public bool IsEdgeOf(IVertex v)
        {
            return v == Start || v == End;
        }

        public IVertex GetOppositeOf(IVertex v)
        {
            return v == Start ? End : v == End ? Start : null;
        }

        public override string ToString()
        {
            return Label;
        }
    }
}