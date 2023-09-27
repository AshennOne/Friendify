namespace API.Dtos
{
    public class FollowDto
    {
        public int Id { get; set; }

        public string FollowerId { get; set; }
        public string FollowedId { get; set; }
        public DateTime FollowDate { get; set; }
    }
}