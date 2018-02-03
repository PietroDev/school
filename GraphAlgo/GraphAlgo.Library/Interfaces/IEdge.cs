namespace GraphAlgo.Library
{
    public interface IEdge : IPosition
    {
        IVertex Start { get; }
        
        IVertex End { get; }

        double Weight { get; set; }
        
        bool IsEdgeOf(IVertex v);

        IVertex GetOppositeOf(IVertex v);
    }
}
