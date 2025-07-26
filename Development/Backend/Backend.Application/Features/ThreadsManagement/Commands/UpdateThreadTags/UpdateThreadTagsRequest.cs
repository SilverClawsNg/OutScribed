namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadTags
{
    public class UpdateThreadTagsRequest
    {

        public Guid Id { get; set; }

        public string Tags { get; set; } = null!;

    }
}
