using OutScribed.Client.Models;

namespace OutScribed.Client.Services.Interfaces
{
    public interface ISelectServices
    {
        List<DropdownList> Get<T>();

        List<DropdownList> Get<T>(string selected);
    }
}
