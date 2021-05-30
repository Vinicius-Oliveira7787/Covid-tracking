using System;
using Domain.Authentication;
using Domain.Countries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.CovidApi
{
    [ApiController]
    [Route("[controller]")]
    public class CovidApiController : ControllerBase
    {
        private static ICountriesService _countriesService { get; set; }
        private readonly AuthService _jwtService;

        public CovidApiController(ICountriesService service, AuthService authenticationService)
        {
            _countriesService = service;
            _jwtService = authenticationService;
        }

        [HttpGet("country")]
        public IActionResult GetSingleCountryByName(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;
            
            var data = _countriesService.GetCountryByName(request.CountryName);
            if (data != null) return Ok(data);

            return NotFound(new { message = "Country Not Founded" });
        }

        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            var data = _countriesService.GetAll();
            return Ok(data);
        }

        [HttpGet("countries/percentage")]
        public IActionResult GetPercentageDiference()
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;
            
            var data = _countriesService.PercentageDiference();
            return Ok(data);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            var wasDeleted = _countriesService.Delete(request.CountryName);
            if (wasDeleted)
            {
                return Ok(new { message = "success" });
            }

            return NotFound(new { message = "Country Not Founded" });
        }

        [HttpPut("update")]
        public IActionResult Update(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            var wasUpdated = _countriesService.Update(request.CountryName);
            if (wasUpdated)
            {
                return Ok(new { message = "success"});
            }

            return NotFound(new { message = "Country Not Founded"});
        }

        [HttpPost("country")]
        public IActionResult Create(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            var data = _countriesService.Create(request.CountryName);
            if (data.IsValid)
            {
                return Created("", new { message = "success"});
            }

            return BadRequest(new {errors = data.Errors});
        }

        private (IActionResult statusCode, bool isValid) ValidateToken()
        {
            var userId = "";
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);
                userId = token.Issuer;

                if (userId != "1ccc377c-6090-42d7-a74f-8f3cd49357a2")
                {
                    return (Unauthorized(), false);
                }
            }
            
            catch (Exception)
            {
                return (Unauthorized(), false);
            }

            return (Ok(), true);
        }
    }
}
