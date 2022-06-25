namespace MobileStore.Authentication.Domain.Service
{
    public interface ITokenBuilder
    {
        string BuildToken(string email);
    }
}
