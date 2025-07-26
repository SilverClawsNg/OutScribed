using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class LinkTaleRequest
    {

        public Guid WatchListId { get; set; }

        [Required(ErrorMessage = "Select a tale")]
        public Guid TaleId { get; set; }

    }
}
