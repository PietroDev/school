using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphAlgo.Library
{
    public interface IPath
    {
        IVertex Start { get; }

        IVertex End { get; }

        IEnumerable<IEdge> Edges { get; }

        double TotalWeight { get; }

    }
}
