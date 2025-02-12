using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HatController : ControllerBase
    {
        private readonly IHatServices _hatServices;

        public HatController(IHatServices hatServices)
        {
            _hatServices = hatServices;
        }

        [HttpGet("GetAllHatId")]
        public async Task<List<HatDto>> GetAllHatDtoAsync()
        {
            return await _hatServices.GetAllHatDtoAsync();
        }


    }
}
