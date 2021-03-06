using CovidTracking.API.Client.ApiConnection;
using CovidTracking.Data.Repositories;
using CovidTracking.Services.Countries;
using Moq;

namespace Tests.Mocks
{
    public abstract class MyMocks
    {
        protected Mock<ICountriesRepository> repository = new Mock<ICountriesRepository>();
        protected Mock<IConsumer> consumer { get; set; } = new Mock<IConsumer>();
        protected CountriesService countriesService;

        public MyMocks()
        {
            countriesService = new CountriesService(repository.Object, consumer.Object);
        }
    }
}