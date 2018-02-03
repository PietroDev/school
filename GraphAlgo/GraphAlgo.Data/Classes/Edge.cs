using GraphAlgo.Library;

namespace GraphAlgo.Data
{
    public class Edge : Position, IEdge
    {
        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        public double Weight { get; set; }

        public Edge(string id, IVertex start, IVertex end) : base(id)
        {
            Weight = 1;
            Start = start;
            End = end;
        }

        public bool IsEdgeOf(IVertex v)
        {
            return v == Start || v == End;
        }

        public IVertex GetOppositeOf(IVertex v)
        {
            if (v == Start)
                return End;
            if (v == End)
                return Start;
            return null;
        }
    }
}