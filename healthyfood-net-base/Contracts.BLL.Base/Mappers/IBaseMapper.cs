namespace ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers
{
    public interface IBaseMapper<TLeftObject, TRightObject> : global::ee.itcollege.magord.healthyfood.Contracts.DAL.Base.Mappers.IBaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class?, new()
        where TRightObject : class?, new()
    {
        
    }
}