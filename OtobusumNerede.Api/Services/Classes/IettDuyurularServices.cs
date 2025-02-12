using System.Text.Json;
using AutoMapper;
using DuyurularServices;
using Microsoft.AspNetCore.Mvc;
using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services.Classes
{
    public class IettDuyurularServices : IIettDuyurularServices
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private List<DuyurularDto> _duyurularListesi;

        public IettDuyurularServices(IMapper mapper, HttpClient httpClient, List<DuyurularDto> duyurularListesi)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _duyurularListesi = duyurularListesi;
        }

        public async Task<List<DuyurularDto>> GetDuyurularAsync()
        {

            DuyurularSoapClient duyurularSoapClient = new DuyurularSoapClient();
            var response = await duyurularSoapClient.GetDuyurular_jsonAsync();
            string jsonResult = response.Body.GetDuyurular_jsonResult;

            List<DuraklarJsonModel> duyurularJsonList = JsonSerializer.Deserialize<List<DuraklarJsonModel>>(jsonResult);

            _duyurularListesi = _mapper.Map<List<DuyurularDto>>(duyurularJsonList);

            return _duyurularListesi;
        }

    }
}
