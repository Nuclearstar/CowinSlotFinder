using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static CowinSlotFinder.CowinRepository.CowinEntities;

namespace CowinSlotFinder.CowinServices
{
    public class DataService : IDataService
    {
        private IList<SlotDetails> slots;
        public async Task<int> FetchAvailablePincodes(int pincode)
        {
            var date = DateTime.Now.ToString("dd-MM-yy");
            var url = "https://cdn-api.co-vin.in/api/v2/appointment/sessions/public/calendarByPin?pincode=" + pincode + "&date=" + date + "";
            SessionsCalendarEntity listOfCenters;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();
                listOfCenters = JsonConvert.DeserializeObject<SessionsCalendarEntity>(jsonString);
            }

            if (listOfCenters != null)
                return ProcessData(listOfCenters);
            else
                return 0;
        }

        private int ProcessData(SessionsCalendarEntity listOfCenters)
        {
            slots = new List<SlotDetails>();
            foreach (var center in listOfCenters.centers)
            {
                string centerName = center.name;
                foreach (var session in center.sessions)
                {
                    if (session.available_capacity > 0)
                    {
                        var obj = new SlotDetails() { pincode = center.pincode };
                        slots.Add(obj);
                    }
                }
            }

            if (slots.Count > 0)
                return slots.Select(n => n.pincode).Distinct().ToArray()[0];
            else
                return 0;
        }
    }
}
