using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public interface IGraph
    {

        IVertex NewVertex(string id);

        IEdge NewEdge(string id, IVertex source, IVertex target);

        IQueryable<IVertex> Vertices { get; }

        IQueryable<IEdge> Edges { get; }

        IEnumerable<IVertex> AdjacentOf(IVertex v);

        IEnumerable<IEdge> EdgesOf(IVertex v);

        IEdge FindEdgeConnecting(IVertex v, IVertex w);
    }
}
