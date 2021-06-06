using AlexOneLab.Events.Models;
using Replikit.Extensions.Storage.Repositories;

namespace AlexOneLab.Events.Repositories
{
    public class CountdownRepository : EntityRepository<Countdown>
    {
        public CountdownRepository(RepositoryContext<Countdown> context) : base(context) {}
    }
}
