using System.Linq;

namespace Graph.Core.Library
{
    public static class Utility
    {
        public static AllPairShortestPath AllPairShortestPath(this IGraph graph)
        {
            AllPairShortestPath p = new AllPairShortestPath(graph);
            p.Compute();
            return p;
        }

        public static SingleSourceShortestPath ShortestPath(this IGraph graph, IVertex start)
        {
            SingleSourceShortestPath p = new SingleSourceShortestPath(graph);
            p.Compute(start);
            return p;
        }
    }
}