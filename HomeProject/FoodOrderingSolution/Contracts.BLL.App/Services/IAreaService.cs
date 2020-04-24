using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IAreaService : IAreaRepository<Guid, Area>
    {
        // TODO: add custom methods
    }
}