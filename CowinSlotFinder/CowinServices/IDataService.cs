using System;
using System.Threading.Tasks;

namespace CowinSlotFinder.CowinServices
{
    public interface IDataService
    {
        public Task<int> FetchAvailablePincodes(int pincode);
    }
}
