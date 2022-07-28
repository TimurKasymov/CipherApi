using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace cipher.Controllers
{
       [Route("api/cipher")]
       public class CipherController : Controller
       {
              private readonly ILogger<CipherController> _logger;
              private readonly IOptions<CipherSettings> _cipherSettings;

              public CipherController(ILogger<CipherController> logger, IOptions<CipherSettings> cipherSettings)
              {
                     _cipherSettings = cipherSettings;
                     _logger = logger;
              }
              public IActionResult GetCipheredString(string value)
              {

              }

              [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
              public IActionResult Error()
              {
                     return View("Error!");
              }
       }
}