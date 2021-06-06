using AlexOneLab.Events.Managers;
using AlexOneLab.Events.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Messages.Features;
using Replikit.Core.Hosting.Modules;
using Replikit.Core.Hosting.Requirements;
using Replikit.Extensions.Storage;

namespace AlexOneLab.Events
{
    public class EventsModule : IReplikitModule
    {
        public void ConfigureRequirements(IRequirementCollection requirements)
        {
            requirements.RequireMessageCollection(MessageCollectionFeatures.Edit);
            requirements.RequireMessageCollection(MessageCollectionFeatures.Pin);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRepository<EventRepository>();
            services.AddRepository<CountdownRepository>();

            services.AddSingleton<CountdownManager>();
            
            services.AddStorage();
        }
    }
}
