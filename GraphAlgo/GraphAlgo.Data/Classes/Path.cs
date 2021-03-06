﻿using System;
using System.Collections.Generic;
using System.Linq;
using GraphAlgo.Library;

namespace GraphAlgo.Data
{
    public sealed class Path : IPath
    {
        private readonly List<IEdge> _edges = new List<IEdge>();

        public IVertex Start { get; private set; }

        public IVertex End { get; private set; }

        public double TotalWeight
        {
            get
            {
                double w = 0;
                foreach (IEdge e in Edges)
                {
                    w += e.Weight;
                }
                return w;
                // LINQ: return Edges.Sum(e => e.Weight);
            }
        }

        public IEnumerable<IEdge> Edges
        {
            get
            {
                return _edges;
            }
        }

        public Path(IVertex start)
        {
            Start = start;
            End = start;
        }

        public void AddEdgeLast(IEdge e)
        {
            if (!e.IsEdgeOf(End))
                throw new ArgumentException("Wrong edge");
            _edges.Add(e);
            End = e.GetOppositeOf(End);
        }

        public void AddEdgeFirst(IEdge e) {
            if (!e.IsEdgeOf(Start))
                throw new ArgumentException("Wrong edge");
            _edges.Insert(0, e);
            Start = e.GetOppositeOf(Start);
        }

        public override string ToString()
        {
            var resp = $"Path: [{Start}";
            if (Edges.Any())
            {
                IVertex vertex = Start;
                foreach (IEdge edge in _edges)
                {
                    vertex = edge.GetOppositeOf(vertex);
                    resp = $"{resp} -> {vertex} ({edge})";
                }
                resp = $"{resp}]";
            }
            else
            {
                resp = $"{resp} -> {End}]";
            }
            return resp;
        }
    }
}