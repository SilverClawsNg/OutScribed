using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;

namespace Backend.Domain.Models.UserManagement.Entities
{
    public class Activity : Entity
    {

        public Guid AccountId { get; set; }

        public DateTime ActiveDate { get; set; }

        public Label Details { get; set; }

        public ActivityTypes Type { get; set; }

        public ActivityConstructorTypes ConstructorType { get; set; }

        public bool HasRead { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Activity() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        private Activity(Label details, ActivityTypes type, 
            ActivityConstructorTypes constructorType, DateTime date)
            : base(Guid.NewGuid())
        {
            ActiveDate = date;
            Details = details;
            HasRead = false;
            Type = type;
            ConstructorType = constructorType;
        }

        /// <summary>
        /// Creates a new activity
        /// </summary>
        /// <param name="details"></param>
        /// <param name="type"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Result<Activity> Create(string details, ActivityTypes type,
             ActivityConstructorTypes constructorType, DateTime date)
        {

            var detailsResult = Label.Create(details, "Activity Details", 256);

            if (detailsResult.IsFailure)
                return detailsResult.Error;

            return new Activity(detailsResult.Value, type,
                constructorType, date);
        }

        /// <summary>
        /// Marks an activity as read
        /// </summary>
        public Result<bool> MarkAsRead()
        {
            HasRead = true;

            return true;
        }

    }

}
