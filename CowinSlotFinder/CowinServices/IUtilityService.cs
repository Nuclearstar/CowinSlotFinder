using System;
namespace CowinSlotFinder.CowinServices
{
    public interface IUtilityService
    {
        public void SendSMS(string messageBody, string phoneNumber);

        public void SendEmail(string messageBody, string email);
    }
}
