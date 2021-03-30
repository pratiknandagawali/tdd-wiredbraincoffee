namespace WiredBrainCoffee.API.Repositories
{
    using WiredBrainCoffee.API.Domain;

    public interface IDeskBookingRepository
    {
        void Save(DeskBooking deskBooking);
    }
}
