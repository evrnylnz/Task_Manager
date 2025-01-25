namespace Task_Manager.Infrastructure
{
    public class TokenResponseDto
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public TokenResponseDto(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
    }
}
