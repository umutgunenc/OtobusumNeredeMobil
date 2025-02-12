namespace OtobusumNerede.Api.Services.Interfaces
{

    public interface IUpdateDatabase
    {
        public Task UpdateHatlarAsync();

        public Task UpdateDuraklarAsync();
    }

}
