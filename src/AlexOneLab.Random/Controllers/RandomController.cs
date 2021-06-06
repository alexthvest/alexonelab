using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;

namespace AlexOneLab.Random.Controllers
{
    public class RandomController : Controller
    {
        [Command("probability")]
        [Command("вероятность")]
        public OutMessage Probability(string eventName)
        {
            var percent = new System.Random().Next(0, 100);
            
            return OutMessage.FromCode($"Вероятность {eventName} равна {percent}%");
        }
    }
}
