using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using AlexOneLab.Events.Models;
using AlexOneLab.Events.Repositories;
using MongoDB.Bson;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models;
using SmartFormat;

namespace AlexOneLab.Events.Managers
{
    public class CountdownManager
    {
        private readonly CountdownRepository _countdownRepository;
        
        private readonly Dictionary<ObjectId, Timer> _timers = new();

        public CountdownManager(CountdownRepository countdownRepository)
        {
            _countdownRepository = countdownRepository;
        }

        public async Task<Countdown> CreateAsync(Event @event, IMessageCollection messageCollection)
        {
            var date = @event.DateTime;
            var now = DateTimeOffset.UtcNow.ToOffset(date.Offset);
            var span = date.Subtract(now);
            
            var countdownMessage = BuildCountdownMessage(@event, span);
            
            var sentMessage = await messageCollection.Send(countdownMessage);
            await messageCollection.Pin(sentMessage.Id);

            var countdown = new Countdown(@event, sentMessage.Id);
            await _countdownRepository.Save(countdown);

            return countdown;
        }

        public void Start(Countdown countdown, IMessageCollection messageCollection)
        {
            if (!_timers.TryGetValue(countdown.Id, out var timer))
            {
                timer = new Timer(1000 * (60 - DateTime.UtcNow.Second));
                
                timer.Elapsed += async (_, _) =>
                {
                    timer.Interval = 60 * 1000;
                    await ElapsedHandler(countdown, messageCollection);
                };

                _timers.Add(countdown.Id, timer);
            }

            timer.Stop();
            timer.Start();
        }

        public async Task StopAsync(Countdown countdown, IMessageCollection messageCollection)
        {
            if (_timers.TryGetValue(countdown.Id, out var timer))
            {
                timer.Stop();
                _timers.Remove(countdown.Id);
            }

            await messageCollection.Unpin(countdown.MessageId);
            await messageCollection.Delete(countdown.MessageId);

            await _countdownRepository.Delete(countdown);
        }

        private OutMessage BuildCountdownMessage(Event @event, TimeSpan span)
        {
            var template = "Until {0}:{1:cond:>0? {1} {1:day|days}|}{2:cond:>0? {2} {2:hour|hours}|}{3:cond:>0? {3} {3:minute|minutes}|}";
            var message = Smart.Format(template, @event.Name, span.Days, span.Hours, span.Minutes + 1);

            return OutMessage.FromCode(message);
        }

        private async Task ElapsedHandler(Countdown countdown, IMessageCollection messageCollection)
        {
            var date = countdown.Event.DateTime;
            var now = DateTimeOffset.UtcNow.ToOffset(date.Offset);
            var span = date.Subtract(now);

            if (span <= TimeSpan.Zero)
            {
                await StopAsync(countdown, messageCollection);
                return;
            }
            
            var countdownMessage = BuildCountdownMessage(countdown.Event, span);
            await messageCollection.Edit(countdown.MessageId, countdownMessage);
        }
    }
}
