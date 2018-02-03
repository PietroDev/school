using System.Collections.Generic;
using System.Linq;
using GraphAlgo.Library;

namespace GraphAlgo.Data
{
    public class Graph : IGraph
    {
        private readonly IList<Edge> _edges = new List<Edge>();
        private readonly IList<Vertex> _vertices = new List<Vertex>();

        public IEnumerable<IVertex> Vertices
        {
            get
            {
                return _vertices;
            }
        }

        public IVertex NewVertex(string id)
        {
            Vertex v = new Vertex(id);
            _vertices.Add(v);
            return v;
        }

        public IEnumerable<IEdge> Edges
        {
            get
            {
                return _edges;
            }
        }

        public IEdge NewEdge(string id, IVertex v1, IVertex v2)
        {
            Edge e = new Edge(id, v1, v2);
            _edges.Add(e);
            return e;
        }

        public IEnumerable<IVertex> GetAdjacentOf(IVertex v)
        {
            IList<IVertex> list = new List<IVertex>();
            foreach (IEdge e in EdgesOf(v))
            {
                IVertex w = e.GetOppositeOf(v);
                list.Add(w);
            }
            return list;
            // LINQ: return EdgesOf(v).Select(e => e.GetOppositeOf(v));
        }

        public IEnumerable<IEdge> EdgesOf(IVertex v)
        {
            IList<IEdge> list = new List<IEdge>();
            foreach (IEdge e in Edges)
            {
                if (e.IsEdgeOf(v))
                    list.Add(e);
            }
            return list;
            // LINQ: return Edges.Where(e => e.IsEdgeOf(v));
        }

        public IEdge FindEdgeConnecting(IVertex v, IVertex u)
        {
            foreach (IEdge e in Edges)
            {
                if (e.IsEdgeOf(v) && e.IsEdgeOf(u))
                    return e;
            }
            return null;
            // LINQ: return Edges.Where(e => e.IsEdgeOf(v) && e.IsEdgeOf(u)).SingleOrDefault();
        }
    }
}