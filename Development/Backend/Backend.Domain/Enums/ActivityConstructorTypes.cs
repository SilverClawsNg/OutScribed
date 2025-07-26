namespace Backend.Domain.Enums
{
    public enum ActivityConstructorTypes
    {

        None,

        //Account
        Created_Account,
        Signed_In,
        Changed_Password,
        Reset_Password,
        Assigned_Role,
        Removed_Role,
        Update_Profile,
        Submitted_Application,
        Added_Contact,
        Updated_Contact,
        FollowBy_Account,
        Following_Account,
        UnFollowing_Account,

        //Tale
        Create_Tale,
        Update_Tale_Basic,
        Update_Tale_Summary,
        Update_Tale_Details,
        Update_Tale_Photo,
        Update_Tale_Country,
        Update_Tale_Tag,
        Tale_Submitted,
        Tale_Published,
        Tale_Status_Updated,
        Rated_Tale,
        Flagged_Tale,
        Commented_Tale,
        CommentedTo_Tale,
        Following_Tale,
        Unfollowing_Tale,
        Rated_Comment_Tale,
        Flagged_Comment_Tale,
        Responded_Tale,
        RespondedTo_Tale,

        //Threads
        Create_Thread,
        Update_Thread_Basic,
        Update_Thread_Summary,
        Update_Thread_Details,
        Update_Thread_Country,
        Update_Thread_Tag,
        Update_Thread_Photo,
        Thread_Published,
        Add_Thread_Addendum,
        Rated_Thread,
        Flagged_Thread,
        Commented_Thread,
        CommentedTo_Thread,
        Following_Thread,
        Unfollowing_Thread,
        Rated_Comment_Thread,
        Flagged_Comment_Thread,
        Responded_Thread,
        RespondedTo_Thread,

        //Watchlist
        Following_Watchlist,
        Unfollowing_Watchlist

    }
}
