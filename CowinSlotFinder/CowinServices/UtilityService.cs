using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace CowinSlotFinder.CowinServices
{
    public class UtilityService : IUtilityService
    {
        private readonly IConfigurationRoot _config;
        public UtilityService(IConfigurationRoot config)
        {
            _config = config;
        }

        public void SendEmail(string messageBody, string email)
        {
            try
            {
                StringBuilder emailMessageBody = new StringBuilder();
                emailMessageBody.Append(messageBody);
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("testemail@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Covid slots are available - book immediately";
                    mail.Body = emailMessageBody.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("testemail@gmail.com", "testpassword");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }                        
                }

                Console.WriteLine("Email sent \n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending the email " + ex.Message);
                throw ex;
            }
        }

        public void SendSMS(string messageBody, string phoneNumber)
        {
            string accountSid = _config.GetSection("TWILIO_ACCOUNT_SID").Value;
            string authToken = _config.GetSection("TWILIO_AUTH_TOKEN").Value;
            string fromPhoneNumber = _config.GetSection("TWILIO_PHONE_NUMBER").Value;

            try
            {
                TwilioClient.Init(accountSid, authToken);
                var smsMessage = MessageResource.Create(
                body: messageBody,
                from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                to: new Twilio.Types.PhoneNumber($"+91{ phoneNumber }")
                );

                if (smsMessage.Sid != null)
                    Console.WriteLine($"SMS sent to + 91{ phoneNumber }");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in sending the SMS " + ex.Message);
                throw ex;
            }            
        }
    }
}
