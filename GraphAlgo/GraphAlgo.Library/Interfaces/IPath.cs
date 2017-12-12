using System;
using System.Linq;

namespace GraphAlgo.Library
{
    public interface IPath
    {
        IVertex Start { get; }

        IVertex End { get; }

        IQueryable<IEdge> Edges { get; }

        double TotalWeight { get; }

    }
}
