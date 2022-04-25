using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PictureStore.Common;

namespace PictureStore.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IOptionsMonitor<PictureStoreConfiguration> _config;

        public PicturesController(
            IOptionsMonitor<PictureStoreConfiguration> config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("image/{id}")]
        public IActionResult Get(string id) 
        {
            var file = Path.Combine(
                _config.CurrentValue.BaseImageDirectory,
                id);
            var image = System.IO.File.OpenRead(file);
            return File(image, "image/jpeg");
        }
    }
}
