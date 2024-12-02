using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public class Aggregate<TId> : Entity<TId>
    {
        private List<DomainEvent> _domainEvents = [];
        public IReadOnlyList<DomainEvent> DomainEvents=>_domainEvents.AsReadOnly();
        public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents()=>_domainEvents.Clear(); 
    }
}
