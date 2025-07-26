using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.UserManagement.Entities
{
    public class Admin : Entity
    {

        public Guid AccountId { get; private set; }

        public Label Address { get; private set; }

        public Countries Country { get; private set; }

        public Label Application { get; private set; }

        public DateTime Date { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Admin() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Admin(Label address, Countries country, Label application) 
            : base(Guid.NewGuid()) 
        {
            Address = address;
            Country = country;
            Application = application;
            Date = DateTime.UtcNow;
        }

        public static Result<Admin> Create(string address, Countries? country, 
            string? applicationUrl)
        {

            var addressResult = Label.Create(address, "Address", 128);

            if (addressResult.IsFailure)
                return addressResult.Error;

            if (country == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                     Title: "Null Country",
                                      Description: "No country was found.");

            if (!Enum.IsDefined(typeof(Countries), country))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Invalid Country",
                                    Description: $"The country with title: {country} is invalid.");

            if (applicationUrl == null)
            {
                return new Error(
                      Code: StatusCodes.Status400BadRequest,
                      Title: $"Null Application",
                      Description: $"Application is required."
                      );
            }

            var applicationResult = Label.Create(applicationUrl, "Writer Application", 60);

            if (applicationResult.IsFailure)
                return applicationResult.Error;

            return new Admin(addressResult.Value, (Countries)country,
                applicationResult.Value);

        }

    }
}
