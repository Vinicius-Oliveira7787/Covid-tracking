using System;
using CovidTracking.Services.Authentication;
using CovidTracking.Services.Countries;
using Microsoft.AspNetCore.Mvc;

namespace CovidTracking.Controllers.CovidApi
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
            try
            {
                ValidateToken();

                var data = _countriesService.GetCountry(request.CountryName);
                if (data == null) throw new Exception("Country Not Founded");

                return Ok(data);
            }
            
            catch (System.Exception ex)
            {
                return BadRequest(new {Error = HandleException(ex)});
            }
        }

        [HttpGet("countries")]
        public IActionResult GetAllCountries()
        {
            try
            {
                ValidateToken();
                return Ok(_countriesService.GetAll());
            }

            catch (System.Exception ex)
            {
                return BadRequest(new {Error = HandleException(ex)});
            }
        }

        [HttpGet("countries/percentage")]
        public IActionResult GetPercentageDiference()
        {
            try
            {
                ValidateToken();
                return Ok(_countriesService.PercentageDiference());
            }

            catch (System.Exception ex)
            {
                return BadRequest(new {Error = HandleException(ex)});
            }
        }

        [HttpDelete("delete")]
        public IActionResult Delete(CountryNameRequest request)
        {
            try
            {
                ValidateToken();
                
                _countriesService.Delete(request.CountryName);
                return Ok(new { message = "success" });
            }

            catch (Exception ex)
            {
                return BadRequest(new {Error = HandleException(ex)});
            }
        }

        [HttpPut("update")]
        public IActionResult Update(CountryNameRequest request)
        {
            try
            {
                ValidateToken();

                _countriesService.Update(request.CountryName);
                
                return Ok(new { message = "Success"});
            }

            catch (System.Exception ex)
            {
                return BadRequest(new {Error = HandleException(ex)});
            }
        }

        [HttpPost("country")]
        public IActionResult Create(CountryNameRequest request)
        {
            try
            {
                ValidateToken();
    
                _countriesService.Create(request.CountryName);

                return Created("", new { message = "Success"});
            }

            catch (Exception ex)
            {
                return BadRequest(new {Error = HandleException(ex)});
            }
        }

        private string HandleException(Exception ex)
        {
            // TODO: Verificar se o message == inner exception
            return ex.Message.Contains("See the inner exception for details")
                ? ex.InnerException.Message
                : ex.Message;
        }
        
        private void ValidateToken()
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);

            var userId = token.Issuer;

            if (userId != "1ccc377c-6090-42d7-a74f-8f3cd49357a2")
            {
                throw new Exception("Unauthorized");
            }
        }
    }
}
