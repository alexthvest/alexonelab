using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AlexOneLab.Events.Models;
using MongoDB.Driver;
using Replikit.Extensions.Storage.Repositories;

namespace AlexOneLab.Events.Repositories
{
    public class EventRepository : EntityRepository<long, Event>
    {
        public EventRepository(RepositoryContext<long, Event> context) : base(context) {}

        public async Task<IReadOnlyList<Event>> FindAllAndSortBy(Expression<Func<Event, object>> field)
        {
            return await Collection
                .Find(FilterDefinition<Event>.Empty)
                .SortBy(field)
                .ToListAsync();
        }
    }
}
