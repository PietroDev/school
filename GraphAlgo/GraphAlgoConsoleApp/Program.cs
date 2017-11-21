using System;
using System.Linq;
using GraphAlgo.Data;
using GraphAlgo.Library;

namespace GraphConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"xml/graph.xml";
            IGraph g = new Graph();
            g.CreateFromXmlDocument(path);
            IVertex[] vs = g.Vertices.ToArray();
            Random random = new Random();
            IVertex v = vs[random.Next(vs.Length)];
            IVertex w = vs[random.Next(vs.Length)];

            AllPairShortestPath ap = new AllPairShortestPath(g);
            ap.Compute();
            Path p = ap.FindShortestPath(v, w);
            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
