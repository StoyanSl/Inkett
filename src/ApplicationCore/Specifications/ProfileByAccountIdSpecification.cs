using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Specifications
{
    public class ProfileByAccountIdSpecification:BaseSpecification<Profile>
    {
        public ProfileByAccountIdSpecification(string accountId):base(p=>p.AccountId==accountId)
        {

        }
    }
}
