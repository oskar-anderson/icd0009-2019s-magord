using Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IContactServiceMapper : IBaseMapper<DALAppDTO.Contact, BLLAppDTO.Contact>
    {
        BLLAppDTO.ContactView MapContactView(DALAppDTO.ContactView inObject);
    }
}