
namespace OtobusumNerede.Api.Data.Entities
{
    public class Durak
    {
        public int Id { get; set; }
        public int DurakKodu { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public string Adi { get; set; }
        public bool EngelliKullaniminaUygunMu { get; set; }
        public bool AkilliMi { get; set; }
        public string IlceAdi { get; set; }


    }
}
