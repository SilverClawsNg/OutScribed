namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadDetails
{
    public class UpdateThreadDetailsRequest
    {

        public Guid Id { get; set; }

        public string Details { get; set; } = null!;

    }
}
