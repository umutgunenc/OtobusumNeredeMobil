using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using AutoMapper;
using DuyurularServices;
using OtobusumNerede.Api.Data.Entities;
using OtobusumNerede.Api.Data.ServicesModels;
using OtobusumNerede.Shared.DTOs;
using OtobusumNerede.Shared.Enums;

namespace OtobusumNerede.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetPlanlananSeferSaati_jsonServicesModel, SeferSaatiDto>()
                .ForMember(dest => dest.HatKodu, opt => opt.MapFrom(src => src.SHATKODU))
                .ForMember(dest => dest.HatAdi, opt => opt.MapFrom(src => src.SHATKODU + "  -  " + src.HATADI))
                .ForMember(dest => dest.SeferSaati, opt => opt.MapFrom(src => src.DT))
                .ForMember(dest => dest.GunTipi, opt => opt.MapFrom(src => src.SGUNTIPI))
                .ForMember(dest => dest.GuzergahKodu, opt => opt.MapFrom(src => src.SGUZERAH))
                .AfterMap((src, dest) =>
                {
                    var duraklar = src.HATADI.Split(new[] { '-' });

                    if (src.SYON == "G") // Gidiş
                    {
                        dest.BaslangıcDuragi = duraklar[0].Trim();
                        dest.SeferYonu = SeferYonu.Gidis;

                    }
                    else if (src.SYON == "D") // Dönüş
                    {
                        dest.BaslangıcDuragi = duraklar[1].Trim();
                        dest.SeferYonu = SeferYonu.Donus;
                    }

                });

            CreateMap<Hat, HatDto>()
                .ForMember(dest => dest.HatKoduAdi, opt => opt.MapFrom(src => src.Id + " - " + src.HatAdi))
                .ForMember(dest => dest.HatKodu, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HatAdi, opt => opt.MapFrom(src => src.HatAdi));

            CreateMap<Table, HatDurakDto>()
                .ForMember(dest => dest.HatKodu, opt => opt.MapFrom(src => src.HatKodu))
                .ForMember(dest => dest.GidisYonAdi, opt => opt.MapFrom(src => src.YonAdi))
                .ForMember(dest => dest.DurakKodu, opt => opt.MapFrom(src => src.DurakKodu))
                .ForMember(dest => dest.DurakAdi, opt => opt.MapFrom(src => src.DurakAdi))
                .ForMember(dest => dest.Enlem, opt => opt.MapFrom(src => src.YKoordinati))
                .ForMember(dest => dest.Boylam, opt => opt.MapFrom(src => src.XKoordinati))
                .ForMember(dest => dest.SiraNo, opt => opt.MapFrom(src => src.SiraNo))
                .AfterMap((src, dest) =>
                {
                    if (src.Yon == "D")
                        dest.SeferYonu = SeferYonu.Donus;
                    else
                        dest.SeferYonu = SeferYonu.Gidis;

                });

            //CreateMap<GetHatOtoKonumJsonResultServiceModel, HatOtobusDto>()
            //    .ForMember(dest => dest.HatKodu, src => src.MapFrom(src => src.hatkodu))
            //    .ForMember(dest => dest.Boylam, src => src.MapFrom(src => src.boylam))
            //    .ForMember(dest => dest.Enlem, src => src.MapFrom(src => src.enlem))
            //    .ForMember(dest => dest.KapiNo, src => src.MapFrom(src => src.kapino))
            //    .ForMember(dest => dest.YonAdi, src => src.MapFrom(src => src.yon))
            //    .ForMember(dest => dest.GuzergahKodu, src => src.MapFrom(src => src.guzergahkodu))
            //    .ForMember(dest => dest.KonumZamani, src => src.MapFrom(src => DateTime.Parse(src.son_konum_zamani)));

            CreateMap<DuraklarJsonModel, DuyurularDto>()
                .ForMember(dest => dest.DuyuruBasligi, src => src.MapFrom(src => src.HATKODU + " / " + src.HAT))
                .ForMember(dest => dest.DuyuruSaati, src => src.MapFrom(src => src.GUNCELLEME_SAATI))
                .ForMember(dest => dest.DuyuruTipi, src => src.MapFrom(src => src.TIP))
                .ForMember(dest => dest.DuyuruMesaji, src => src.MapFrom(src => src.MESAJ))
                .AfterMap((src, dest) =>
                {
                    string silinecekKisim = src.GUNCELLEME_SAATI.Split(':').First().Trim();
                    dest.DuyuruSaati = src.GUNCELLEME_SAATI.Remove(0, silinecekKisim.Length + 1);
                });


        }
    }
}
