using System;
using System.Linq;
using Graph.Core.Library;
using Graph.Helpers.Library;

namespace GraphConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"xml/graph.xml";
            IGraph g = new Graph.Core.Library.Graph();
            g.CreateFromXmlDocument(path);
            IVertex[] vs = g.Vertices.ToArray();
            Random random = new Random();
            IVertex v = vs[random.Next(vs.Length)];
            IVertex w = vs[random.Next(vs.Length)];

            AllPairShortestPath ap = g.AllPairShortestPath();

            Path p = ap.ShortestPath(v, w);
            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
