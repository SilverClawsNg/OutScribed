namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleDetails
{
    public class UpdateTaleDetailsRequest
    {

        public Guid Id { get; set; }

        public string Details { get; set; } = null!;

    }
}
