namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleTags
{
    public class UpdateTaleTagsRequest
    {

        public Guid Id { get; set; }

        public string Tags { get; set; } = null!;

    }
}
