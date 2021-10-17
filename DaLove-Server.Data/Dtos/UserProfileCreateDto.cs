namespace DaLove_Server.Data.Dtos
{
    public record UserProfileCreateDto
    {
        public string DisplayName { get; set; }

        public string UniqueUserName { get; set; }
    }
}
