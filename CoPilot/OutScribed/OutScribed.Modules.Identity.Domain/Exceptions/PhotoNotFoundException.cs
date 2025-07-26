namespace OutScribed.Modules.Identity.Domain.Exceptions
{
   
    public class PhotoNotFoundException : Exception
    {
     
        public PhotoNotFoundException() : base("Photo is required to create a profile.") { }

        public PhotoNotFoundException(string message) : base(message) { }

        public PhotoNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }

}
