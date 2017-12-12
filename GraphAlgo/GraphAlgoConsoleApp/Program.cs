using System;
using GraphAlgo.Data;
using GraphAlgo.Library;

namespace GraphConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"xml/Rete.xml";
            IGraph g = new Graph();
            g.CreateFromXmlDocument(path);
            IVertex v = g.Vertices.FindByID("B");
            IVertex w = g.Vertices.FindByID("F");

            SingleSourceShortestPath sp = new SingleSourceShortestPath(g, v);
            sp.Compute();
            IPath p = sp.GetShortestPath(w);
            Console.WriteLine($"{p} (weight: {p.TotalWeight})");
            Console.ReadLine();
        }
    }
}
