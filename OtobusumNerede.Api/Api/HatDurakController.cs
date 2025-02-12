using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;

namespace OtobusumNerede.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HatDurakController : ControllerBase
    {
        private readonly IHatDurakServices _hatDurakServices;

        public HatDurakController(IHatDurakServices hatDurakServices)
        {
            _hatDurakServices = hatDurakServices;
        }

        [HttpGet("GetHatDurak/{hatKodu}")]
        public async Task<List<HatDurakDto>> GetHatDurakByIdAsync(string hatKodu)
        {
            var resault = await _hatDurakServices.HattinDuraklariAsync(hatKodu);
            return resault;
        }

    }
}
