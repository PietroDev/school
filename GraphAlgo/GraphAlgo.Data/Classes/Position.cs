﻿using GraphAlgo.Library;

namespace GraphAlgo.Data
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

        public override string ToString()
        {
            return Label;
        }
    }
}
