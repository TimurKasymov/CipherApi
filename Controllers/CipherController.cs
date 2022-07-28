using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace cipher.Controllers
{
       [Route("api/cipher")]
       [ApiController]
       public class CipherController : ControllerBase
       {
              private readonly ILogger<CipherController> _logger;
              private readonly IOptions<CipherSettings> _cipherSettings;

              public CipherController(ILogger<CipherController> logger, IOptions<CipherSettings> cipherSettings)
              {
                     _cipherSettings = cipherSettings;
                     _logger = logger;
              }

              [HttpGet("encrypt")]
              public async Task<IActionResult> EncryptString(string value)
              {
                     var result = await CipherManager.EncryptStringAsync(value, _cipherSettings.Value.Key);
                     return Ok(result);
              }

              [HttpGet("decrypt")]
              public async Task<IActionResult> DecryptString(string value)
              {
                     var result = await CipherManager.DecryptAsync(value, _cipherSettings.Value.Key);
                     return Ok(result);
              }
       }
}