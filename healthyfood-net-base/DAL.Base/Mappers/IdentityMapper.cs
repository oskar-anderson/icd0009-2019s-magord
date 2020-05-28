using ee.itcollege.magord.healthyfood.Contracts.DAL.Base.Mappers;

namespace ee.itcollege.magord.healthyfood.DAL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : IBaseMapper<TLeftObject, TRightObject> 
        where TRightObject : class?, new() 
        where TLeftObject : class?, new()
    {
        public TRightObject Map(TLeftObject inObject)
        {
            return inObject as TRightObject ?? default!;
        }

        public TLeftObject Map(TRightObject inObject)
        {
            return inObject as TLeftObject ?? default!;
        }
    }
}