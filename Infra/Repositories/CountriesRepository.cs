using Domain.Countries;

namespace Infra.Repositories
{
    public class CountriesRepository : Repository<Country>, ICountriesRepository
    {
        public CountriesRepository(CovidContext brasileiraoContext) : base(brasileiraoContext) {}
    }
}