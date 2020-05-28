﻿using System;
using BLL.App.DTO;
using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPriceService : IBaseEntityService<Price>, IPriceRepositoryCustom<PriceView>
    {
        // TODO: add custom methods
    }
}