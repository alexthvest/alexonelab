using Replikit.Adapters.Telegram;
using Replikit.Core;
using Replikit.Core.Hosting.Modules;

ReplikitHost.RunModule<ProgramModule>();

public class ProgramModule : IReplikitModule
{
    public void ConfigureModules(IModuleCollection modules)
    {
        modules.AddTelegram();
    }
}