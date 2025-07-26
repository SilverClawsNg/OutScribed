namespace Backend.Application.Features.TalesManagement.Commands.FollowTale
{
    public class FollowTaleRequest
    {
        public Guid TaleId { get; set; }

        public bool Option { get; set; }

    }
}
