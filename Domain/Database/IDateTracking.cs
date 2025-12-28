namespace PressedCoins.Domain.Database;

public interface IDateTracking : IDateCreated, IDateModified;

public interface IDateCreated
{
    public DateTime DateCreated { get; }
}

public interface IDateModified
{
    public DateTime DateModified { get; }
}