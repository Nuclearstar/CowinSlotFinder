using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static CowinSlotFinder.CowinRepository.CowinEntities;

namespace CowinSlotFinder.CowinServices
{
    public class NotifcationService : INotifcationService
    {
        private readonly IDataService _dataService;
        private readonly IUtilityService _utilityService;

        public NotifcationService(IDataService dataService, IUtilityService utilityService)
        {
            _dataService = dataService;
            _utilityService = utilityService;
        }

        public async Task SlotFinderByPincode()
        {
            var userData = GetNotificationData();
            foreach (var info in userData.notificationData)
            {
                IList<int> availablePincodes = new List<int>();
                foreach (var pincode in info.pincodes)
                {
                    var eligiblePincode = await _dataService.FetchAvailablePincodes(pincode);

                    if (eligiblePincode > 0)
                        availablePincodes.Add(eligiblePincode);
                }
                _utilityService.SendSMS(formattedMessage(availablePincodes), info.phone);
                _utilityService.SendEmail(formattedMessage(availablePincodes), info.email);
            }
        }

        private string formattedMessage(IList<int> availablePincodes)
        {
            string msgHeader = "Vaccination Slots available at ";
            string pincodeMessage = "pincode ";
            string appenedMessage = string.Empty;
            var counter = 0;

            foreach (var pincode in availablePincodes)
            {
                appenedMessage = appenedMessage + " " + pincode + ",";
                counter++;
            }
            appenedMessage = appenedMessage.TrimEnd(',');

            if (counter > 1)
                pincodeMessage = "pincodes ";

            return msgHeader + pincodeMessage + appenedMessage;
        }

        private MasterData GetNotificationData()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "notificationData.json");
            var jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<MasterData>(jsonString);
        }
    }
}
