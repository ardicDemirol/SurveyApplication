namespace SurveyApplication.Entities;

public abstract class Entity : IEquatable<Entity>
{
    public int Id { get; init; }

    protected Entity(int id) => Id = id;

    public bool Equals(Entity? other)
    {
        if (other is null) return false;

        if (other.GetType() != GetType()) return false;

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        if (obj is not Entity entity) return false;

        return entity.Id == Id;
    }


    public static bool operator ==(Entity? first, Entity second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity first, Entity second) => !(first == second);

    public override int GetHashCode()
    {
        return Id.GetHashCode() * 41;
    }
}
