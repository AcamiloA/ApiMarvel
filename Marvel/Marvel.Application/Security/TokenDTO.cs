using Marvel.Application.DTO;

namespace Marvel.Application.Security
{
    public class TokenDTO : UserDTO
    {
        public int? Id { get; set; }
        public string? Token { get; set; } = string.Empty;
        public bool IsSucceeded { get; set; }
        public IOAuth2Response Result
        {
            get
            {
                if (IsSucceeded)
                {
                    OAuth2SuccessResponse result = new()
                    {
                        Access_token = Token!,
                        Refresh_token = Token!,
                        Token_type = TokenType,
                        Expires_in = ExpiresIn,
                    };
                    return result;
                }
                else
                {
                    OAuth2ErrorResponse result = new()
                    {
                        Error = "Unauthorized",
                        ErrorDescription = "Usuario o contraseña incorrectos"
                    };
                    return result;
                }
            }
        }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = string.Empty;
    }
}
