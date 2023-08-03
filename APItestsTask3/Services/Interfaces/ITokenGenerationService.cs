namespace APItestsTask3.Services.Interfaces
{
    public interface ITokenGenerationService
    {
        public string GetAccessToken(string user = "admin@test");
    }
}
