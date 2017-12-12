using System;
using System.Linq;
using System.Collections.Generic;
using GraphAlgo.Library;

namespace GraphAlgo.Data
{
    /**
     * Dijkstra's algorithm O((E+V)logV)
     */
    public sealed class SingleSourceShortestPath
    {
        private readonly IGraph _graph;
        private readonly IVertex _start;
        private IDictionary<IVertex, IVertex> _parents = new Dictionary<IVertex, IVertex>();

        public SingleSourceShortestPath(IGraph graph, IVertex start)
        {
            _graph = graph;
            _start = start;
        }

        public void Compute()
        {
            IDictionary<IVertex, double> distances = new Dictionary<IVertex, double>();
            IList<IVertex> nodes = new List<IVertex>();
            ISet<IVertex> visited = new HashSet<IVertex>();
            _parents.Clear();

            foreach (IVertex v in _graph.Vertices)
            {
                distances.Add(v, Double.PositiveInfinity);
                nodes.Add(v);
            }
            distances[_start] = 0;

            while (nodes.Count > 0)
            {
                // May be better with PriorityQueue
                nodes.OrderBy(w => distances[w]);
                IVertex u = nodes[0];
                nodes.RemoveAt(0);
                visited.Add(u);

                foreach (IVertex v in _graph.GetAdjacentOf(u)) {
                    if (visited.Contains(v))
                        continue;
                    IEdge e = _graph.FindEdgeConnecting(u, v);
                    double dist = distances[u] + e.Weight;
                    if (dist < distances[v]) {
                        distances[v] = dist;
                        _parents[v] = u;
                    }
                }
            }
        }

        public IPath GetShortestPath(IVertex target) {
            Path path = new Path(target);
            IVertex v = target;
            do
            {
                IVertex p = _parents[v];
                path.AddEdgeFirst(_graph.FindEdgeConnecting(p, v));
                v = p;
            } while (v != _start);
            return path;
        }
    }
}
