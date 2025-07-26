namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleText
{
    public record UpdateTaleTextResponse(string Text)
    {
        public string Text { get; set; } = Text;

    }
}
