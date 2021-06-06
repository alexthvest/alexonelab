using AlexOneLab.Events;
using AlexOneLab.Random;
using Replikit.Adapters.Telegram;
using Replikit.Core;
using Replikit.Core.Hosting.Modules;

ReplikitHost.RunModule<ProgramModule>();

public class ProgramModule : IReplikitModule
{
    public void ConfigureModules(IModuleCollection modules)
    {
        modules.AddModule<EventsModule>();
        modules.AddModule<RandomModule>();
        
        modules.AddTelegram();
    }
}