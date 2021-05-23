# CowinSlotFinder

## Abstract:

- Windows Software application to fetch the available slots that can be used to book the appointment for COVID-19 Vaccination, Co-WIN using the [APISetu APIs](https://apisetu.gov.in/public/marketplace/api/cowin/cowin-public-v2#/)from Govt Of India.

- This is a .NET Core console app which sends SMS & Email notification when vaccination slots are available for pincodes using Twilio SMS integration & SMTP client for mail notifications.

- This can be easily integrated with Windows Task Scheduler or Azure to send SMS & Email notification once slots are available at the requested pincodes.

## Installation Steps & Usage:

- To use this we require free twilio account to get Twilio API key, Sid and phone number. 
- Just replace them in the twiliosettings.json.
- Then enter your phone number & mail id along with the pincodes for which you are looking for a vaccination slot in notificationData.Json file. 
- Once these details are filled correctly, you can use the dll to schedule in windows task scheduler and just wait and relax till you get notified of slots.

## Dependencies:

Clean & rebuild the project once you install the following packages using Nuget Package Manager:
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Configuration
- Microsoft.Extensions.Configuration.EnvironmentVariables
- Microsoft.Extensions.Configuration.FileExtensions
- Microsoft.Extensions.Configuration.Json
- Newtonsoft.Json
- Twilio

.NET Core 3.1 Runtime is supported & Use of Visual Studio 2019 is recommended for the purpose of development.

### References and Credits:

This implementation took inspiration from Amit Sharma's (amitagt007) work.

### Disclaimer

Please do not abuse this API endpoint as it is something very critical. Avoid sending requests to the API Endpoint every few minutes. 
