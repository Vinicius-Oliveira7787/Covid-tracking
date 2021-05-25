using System;
using System.Collections.Generic;
using System.Linq;
using Domain.ApiConnection.Consumers;
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
            var teste2 = new Consumer().FilterData();
            // System.Console.WriteLine(teste2);
            return Ok(teste2);
        }
    }
}
