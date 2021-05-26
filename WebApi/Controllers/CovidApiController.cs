using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ApiConnection.Consumers;
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

        [HttpGet]
        public IActionResult Get()
        {
            // var teste2 = new Repository();
            // var teste2 = new Consumer().FilterDataToPutInDb();
            var teste2 = new Consumer().HandleDataToPutInDb();
            // System.Console.WriteLine(teste2);
            return Ok(teste2[0]);
        }

        [HttpPost]
        public IActionResult GetCountryById(GetCountryByNameUsingTheApiRequest request)
        {
            var consumer = new Consumer();
            var data = consumer.GetSingleCountry(request.CountryName);
            // var teste = _countriesService.Create();
            return Ok(data);
        }
    }
}
