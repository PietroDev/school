using GraphAlgo.Library;

namespace GraphAlgo.Data
{
    public class Vertex : Position, IVertex
    {
        public Vertex(string id) : base(id)
        {
        }

        public override string ToString()
        {
            return Label;
        }
    }
}