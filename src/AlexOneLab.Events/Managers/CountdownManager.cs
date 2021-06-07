using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using AlexOneLab.Events.Models;
using AlexOneLab.Events.Repositories;
using AlexOneLab.Events.Resources;
using MongoDB.Bson;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Localization;

namespace AlexOneLab.Events.Managers
{
    public class CountdownManager
    {
        private readonly EventRepository _eventRepository;
        private readonly CountdownRepository _countdownRepository;
        private readonly ILocalizer _localizer;

        private readonly Dictionary<ObjectId, Timer> _timers = new();

        public CountdownManager(EventRepository eventRepository, CountdownRepository countdownRepository, ILocalizer localizer)
        {
            _eventRepository = eventRepository;
            _countdownRepository = countdownRepository;
            _localizer = localizer;
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

            if (span <= TimeSpan.Zero)
            {
                await StopAsync(countdown, messageCollection, false);
            }

            return countdown;
        }

        public void Start(Countdown countdown, IMessageCollection messageCollection)
        {
            if (!_timers.TryGetValue(countdown.Id, out var timer))
            {
                timer = new Timer(1000 * (61 - DateTime.UtcNow.Second));
                
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

        public async Task StopAsync(Countdown countdown, IMessageCollection messageCollection, bool stopped = true)
        {
            if (_timers.TryGetValue(countdown.Id, out var timer))
            {
                timer.Stop();
                _timers.Remove(countdown.Id);
            }
            
            await messageCollection.Unpin(countdown.MessageId);

            if (stopped)
            {
                await messageCollection.Delete(countdown.MessageId);
            }
            else
            {
                await _eventRepository.Delete(countdown.Event.Id);
            }
            
            await _countdownRepository.Delete(countdown);
        }

        private OutMessage BuildCountdownMessage(Event @event, TimeSpan span)
        {
            if (span <= TimeSpan.Zero)
            {
                return OutMessage.FromCode(Locale.EventEnded);
            }
            
            var message = _localizer[Locale.UntilEvent, @event.Name, span.Days, span.Hours, span.Minutes + 1];
            return OutMessage.FromCode(message);
        }

        private async Task ElapsedHandler(Countdown countdown, IMessageCollection messageCollection)
        {
            var date = countdown.Event.DateTime;
            var now = DateTimeOffset.UtcNow.ToOffset(date.Offset);
            var span = date.Subtract(now);

            if (span <= TimeSpan.Zero)
            {
                await StopAsync(countdown, messageCollection, false);
            }

            try
            {
                var countdownMessage = BuildCountdownMessage(countdown.Event, span);
                await messageCollection.Edit(countdown.MessageId, countdownMessage);
            }
            catch
            {
                // ignored
            }
        }
    }
}
