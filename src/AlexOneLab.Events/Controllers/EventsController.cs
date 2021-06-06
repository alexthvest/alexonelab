﻿using System;
using System.Text;
using System.Threading.Tasks;
using AlexOneLab.Events.Managers;
using AlexOneLab.Events.Models;
using AlexOneLab.Events.Repositories;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models;
using Replikit.Core.Controllers;
using Replikit.Core.Controllers.Patterns;
using Replikit.Extensions.Presentation;

namespace AlexOneLab.Events.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventRepository _eventRepository;
        private readonly CountdownRepository _countdownRepository;
        private readonly CountdownManager _countdownManager;

        public EventsController(EventRepository eventRepository, CountdownRepository countdownRepository, 
            CountdownManager countdownManager)
        {
            _eventRepository = eventRepository;
            _countdownRepository = countdownRepository;
            _countdownManager = countdownManager;
        }

        [Command("events countdown start")]
        public async Task<OutMessage?> StartCountdownToEvent(long id)
        {
            var @event = await _eventRepository.Find(e => e.Id == id);
            var countdown = await _countdownRepository.Find(c => c.Event.Id == id && c.MessageId.ChannelId == Event.Channel.Id);

            if (@event is null)
            {
                return OutMessage.FromCode("Event not found");
            }

            if (countdown is not null)
            {
                await _countdownManager.StopAsync(countdown, Event.MessageCollection);
            }

            countdown = await _countdownManager.CreateAsync(@event, Event.MessageCollection);
            _countdownManager.Start(countdown, Event.MessageCollection);

            return null;
        }
        
        [Command("events countdown stop")]
        public async Task<OutMessage> StopCountdownToEvent(long id)
        {
            var countdown = await _countdownRepository.Find(c => c.Event.Id == id && c.MessageId.ChannelId == Event.Channel.Id);

            if (countdown is null)
            {
                return OutMessage.FromCode("No countdown for this event was found");
            }

            await _countdownManager.StopAsync(countdown, Event.MessageCollection);
            
            return OutMessage.FromCode("Countdown stopped");
        }

        [Command("events rm")]
        public async Task<OutMessage> RemoveEventFromList(long id)
        {
            var @event = await _eventRepository.Find(e => e.Id == id);

            if (@event is null)
            {
                return OutMessage.FromCode("Event not found");
            }

            await _eventRepository.Delete(@event);
            return OutMessage.FromCode("Event removed from scheduled");
        }

        [Command("events add")]
        public async Task<OutMessage> AddEventToList(string name, DateTimeOffset dateTime)
        {
            if (DateTimeOffset.UtcNow > dateTime.UtcDateTime)
            {
                return OutMessage.FromCode("Invalid date");
            }
            
            var @event = new Event(name, dateTime);
            await _eventRepository.Save(@event);
            
            return OutMessage.FromCode("New event scheduled");
        }

        [Command("events")]
        public async Task<OutMessage> GetEventsList(TimeSpan? offset = default)
        {
            var events = await _eventRepository.FindAllAndSortBy(e => e.DateTime);

            OutMessage RenderEvent(Event @event)
            {
                var builder = new StringBuilder();
                var dateTime = @event.DateTime.ToOffset(offset.HasValue && offset != TimeSpan.Zero 
                    ? offset.Value 
                    : TimeZoneInfo.Local.BaseUtcOffset);

                builder.AppendLine($"[{@event.Id}] {@event.Name}");
                builder.AppendLine($"  Start: {dateTime}");

                return OutMessage.FromCode(builder);
            }
            
            return new MessageBuilder()
                .AddList(events, RenderEvent, "No upcoming events");
        }
    }
}
