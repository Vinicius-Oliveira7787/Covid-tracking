using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Infra;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // var teste2 = new Repository();
            var teste2 = new Teste().testeAsync();
            System.Console.WriteLine(teste2.Result);
            return Ok();
        }
    }
}
