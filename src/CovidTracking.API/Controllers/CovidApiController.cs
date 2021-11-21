using CovidTracking.Services.Countries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CovidTracking.Controllers.CovidApi
{
    [ApiController]
    [Route("[controller]")]
    public class CovidApiController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CovidApiController(ICountriesService service)
        {
            _countriesService = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("country")]
        public IActionResult GetSingleCountryByName(string countryName)
        {
            var data = _countriesService.GetCountry(countryName) 
                ?? throw new Exception("Country Not Founded");

            return Ok(data);
        }

        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            return Ok(_countriesService.GetAll());
        }

        [HttpGet("countries/percentage")]
        public IActionResult GetPercentageDiference()
        {
            return Ok(_countriesService.PercentageDiference());
        }

        [HttpDelete("delete")]
        public IActionResult Delete(string countryName)
        {
            _countriesService.Delete(countryName);
            return Ok(new { message = "success" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(string countryName)
        {
            await _countriesService.UpdateAsync(countryName);

            return Ok(new { message = "Success" });
        }

        [HttpPost("country")]
        public async Task<IActionResult> CreateAsync(string countryName)
        {
            await _countriesService.SaveCountryByNameAsync(countryName);

            return Created("", new { message = "Success" });
        }
    }
}
