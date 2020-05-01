using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PersonService : BaseEntityService<IAppUnitOfWork, IPersonRepository, IPersonServiceMapper, DAL.App.DTO.Person,
        BLL.App.DTO.Person>, IPersonService
    {
        public PersonService(IAppUnitOfWork uow) : base(uow, uow.Persons, new PersonServiceMapper())
        {
        }
    }
}