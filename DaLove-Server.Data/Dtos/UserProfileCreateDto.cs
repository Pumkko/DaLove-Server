namespace DaLove_Server.Data.Dtos
{
    public record UserProfileCreateDto
    {
        public string DisplayName { get; init; }

        public string UniqueUserName { get; init; }
    }
}
