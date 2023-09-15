namespace API.Dtos
{
    public class AuthorizedUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string ImgUrl { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}