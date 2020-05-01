using System.Security.Cryptography;
using Contracts.BLL.Base.Mappers;

namespace BLL.Base.Mappers
{
    public class IdentityMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.IdentityMapper<TLeftObject, TRightObject>
        where TRightObject : class, new() 
        where TLeftObject : class, new()
    {
        
    }
}