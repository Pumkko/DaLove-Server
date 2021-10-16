namespace DaLove_Server.Data.Dtos
{
    public record UserProfileCreateDto
    {
        public string UniqueUserName { get; init; }

        public string DisplayUserName { get; init; }
    }
}
