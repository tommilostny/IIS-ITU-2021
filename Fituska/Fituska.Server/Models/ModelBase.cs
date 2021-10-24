namespace Fituska.Server.Models
{
    public abstract record ModelBase(Guid Id) : IModel
    {
        protected ModelBase() : this(Guid.Empty) { }
    }
}
