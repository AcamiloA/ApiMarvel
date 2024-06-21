namespace Marvel.Application.Security
{
    public class OAuth2ErrorResponse : IOAuth2Response
    {
        public bool IsSucceeded { get; set; }
        public string Error { get; set; } = string.Empty;
        public string? ErrorDescription { get; set; }
        public string? ErrorUri { get; set; }
    }
}
