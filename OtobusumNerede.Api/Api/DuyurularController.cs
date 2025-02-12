using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DuyurularController : ControllerBase
    {

        private readonly IIettDuyurularServices _iettDuyurularServices;

        public DuyurularController(IIettDuyurularServices iettDuyurularServices)
        {
            _iettDuyurularServices = iettDuyurularServices;
        }

        [HttpGet("GetAllDuyurular")]
        public async Task<List<DuyurularDto>> GetAllDuyurularAsync()
        {
            return await _iettDuyurularServices.GetDuyurularAsync();
        }

    }
}
