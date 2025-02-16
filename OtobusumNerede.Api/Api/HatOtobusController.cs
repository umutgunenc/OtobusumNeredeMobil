using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Api.Services;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.UIServices.ServicesDataModel;

namespace OtobusumNerede.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HatOtobusController : ControllerBase
    {
        private readonly IHatOtobusServices _hatOtobusServices;

        public HatOtobusController(IHatOtobusServices hatOtobusServices)
        {
            _hatOtobusServices = hatOtobusServices;
        }

        //[HttpPost("GetHatOtobus/{hatKodu}")]
        //public async Task<List<HatOtobusDto>> GetHatDurakByIdAsync(string hatKodu, HttpClient httpClient)
        //{
        //    List<HatOtobusDto> hatOtobusDtoListesi = await _hatOtobusServices.HatOtobusBilgileriAsync(hatKodu);
        //    return hatOtobusDtoListesi;
        //}

        [HttpGet("GetHatGeoJson")]
        public async Task<IActionResult> GetHatGeoJsonAsync([FromQuery] List<string> guzergahKodlari)
        {
            return Ok(await _hatOtobusServices.HatGeoJsonBilgileriAsync(guzergahKodlari));
        }

        [HttpPost("GetHatOtobus")]
        public IActionResult GetHatOtobusDtoAsync([FromBody] List<GetHatOtoKonumJsonResultServiceModel> model)
        {
            return Ok( _hatOtobusServices.HatOtobusBilgileri(model));
        }

        

    }
}
