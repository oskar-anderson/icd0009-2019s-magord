using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface ICampaignService : ICampaignRepository<Guid, Campaign>
    {
        // TODO: add custom methods
    }
}