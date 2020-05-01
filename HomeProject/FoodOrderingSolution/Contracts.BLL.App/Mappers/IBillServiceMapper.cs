using Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IBillServiceMapper : IBaseMapper<DALAppDTO.Bill, BLLAppDTO.Bill>
    {
        
    }
}