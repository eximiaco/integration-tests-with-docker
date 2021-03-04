using System;

namespace CustomerAPI.Seedwork
{
    public abstract class Entity
    {
        public Entity()
        {
        }

        public Entity(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is int id)
            {
                return Id == id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
