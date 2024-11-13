namespace Alza.Core.BusinessReuslt;

public static class BusinessResultHelper
{
    public static EntityNotFoundMessage EntityNotFound<T>(string parameters)
    {
        return new EntityNotFoundMessage(typeof(T), parameters);
    }
}
