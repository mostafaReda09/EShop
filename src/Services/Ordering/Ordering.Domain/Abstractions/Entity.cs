using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public class Entity<TId>
    {
        public TId Id { get; set; } = default!;
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }=string.Empty;
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModified { get; set; } 

    }
}
