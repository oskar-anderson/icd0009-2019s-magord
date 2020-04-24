using Contracts.DAL.Base.Mappers;

namespace DAL.Base.EF.Mappers
{
    public class IdentityMapper<TInObject, TOutObject> : IBaseDALMapper<TInObject, TOutObject>
        where TInObject : class, new() 
        where TOutObject : class, new()
    {
        public TOutObject Map(TInObject inObject)
        {
            return inObject as TOutObject ?? default!;
        }

        public TMapOutObject Map<TMapInObject, TMapOutObject>(TMapInObject inObject) where TMapInObject : class where TMapOutObject : class, new()
        {
            return inObject as TMapOutObject ?? default!;
        }
    }
}