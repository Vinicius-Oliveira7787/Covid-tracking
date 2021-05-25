using System;
using System.Collections.Generic;

namespace Domain
{
    public class Publish
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
