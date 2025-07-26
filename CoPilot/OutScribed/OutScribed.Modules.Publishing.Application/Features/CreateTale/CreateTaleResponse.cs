using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.CreateTale
{
    public record CreateTaleResponse(Guid Id, DateTime CreatedAt, string Title,
        TaleStatus Status, Category Category)
    {
        public Ulid Id { get; set; } = Id;

        public DateTime CreatedAt { get; set; } = CreatedAt;

        public TaleStatus Status { get; set; } = Status;

        public string Title { get; set; } = Title;

        public Category Category { get; set; } = Category;
    }
}
