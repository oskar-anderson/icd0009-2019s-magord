using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class BillServiceMapper : BaseMapper<DALAppDTO.Bill, BLLAppDTO.Bill>, IBillServiceMapper
    {
        
    }
}