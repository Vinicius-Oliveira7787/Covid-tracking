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
        private readonly IAuthService _jwtService;

        public CovidApiController(ICountriesService service, IAuthService authenticationService)
        {
            _countriesService = service;
            _jwtService = authenticationService;
        }

        [HttpGet("country")]
        public IActionResult GetSingleCountryByName(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;
            
            try
            {
                var data = _countriesService.GetCountry(request.CountryName);
                if (data == null) throw new Exception("Country Not Founded");

                return Ok(data);
            }
            
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            try
            {
                var data = _countriesService.GetAll();
                return Ok(data);
            }

            catch (System.Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpGet("countries/percentage")]
        public IActionResult GetPercentageDiference()
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            try
            {
                return Ok(_countriesService.PercentageDiference());
            }

            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;
            
            try
            {
                _countriesService.Delete(request.CountryName);
                return Ok(new { message = "success" });
            }
            
            catch (System.Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public IActionResult Update(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            var updateVerification = _countriesService.Update(request.CountryName);
            
            if (updateVerification.isValid)
            {
                return Ok(new { message = updateVerification.message});
            }

            return BadRequest(new { message = updateVerification.message});
        }

        [HttpPost("country")]
        public IActionResult Create(CountryNameRequest request)
        {
            var response = ValidateToken();
            if (!response.isValid) return response.statusCode;

            var data = _countriesService.Create(request.CountryName);
            if (data.isValid)
            {
                return Created("", new { message = data.message});
            }

            return BadRequest(new {errors = data.message});
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
