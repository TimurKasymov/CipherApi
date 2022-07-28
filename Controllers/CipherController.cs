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
                     _logger.LogInformation("Encription started..");
                     var result = await CipherManager.EncryptStringAsync(value, _cipherSettings.Value.Key);
                     _logger.LogInformation("Encription finished..");
                     return Ok(result);
              }

              [HttpGet("decrypt")]
              public async Task<IActionResult> DecryptString(string value)
              {
                     _logger.LogInformation("Decryption started..");
                     var result = await CipherManager.DecryptAsync(value, _cipherSettings.Value.Key);
                     _logger.LogInformation("Decryption finished..");
                     return Ok(result);
              }
       }
}