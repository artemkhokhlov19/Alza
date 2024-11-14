namespace Alza.Core.BusinessResult;

public class EntityNotFoundMessage
{
    private Type entityType;
    private string parameters;

    public EntityNotFoundMessage(Type entityType, string parameters)
    {
        this.entityType = entityType;
        this.parameters = parameters;
    }

    public override string ToString()
    {
        return entityType.FullName + " was not found with parameters " + parameters;
    }
}
