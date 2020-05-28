﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base;
using DAL.App.DTO;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{

    public interface IPaymentTypeRepository : IBaseRepository<PaymentType>, IPaymentTypeRepositoryCustom
    {
        
    }

    public interface IPaymentTypeRepositoryCustom
    {
        
    }
    
}