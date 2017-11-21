using System;
using System.Linq;

namespace Graph
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string file = @"graph.xml";
            IGraph g = new Graph();
            GraphMLReader.CreateFromXmlDocument(g, file);
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