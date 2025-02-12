using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtobusumNerede.Api.Data;
using OtobusumNerede.Api.Data.Entities;
using OtobusumNerede.Api.Services.Classes;
using OtobusumNerede.Api.Services.Interfaces;
using OtobusumNerede.Shared.DTOs;

namespace OtobusumNerede.Api.Services.Classes
{
    public class HatServices : IHatServices
    {
        private readonly OtobusumNeredeDbContext _context;
        private readonly IMapper _mapper;
        private List<HatDto> _hatDtoListesi;

        public HatServices(OtobusumNeredeDbContext context, IMapper mapper, List<HatDto> hatDtoListesi)
        {
            _context = context;
            _mapper = mapper;
            _hatDtoListesi = hatDtoListesi;
        }

        public async Task<List<HatDto>> GetAllHatDtoAsync()
        {
            var hatlar = await _context.Hatlar.ToListAsync();
            _hatDtoListesi = _mapper.Map<List<HatDto>>(hatlar);
            return _hatDtoListesi;
        }

    }
}
