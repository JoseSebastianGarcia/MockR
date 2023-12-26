using System;
using System.Collections.Generic;

namespace MockR
{
    public class CloneableList<T> : List<T>, ICloneable
    {
        public object Clone() => this.MemberwiseClone();

        //public void ForEach(Func<T> item) => this.ForEach(item);
    }
}
