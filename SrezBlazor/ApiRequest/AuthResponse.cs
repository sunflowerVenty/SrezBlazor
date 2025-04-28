using SrezBlazor.ApiRequest.Model;

namespace CpCinemaBlazor.ApiRequest.Model
{
    public class AuthResponse
    {
        public bool Status { get; set; }
        public string Token { get; set; }

    }

    public static class SingletoneUser
    {
        public static UserProd up;


    }
}
