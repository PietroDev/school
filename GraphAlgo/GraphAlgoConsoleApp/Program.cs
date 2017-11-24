using System;
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
            IVertex v = g.Vertices.FindByID("n0");
            IVertex w = g.Vertices.FindByID("n5");

            AllPairShortestPath ap = new AllPairShortestPath(g);
            ap.Compute();
            Path p = ap.FindShortestPath(v, w);
            Console.WriteLine(p);
            Console.ReadLine();
        }
    }
}
