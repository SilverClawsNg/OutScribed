namespace Backend.Domain.Exceptions
{
    public record Error(int Code, string Title, string Description)
    {

        public int Code { get; set; } = Code;

        public string Title { get; set; } = Title;

        public string Description { get; set; } = Description;
    }

}
