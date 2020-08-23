using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class TestController : ApiController
    {
        // GET api/test
        public string Get()
        {
            int a = 0;
            int b = 1 / a;
            return "value";
        }

      
    }
}
