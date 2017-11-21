using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Core.Library
{
    /**
     * Floyd–Warshall algorithm
     */
    public sealed class AllPairShortestPath
    {
        private readonly IGraph _graph;
        private readonly IDictionary<IVertex, int> _vertexIndex = new Dictionary<IVertex, int>();
        private double[,] _costs;
        private int[,] _predecessors;
        private IVertex[] _vertices;

        public AllPairShortestPath(IGraph graph)
        {
            _graph = graph;
        }

        public void Compute()
        {
            _vertices = _graph.Vertices.ToArray();
            _vertexIndex.Clear();

            for (int i = 0; i < _vertices.Length; i++)
                _vertexIndex[_vertices[i]] = i;

            _predecessors = new int[_vertices.Length, _vertices.Length];
            _costs = new double[_vertices.Length, _vertices.Length];

            for (int i = 0; i < _vertices.Length; i++)
            {
                for (int j = 0; j < _vertices.Length; j++)
                {
                    _predecessors[i, j] = -1;
                    _costs[i, j] = Double.PositiveInfinity;
                }

                IVertex u = _vertices[i];
                foreach (IEdge e in _graph.EdgesOf(u))
                {
                    int j = _vertexIndex[e.GetOppositeOf(u)];
                    _predecessors[i, j] = i;
                    _costs[i, j] = e.Weight;
                }

                // Cost from any node to itself is 0
                _costs[i, i] = 0;
                _predecessors[i, i] = i;
            }

            for (int i = 0; i < _vertices.Length; i++)
                for (int j = 0; j < _vertices.Length; j++)
                    for (int k = 0; k < _vertices.Length; k++)
                    {
                        if (_costs[j, i] + _costs[i, k] < _costs[j, k])
                        {
                            if (j == k)
                                throw new ArgumentException("Negative cycle");

                            // It is cheaper to go through I
                            _costs[j, k] = _costs[j, i] + _costs[i, k];
                            _predecessors[j, k] = i;
                        }
                    }
        }

        public Path ShortestPath(IVertex source, IVertex target)
        {
            int si = _vertexIndex[source];
            int ti = _vertexIndex[target];
            if (Double.IsPositiveInfinity(_costs[si, ti]))
                return null;
            Path p = new Path(source);
            if (source != target)
            {
                FillPath(p, si, ti);
                p.AddEdge(_graph.FindEdgeConnecting(p.End, target));
            }
            return p;
        }

        private void FillPath(Path p, int start, int end)
        {
            int mid = _predecessors[start, end];
            if (mid == -1)
                throw new ArgumentException("No subpath available: " + _vertices[start] + " -> " + _vertices[end]);
            if (start == end || start == mid)
                return;

            FillPath(p, start, mid);
            p.AddEdge(_graph.FindEdgeConnecting(p.End, _vertices[mid]));
            if (mid != _predecessors[mid, end])
                FillPath(p, mid, end);
        }
    }
}