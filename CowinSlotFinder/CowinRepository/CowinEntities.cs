using System;
using System.Collections.Generic;

namespace CowinSlotFinder.CowinRepository
{
    public class CowinEntities
    {
		public class SlotDetails
        {
			public string date { get; set; }
			public string center_name { get; set; }
			public int available_slots { get; set; }
			public int pincode { get; set; }
		}

        public class Center
        {
            public int center_id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string state_name { get; set; }
            public string district_name { get; set; }
            public string block_name { get; set; }
            public int pincode { get; set; }
            public int lat { get; set; }
            public int @long { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string fee_type { get; set; }
            public List<Session> sessions { get; set; }
        }

        public class NotificationData
        {
            public string phone { get; set; }
            public List<int> pincodes { get; set; }
            public string email { get; set; }
        }

        public class MasterData
        {
            public List<NotificationData> notificationData { get; set; }
        }

        public class SessionsCalendarEntity
        {
            public List<Center> centers { get; set; }
        }

        public class Session
        {
            public string session_id { get; set; }
            public string date { get; set; }
            public int available_capacity { get; set; }
            public int min_age_limit { get; set; }
            public string vaccine { get; set; }
            public List<string> slots { get; set; }
        }
    }
}
