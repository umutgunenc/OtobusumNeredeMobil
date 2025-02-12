using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.UIServices;
using OtobusumNerede.Shared.UIServicesModels;

namespace OtobusumNerede.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebHatOtobusController : ControllerBase
    {
        private readonly IUI_HatOtobusServices _uiHatOtobusServices;
        public WebHatOtobusController(IUI_HatOtobusServices uiHatOtobusServices)
        {
            _uiHatOtobusServices = uiHatOtobusServices;
        }

        [HttpGet("GetHatOtobus/{hatKodu}")]
        public async Task<List<GetHatOtoKonumJsonResultServiceModel>> GetHatOtobusAsync(string hatKodu)
        {
            return await _uiHatOtobusServices.HatOtobusBilgileriAsync(hatKodu);
        }

    }
}
