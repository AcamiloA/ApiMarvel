namespace Marvel.Application.Security
{
    public class OAuth2SuccessResponse : IOAuth2Response
    {
        public bool IsSucceeded { get; set; }
        public string Access_token { get; set; } = string.Empty;
        public string Token_type { get; set; } = string.Empty;
        public int Expires_in { get; set; }
        public string? Refresh_token { get; set; }
    }
}
