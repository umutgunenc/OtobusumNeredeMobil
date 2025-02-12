using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;

namespace OtobusumNerede.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HatRotaController : ControllerBase
    {
        private readonly IHatRotaServices _hatRotaServices;

        public HatRotaController(IHatRotaServices hatRotaServices)
        {
            this._hatRotaServices = hatRotaServices;
        }


        [HttpGet("UpdateRotalar")]
        public async Task UpdateRotalarAsync()
        {
            await _hatRotaServices.UpdateHatRotalariAsync();
        }
    }
}
