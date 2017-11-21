using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using Graph.Core.Library;

namespace Graph.Helpers.Library
{
    public static class GraphHelper
    {
        public static void CreateFromXmlDocument(this IGraph g, string file) {
            XDocument doc = XDocument.Load(file);

            XElement gml = doc.Descendants().First();
            var weightNode = gml
                            .Descendants()
                            .Single(n => n.Name.LocalName == "key" &&
                                         n.Attribute("attr.name").Value == "weight");
            String weightKey = weightNode.Attribute("id").Value;
            double weightDefault = Convert.ToDouble(weightNode.Value);

            XElement graph = gml.Descendants().Single(n => n.Name.LocalName == "graph");

            foreach (XElement n in graph.Descendants().Where(n => n.Name.LocalName == "node"))
            {
               IVertex v = g.NewVertex(n.Attribute("id").Value);
            }
            foreach (XElement n in graph.Descendants().Where(n => n.Name.LocalName == "edge"))
            {
                IVertex source = FindByID(g.Vertices, n.Attribute("source").Value);
                IVertex target = FindByID(g.Vertices, n.Attribute("target").Value);
                IEdge e = g.NewEdge(n.Attribute("id").Value, source, target);
                XElement wn = n.Descendants()
                               .SingleOrDefault(m => m.Name.LocalName == "data" &&
                                                     m.Attribute("key").Value == weightKey);
                e.Weight = wn == null ? weightDefault : Convert.ToDouble(wn.Value);
            }
        }

        private static P FindByID<P>(IQueryable<P> q, string id) where P : IPosition
        {
            return q.FirstOrDefault(p => p.ID == id);
        }
    }
}
