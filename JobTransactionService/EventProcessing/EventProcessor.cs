using System;
using System.Text.Json;
using AutoMapper;
using JobTransactionService.Data;
using JobTransactionService.Dtos;
using JobTransactionService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace JobTransactionService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.JobTransactionPublished:
                    AddJobTransaction(message);
                    break;
                default:
                    AddJobTransaction(message);
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericUpdateNoteDto>(notificationMessage);

            switch(eventType.UpdateNote)
            {
                case "GET Request New Job":
                    Console.WriteLine("--> Job Status is New Event Detected");
                    return EventType.JobTransactionPublished;
                default:
                    Console.WriteLine("--> Could not determine Job Status Event Type");
                    return EventType.Undetermined;
            }
        }

        private void AddJobTransaction(string jobTransactionPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IJobTransactionRepo>();

                var jobTransactionPublishedDto = JsonSerializer.Deserialize<JobTransactionPublishedDto>(jobTransactionPublishedMessage);

                try
                {
                    //Console.WriteLine($"TEST: {jobTransactionPublishedMessage}");
                    var jobTransaction = _mapper.Map<JobTransaction>(jobTransactionPublishedDto);
                    Console.WriteLine(jobTransaction.JobId + jobTransaction.JobStatus + jobTransaction.UpdateNote);

                    repo.CreateJobTransaction(jobTransaction);
                    repo.SaveChanges();
                    Console.WriteLine("--> Job Transaction added!");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not add JobTransaction to DB: {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        JobTransactionPublished,
        Undetermined
    }
}