using System.Collections.Generic;
using System.Linq;
using GraphAlgo.Library;

namespace GraphAlgo.Data
{
    public class Graph : IGraph
    {
        private readonly IList<Edge> _edges = new List<Edge>();
        private readonly IList<Vertex> _vertices = new List<Vertex>();

        public IQueryable<IVertex> Vertices
        {
            get
            {
                return _vertices.AsQueryable();
            }
        }

        public IVertex NewVertex(string id)
        {
            Vertex v = new Vertex(id);
            _vertices.Add(v);
            return v;
        }

        public IQueryable<IEdge> Edges
        {
            get
            {
                return _edges.AsQueryable();
            }
        }

        public IEdge NewEdge(string id, IVertex v1, IVertex v2)
        {
            Edge e = new Edge(id)
            {
                Start = v1,
                End = v2
            };
            _edges.Add(e);
            return e;
        }

        public IEnumerable<IVertex> GetAdjacentOf(IVertex v)
        {
            return EdgesOf(v).Select(e => e.GetOppositeOf(v));
        }

        public IEnumerable<IEdge> EdgesOf(IVertex v)
        {
            return Edges.Where(e => e.IsEdgeOf(v));
        }

        public IEdge FindEdgeConnecting(IVertex v, IVertex u)
        {
            return Edges.Where(e => e.IsEdgeOf(v) && e.IsEdgeOf(u)).SingleOrDefault();
        }
    }
}