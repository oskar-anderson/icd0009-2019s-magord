using Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IPaymentServiceMapper : IBaseMapper<DALAppDTO.Payment, BLLAppDTO.Payment>
    {
        
    }
}