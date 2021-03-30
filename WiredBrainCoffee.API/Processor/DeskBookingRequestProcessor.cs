namespace WiredBrainCoffee.API.Processor
{
    using System;
    using WiredBrainCoffee.API.Repositories;
    using WiredBrainCoffee.API.Domain;
    using System.Linq;
    using WiredBrainCoffee.API.Constants;

    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository deskBookingRepository;
        private readonly IDeskRepository deskRepository;

        public DeskBookingRequestProcessor(
            IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
        {
            this.deskBookingRepository = deskBookingRepository;
            this.deskRepository = deskRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var deskBookingResult = this.Create<DeskBookingResult>(request);
            var availableDesks = this.deskRepository.GetAvailableDesks(request.Date);

            if (availableDesks.Any())
            {
                deskBookingResult.ResultCode = DeskBookingResultCode.Success;
                this.deskBookingRepository.Save(this.Create<DeskBooking>(request));
            }
           
            return deskBookingResult;
        }

        public T Create<T>(DeskBookingRequest request) where T : DeskBookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date,
            };
        }
    }
}