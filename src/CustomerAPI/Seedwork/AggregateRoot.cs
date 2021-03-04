namespace CustomerAPI.Seedwork
{
    public abstract class AggregateRoot : Entity
    {
        public AggregateRoot()
        {
        }

        public AggregateRoot(int id) : base(id)
        {
        }
    }
}
