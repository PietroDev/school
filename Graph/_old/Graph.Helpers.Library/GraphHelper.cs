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
        public static void CreateFromXmlDocument(IGraph g, string file) {
            XDocument doc;
            using (FileStream str = new FileStream(file, FileMode.Open))
            {
                doc = XDocument.Load(str);
            }
            XElement gml = doc.Descendants().First();
            var weightNode = gml.Descendants().Where(n => n.Name.LocalName == "key")
                .Where(n => n.Attribute("attr.name").Value == "weight").Single();
            String weightKey = weightNode.Attribute("id").Value;
            double weightDefault = Convert.ToDouble(weightNode.Value);

            XElement graph = gml.Descendants().Where(n => n.Name.LocalName == "graph").Single();

            foreach (XElement n in graph.Descendants().Where(n => n.Name.LocalName == "node"))
            {
               IVertex v = g.NewVertex(n.Attribute("id").Value);
            }
            foreach (XElement n in graph.Descendants().Where(n => n.Name.LocalName == "edge"))
            {
                IVertex source = FindByID(g.Vertices, n.Attribute("source").Value);
                IVertex target = FindByID(g.Vertices, n.Attribute("target").Value);
                IEdge e = g.NewEdge(n.Attribute("id").Value, source, target);
                XElement wn = n.Descendants().Where(m => m.Name.LocalName == "data")
                    .Where(a => a.Attribute("key").Value == weightKey)
                    .SingleOrDefault();
                e.Weight = wn == null ? weightDefault : Convert.ToDouble(wn.Value);
            }
        }

        private static P FindByID<P>(IQueryable<P> q, string id) where P : IPosition
        {
            return q.Where(p => p.ID == id).FirstOrDefault();
        }
    }
}
