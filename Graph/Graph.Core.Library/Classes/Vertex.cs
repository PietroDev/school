namespace Graph.Core.Library
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