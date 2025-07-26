using OutScribed.Client.Enums;

namespace OutScribed.Client.Models
{
    public class ActivitySummary
    {
        public ActivityTypes Type { get; set; }

        public ActivityConstructorTypes ConstructorType { get; set; }

        public DateTime Date { get; set; }

        public string DateToString => Date.ToLocalTime().ToString("hh:mm:ss tt, dddd, dd MMMM yyyy");

        public string Details { get; set; } = default!;

        public List<string>? DetailsParameters => [.. Details.Split('*')];

        public string SimpleText =>


    //Account
    ConstructorType == ActivityConstructorTypes.Signed_In ? "You signed into your account"
    : ConstructorType == ActivityConstructorTypes.Created_Account ? "You successfully created a new account"
    : ConstructorType == ActivityConstructorTypes.Changed_Password ? "You changed your password"
    : ConstructorType == ActivityConstructorTypes.Reset_Password ? "You reset your password"
    : ConstructorType == ActivityConstructorTypes.Assigned_Role ? $"Your teams' role was updated to {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Removed_Role ? "You were stripped of all teams' roles"
    : ConstructorType == ActivityConstructorTypes.Update_Profile ? "You updated your profile"
    : ConstructorType == ActivityConstructorTypes.Submitted_Application ? "You submitted an application to join our writing team"
    : ConstructorType == ActivityConstructorTypes.Added_Contact ? $"You added a new {DetailsParameters![0]} contact detail"
    : ConstructorType == ActivityConstructorTypes.Updated_Contact ? $"You updated your {DetailsParameters![0]} contact detail"

    //Tale
    : ConstructorType == ActivityConstructorTypes.Create_Tale ? $"You created a new tale: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Tale_Basic ? $"You updated the basic details of your tale: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Tale_Summary ? $"You updated the summary of your tale: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Tale_Country ? $"You updated the target country of your tale: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Tale_Details ? $"You updated the main details of your tale: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Tale_Photo ? $"You updated the principal photo of your tale: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Tale_Tag ? $"You updated the tags of your tale: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Tale_Submitted ? $"You submitted your tale: {DetailsParameters![0]} for publication"
    : ConstructorType == ActivityConstructorTypes.Tale_Status_Updated ? $"Your tale: {DetailsParameters![0]} was updated with the status: <i>{DetailsParameters![1]}</i>"

    //Thread
    : ConstructorType == ActivityConstructorTypes.Update_Thread_Basic ? $"You updated the basic details of your thread: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Thread_Summary ? $"You updated the summary of your thread: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Thread_Country ? $"You updated the target country of your thread: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Thread_Details ? $"You updated the main details of your thread: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Thread_Tag ? $"You updated the tags of your thread: {DetailsParameters![0]}"
    : ConstructorType == ActivityConstructorTypes.Update_Thread_Photo ? $"You updated the principal photo of your thread: {DetailsParameters![0]}"

     //Watchlist
     : ConstructorType == ActivityConstructorTypes.Following_Watchlist ? $"You followed the watchlist: <a href = \"https://localhost:7092/watchlist/{DetailsParameters![0]}\" target=\"_blank\">{DetailsParameters![1]}</a> "
    : ConstructorType == ActivityConstructorTypes.Unfollowing_Watchlist ? $"You unfollowed the thread: <a href = \"https://localhost:7092/watchlist/{DetailsParameters![0]}\" target=\"_blank\">{DetailsParameters![1]}</a>"

    : string.Empty;

    }
}
