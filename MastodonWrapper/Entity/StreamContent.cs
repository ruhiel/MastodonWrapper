using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastodonWrapper.Entity
{
    public class StreamContent<T>
    {
        public T[] Content { get; set; }
        public int? Prev { get; set; }
        public int? Next { get; set; }
    }
}
