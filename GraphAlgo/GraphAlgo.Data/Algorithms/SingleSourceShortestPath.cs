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
            // Clear MST
            _parents.Clear();

            // No vertex is reachable ...
            foreach (IVertex v in _graph.Vertices)
            {
                distances.Add(v, Double.PositiveInfinity);
                nodes.Add(v);
            }
            // ... except the source
            distances[_start] = 0;

            while (nodes.Count > 0)
            {
                // Find the closest vertex, (it is better with a PriorityQueue)
                nodes.OrderBy(w => distances[w]);
                IVertex u = nodes[0];
                nodes.RemoveAt(0);
                visited.Add(u);

                foreach (IVertex v in _graph.GetAdjacentOf(u)) {
                    // Not visiting a vertex twice
                    if (visited.Contains(v))
                        continue;
                    IEdge e = _graph.FindEdgeConnecting(u, v);
                    double dist = distances[u] + e.Weight;
                    // Replace distance if closer
                    if (dist < distances[v]) {
                        distances[v] = dist;
                        _parents[v] = u;
                    }
                }
            }
        }

        /// <summary>
        /// This will create the path, starting from target and taking parents recursively
        /// </summary>
        public IPath GetShortestPath(IVertex target) {
            Path path = new Path(target);
            IVertex v = target;
            do
            {
                IVertex p = _parents[v];
                IEdge e = _graph.FindEdgeConnecting(p, v);
                path.AddEdgeFirst(e);
                v = p;
            } while (v != _start); // Stop when start has been reached
            return path;
        }
    }
}
