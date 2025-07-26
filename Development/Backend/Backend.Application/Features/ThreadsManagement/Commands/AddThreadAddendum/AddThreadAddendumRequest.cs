namespace Backend.Application.Features.ThreadsManagement.Commands.AddThreadAddendum
{
    public class AddThreadAddendumRequest
    {

        public Guid Id { get; set; }

        public string Details { get; set; } = null!;

    }
}
