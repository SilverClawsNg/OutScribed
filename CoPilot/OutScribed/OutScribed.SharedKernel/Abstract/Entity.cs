namespace OutScribed.SharedKernel.Abstract
{
    public abstract class Entity
    {

        public Ulid Id { get; protected set; }

        protected Entity() => Id = Ulid.NewUlid();

        protected Entity(Ulid id) => Id = id;

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other) return false;
            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

    }
}
