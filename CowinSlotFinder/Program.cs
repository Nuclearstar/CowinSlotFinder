using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CowinSlotFinder.CowinServices;

namespace CowinSlotFinder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("twiliosettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

                var cowinService = new ServiceCollection()
                    .AddSingleton<IDataService, DataService>()
                    .AddSingleton<IUtilityService, UtilityService>()
                    .AddSingleton<INotifcationService, NotifcationService>()
                    .AddSingleton(configuration)
                    .BuildServiceProvider();

                var notifcationService = cowinService.GetService<INotifcationService>();
                await notifcationService.SlotFinderByPincode();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
