using PropertyAgencyMobileApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace PropertyAgencyMobileApp.Services
{
    public class EventDataStore : IDataStore<Event>
    {
        readonly WebClient client;
        readonly DataContractJsonSerializer serializer;
        readonly string baseUrl = "http://10.0.2.2:12345/";
        readonly int agentId = 1;

        public EventDataStore()
        {
            client = new WebClient();
            serializer = new DataContractJsonSerializer(typeof(EventCollection));
        }

        public async Task<bool> AddItemAsync(Event @event)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Event @event)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(string id)
        {
            return await Task.FromResult(new Event());
        }

        public async Task<IEnumerable<Event>> GetItemsAsync
            (bool forceRefresh = false)
        {
            long currentUnixDate = ((DateTimeOffset)DateTime.Now.Date)
                                        .ToUnixTimeSeconds();
            long tomorrowUnixDate = ((DateTimeOffset)DateTime.Now.Date)
                                        .AddDays(1)
                                        .ToUnixTimeSeconds();

            byte[] response =
                await client
                .DownloadDataTaskAsync($"{baseUrl}/events"
                                       + $"?agent_id={agentId}"
                                       + $"&from={currentUnixDate}"
                                       + $"&to={tomorrowUnixDate}");
            Event[] events = ((EventCollection)serializer.ReadObject(new MemoryStream(response))).Events;
            return await Task.FromResult(events);
        }
    }
}