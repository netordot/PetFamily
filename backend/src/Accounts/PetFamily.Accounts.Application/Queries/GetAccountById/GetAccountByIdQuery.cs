﻿using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Queries.GetAccountById
{
    public record GetAccountByIdQuery(Guid AcocuntId) : IQuery;
    
}
