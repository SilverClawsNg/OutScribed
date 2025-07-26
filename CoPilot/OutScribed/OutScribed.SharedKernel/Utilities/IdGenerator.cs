namespace OutScribed.SharedKernel.Utilities
{
  
    public static class IdGenerator
    {
        public static Guid NewGuidId() // Still keep Guid for compatibility or if preferred
        {
            return Guid.NewGuid();
        }

        public static Ulid Generate() // New method for Ulid
        {
            return Ulid.NewUlid();
        }

        public static Guid GenerateAsGuid() // If you want a Guid type but with Ulid's sortability
        {
            return Ulid.NewUlid().ToGuid(); // Converts Ulid to Guid (ensures sortable Guid)
        }
    }
}
