using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class AssignAdminRequest
    {

        public Guid AccountId { get; set; }

        [EnumDataType(typeof(RoleTypes), ErrorMessage = "Enter a valid role")]
        public RoleTypes Role { get; set; }

    }
}
