namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsightPhoto
{
    public class UpdateInsightPhotoResponse(string PhotoUrl)
    {
        public string PhotoUrl { get; set; } = PhotoUrl;

    }
}
