﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TUMI.TIC.ApiCliente.Controllers
{
    
    [Route("api/values")]
    public class ValuesController : ApiController
    {

        
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
