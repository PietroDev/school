namespace Graph
{
    public abstract class Position : IPosition
    {
        public string ID { get; private set; }

        public string Label { get; set; }

        protected Position(string id)
        {
            ID = id;
            Label = ID;
        }
    }
}
