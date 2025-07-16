using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private IConfiguration configuration;
        public TrackerController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

    }
}
