using System;
using System.Threading.Tasks;

namespace CowinSlotFinder.CowinServices
{
    public interface INotifcationService
    {
        public Task SlotFinderByPincode();
    }
}
