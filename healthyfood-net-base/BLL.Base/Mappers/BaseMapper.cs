namespace ee.itcollege.magord.healthyfood.BLL.Base.Mappers
{
    public class BaseMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class, new()
        where TRightObject : class, new()
    {
    }
}