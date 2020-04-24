namespace Contracts.BLL.Base.Mappers
{
    public interface IBaseBLLMapper<TInObject, TOutObject>
        where TOutObject : class, new()
        where TInObject : class, new()
    {
        TOutObject Map(TInObject inObject);

        TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject)
            where TMapOutObject : class, new()
            where TMapInObject : class, new();

    }
    
    
}