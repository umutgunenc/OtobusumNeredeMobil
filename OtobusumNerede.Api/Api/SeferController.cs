using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;

namespace OtobusumNerede.Api.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeferController : ControllerBase
    {
        private readonly ISeferServices _serferServices;

        public SeferController(ISeferServices serferServices)
        {
            _serferServices = serferServices;
        }

        [HttpGet("GetSeferSaatleriByHatId/{hatKodu}/{gun}")]
        public async Task<SeferListeleriDto> GetAllSeferSaatleriByHatId(string hatKodu, GunlerEnum gun)
        {
            var resault= await _serferServices.GidisDonusSeferleriniListele(hatKodu, gun);
            return resault;
        }


    }
}
