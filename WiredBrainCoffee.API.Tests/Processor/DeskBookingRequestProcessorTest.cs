namespace WiredBrainCoffee.API.Tests.Processor
{
    using Moq;
    using Shouldly;
    using System;
    using System.Collections.Generic;
    using WiredBrainCoffee.API.Constants;
    using WiredBrainCoffee.API.Domain;
    using WiredBrainCoffee.API.Processor;
    using WiredBrainCoffee.API.Repositories;
    using Xunit;

    public class DeskBookingRequestProcessorTest
    {
        private readonly DeskBookingRequestProcessor processor;
        private readonly Mock<IDeskRepository> deskRepositoryMock;
        private readonly Mock<IDeskBookingRepository> deskBookingRepositoryMock;

        public DeskBookingRequestProcessorTest()
        {
            this.deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
            this.deskRepositoryMock = new Mock<IDeskRepository>();
            this.processor = new DeskBookingRequestProcessor(
                this.deskBookingRepositoryMock.Object, this.deskRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnTheDeskBookingResultWithRequestedValues()
        {
            var request = new DeskBookingRequest()
            {
                FirstName = "Pratik",
                LastName = "Nandagawali",
                Email = "pratik.nandagawali@test.com",
                Date = new DateTime(2021, 03, 28),
            };

            var result = this.processor.BookDesk(request);

            result.ShouldNotBeNull();
            result.FirstName.ShouldBeEquivalentTo(request.FirstName);
            result.LastName.ShouldBeEquivalentTo(request.LastName);
            result.Email.ShouldBeEquivalentTo(request.Email);
            result.Date.ShouldBeEquivalentTo(request.Date);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfRequestIsNull()
        {
            var exception = Should.Throw<ArgumentNullException>(() => this.processor.BookDesk(null));

            exception.ParamName.ShouldBeEquivalentTo("request");
        }

        [Fact]
        public void ShouldSaveBookDesk()
        {
            var availableDesks = new List<Desk>
            {
                new Desk
                {
                    Id = 1,
                },
                new Desk
                {
                    Id = 2,
                },
            };

            var request = new DeskBookingRequest()
            {
                FirstName = "Pratik",
                LastName = "Nandagawali",
                Email = "pratik.nandagawali@test.com",
                Date = new DateTime(2021, 03, 28),
            };

            DeskBooking savedDeskBooking = null;
            this.deskBookingRepositoryMock.Setup(x=> x.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(
                deskBooking => 
                {
                    savedDeskBooking = deskBooking;
                });
            this.deskRepositoryMock.Setup(
               x => x.GetAvailableDesks(request.Date)).Returns(availableDesks);

            this.processor.BookDesk(request);

            this.deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Once);
            savedDeskBooking.ShouldNotBeNull();
            savedDeskBooking.FirstName.ShouldBeEquivalentTo(request.FirstName);
            savedDeskBooking.LastName.ShouldBeEquivalentTo(request.LastName);
            savedDeskBooking.Email.ShouldBeEquivalentTo(request.Email);
            savedDeskBooking.Date.ShouldBeEquivalentTo(request.Date);
        }

        [Fact]
        public void ShouldNotSaveDeskBookingIfNotAvailable()
        {
            var request = new DeskBookingRequest()
            {
                FirstName = "Pratik",
                LastName = "Nandagawali",
                Email = "pratik.nandagawali@test.com",
                Date = new DateTime(2021, 03, 28),
            };

            var availableDesks = new List<Desk>
            {
                new Desk
                {
                    Id = 1,
                },
                new Desk
                {
                    Id = 2,
                },
            };

            availableDesks.Clear();

            this.deskRepositoryMock.Setup(
                x=>x.GetAvailableDesks(request.Date)).Returns(availableDesks);

            this.processor.BookDesk(request);

            this.deskBookingRepositoryMock.Verify(x => x.Save(It.IsAny<DeskBooking>()), Times.Never);
        }

        [Fact]
        public void ShouldReturnNoDeskAvailableIfAllDeskAreBooked()
        {
            var request = new DeskBookingRequest()
            {
                FirstName = "Pratik",
                LastName = "Nandagawali",
                Email = "pratik.nandagawali@test.com",
                Date = new DateTime(2021, 03, 28),
            };

            var availableDesks = new List<Desk>
            {
                new Desk
                {
                    Id = 1,
                },
                new Desk
                {
                    Id = 2,
                },
            };

            availableDesks.Clear();

            this.deskRepositoryMock.Setup(
               x => x.GetAvailableDesks(request.Date)).Returns(availableDesks);

            var result = this.processor.BookDesk(request);

            result.ShouldNotBeNull();
            result.ResultCode.ShouldBeEquivalentTo(DeskBookingResultCode.NoDeskAvailable);
        }
    }
}
