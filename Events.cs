using FinalProject.Classes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace FinalProject
{
    public class Events : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetEvents()
        {
            // Fetch events from the database
            List<Schedule> events = FetchEventsFromDatabase();

            // Serialize events to JSON
            string jsonEvents = JsonConvert.SerializeObject(events);

            return Ok(jsonEvents);
        }

        private List<Schedule> FetchEventsFromDatabase()
        {
            // Implement code to fetch events from the database using your Schedule class
            // Example code:
            List<Schedule> events = new List<Schedule>();
            // Fetch events from the database and add them to the events list
            return events;
        }
    }
}
