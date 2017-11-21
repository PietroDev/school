using System;
using System.Collections.Generic;

namespace Graph
{
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

            IVertex p;
            while (queue.Count > 0)
            {
                IVertex v = queue.Dequeue();
                parentQueue.TryDequeue(out p);
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