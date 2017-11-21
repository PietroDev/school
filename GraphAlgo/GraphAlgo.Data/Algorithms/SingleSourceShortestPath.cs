using System;
using System.Collections.Generic;
using GraphAlgo.Library;

namespace GraphAlgo.Data
{
    /**
     * Dijkstra's algorithm
     */
    public sealed class SingleSourceShortestPath
    {
        private readonly IGraph _graph;
        private IDictionary<IVertex, int> _maxPaths = new Dictionary<IVertex, int>();

        public int this[IVertex v]
        {
            get
            {
                return _maxPaths[v];
            }
            private set
            {
                _maxPaths[v] = value;
            }
        }

        public SingleSourceShortestPath(IGraph graph)
        {
            _graph = graph;
        }

        public void Compute(IVertex start)
        {
            Queue<IVertex> queue = new Queue<IVertex>();
            HashSet<IVertex> visited = new HashSet<IVertex>();
            Queue<IVertex> parentQueue = new Queue<IVertex>();
            _maxPaths.Clear();

            foreach (IVertex v in _graph.Vertices)
            {
                this[v] = Int32.MaxValue;
            }
            queue.Enqueue(start);
            this[start] = 0;
            visited.Add(start);

            while (queue.Count > 0)
            {
                IVertex v = queue.Dequeue();
                IVertex p = parentQueue.Count > 0 ? parentQueue.Dequeue() : null;
                foreach (IVertex u in _graph.AdjacentOf(v))
                {
                    if (visited.Contains(u))
                        break;
                    if (u == p)
                        continue;
                    queue.Enqueue(u);
                    parentQueue.Enqueue(v);
                    this[u] = this[v] + 1;
                    visited.Add(u);
                }
            }
        }
    }
}