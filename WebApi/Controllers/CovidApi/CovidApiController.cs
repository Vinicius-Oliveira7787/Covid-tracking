using Domain.Countries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.CovidApi
{
    [ApiController]
    [Route("[controller]")]
    public class CovidApiController : ControllerBase
    {
        private static ICountriesService _countriesService { get; set; }

        public CovidApiController(ICountriesService service)
        {
            _countriesService = service;
        }

        [HttpGet("country")]
        public IActionResult GetSingleCountryByName(CountryNameRequest request)
        {
            var data = _countriesService.GetCountryByName(request.CountryName);
            if (data != null)
            {
                return Ok(data);
            }

            return NotFound("Country Not Founded");
        }

        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            var data = _countriesService.GetAll();
            return Ok(data);
        }

        [HttpGet("countries/percentage")]
        public IActionResult GetPercentageDiference()
        {
            var data = _countriesService.PercentageDiference();
            return Ok(data);
        }

        [HttpDelete]
        public IActionResult Delete(CountryNameRequest request)
        {
            var wasDeleted = _countriesService.Delete(request.CountryName);
            if (wasDeleted)
            {
                return Ok();
            }

            return NotFound("Country Not Founded");
        }

        [HttpPut]
        public IActionResult Update(CountryNameRequest request)
        {
            var wasUpdated = _countriesService.Update(request.CountryName);
            if (wasUpdated)
            {
                return Ok();
            }

            return NotFound("Country Not Founded");
        }

        [HttpPost]
        public IActionResult Create(CountryNameRequest request)
        {
            var data = _countriesService.Create(request.CountryName);
            if (data.IsValid)
            {
                return Ok();
            }

            return BadRequest(data.Errors);
        }
    }
}
