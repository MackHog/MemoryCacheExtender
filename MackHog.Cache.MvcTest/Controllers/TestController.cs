using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MackHog.Cache.Core;

namespace MackHog.Cache.MvcTest.Controllers
{
    [Route("api/[controller]")]

    public class TestController: Controller
    {
        private readonly ICache _cache;
        public TestController(ICache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Running()
        {
            CacheManager.Cache.Add(new CacheEntity("hello", "world"));
            _cache.Add(new CacheEntity("goodbye", "moon"));
            return Ok("Running..");
        }

        [HttpGet("dip")]
        public IActionResult Dip()
        {
            var items = _cache.GetAll();
            return Ok(items);
        }

        [HttpGet("static")]
        public IActionResult Static()
        {
            var items = CacheManager.Cache.GetAll();
            return Ok(items);
        }
    }
}
