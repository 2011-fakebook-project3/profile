using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    class Repository : IRepository
    {
        public async Task<DomainProfile> GetProfileAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
