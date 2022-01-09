using PropertyAgencyMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace PropertyAgencyMobileApp.Services
{
    public class EventDataStore : IDataStore<Event>
    {
        private readonly DataContractJsonSerializer serializer;
        private readonly string baseUrl = "http://10.0.2.2:12345";
        private readonly int agentId = 1;

        public EventDataStore()
        {
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
            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("agent_id", agentId.ToString());
                client.QueryString.Add("event_uuid", id);
                string method = "DELETE";
                _ = await client
                    .UploadValuesTaskAsync($"{baseUrl}/event",
                                           method,
                                           new NameValueCollection());
                return await Task.FromResult(true);
            }
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

            using (WebClient client = new WebClient())
            {
                client.QueryString.Add("agent_id", agentId.ToString());
                client.QueryString.Add("from", currentUnixDate.ToString());
                client.QueryString.Add("to", tomorrowUnixDate.ToString());
                byte[] response =
                    await client
                    .DownloadDataTaskAsync($"{baseUrl}/events");
                Event[] events = ((EventCollection)serializer
                    .ReadObject(new MemoryStream(response))).Events;
                return await Task.FromResult(events);
            }
        }
    }
}