using OutScribed.Client.Enums;
using OutScribed.Client.Models;
using OutScribed.Client.Services.Interfaces;
using System;

namespace OutScribed.Client.Services.Implementations
{
    public class SelectServices : ISelectServices
    {
 
        public List<DropdownList> Get<T>()
        {

            var listItems = new List<DropdownList>();

            var enumValues = Enum.GetValues(typeof(T));

            foreach (var enumValue in enumValues)
            {
                listItems.Add(new DropdownList
                {
                    Text = enumValue.ToString()!.Replace("_", " ").Replace("8", " & "),
                    Value = (int)enumValue
                });
            }

            return listItems;

        }

        public List<DropdownList> Get<T>(string selected)
        {
            var listItems = new List<DropdownList>();

            var enumValues = Enum.GetValues(typeof(T));

            foreach (var enumValue in enumValues)
            {

                var index = enumValue.ToString()!.LastIndexOf('_');

                if(index >= 0)
                {

                    var add1 = index + 1;

                    var keyword = enumValue.ToString()![add1..];

                    if (keyword == selected)
                    {
                 
                        listItems.Add(new DropdownList
                        {
                            Text = enumValue.ToString()![..index].Replace("_", " ").Replace("8", " "),
                            Value = (int)enumValue
                        });
                    }
                }


            }

            return listItems;
        }

    }

}
