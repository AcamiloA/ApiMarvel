namespace Marvel.Application.Security
{
    public class OAuth2SuccessResponse : IOAuth2Response
    {
        public string Access_token { get; set; } = string.Empty;
        public string Token_type { get; set; } = string.Empty;
        public string User_id { get; set; } = string.Empty;
        public string Device_id { get; set; } = "000000000000000000000000";
        public int Expires_in { get; set; }
        public string? Refresh_token { get; set; }
        public string? Scope { get; set; }
    }
}
