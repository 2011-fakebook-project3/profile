using System;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    class Repository : IRepository
    {
        public Task CreateProfileAsync(DomainProfile profileData)
        {
            throw new NotImplementedException();
        }

        public async Task<DomainProfile> GetProfileAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfileAsync(string email, DomainProfile domainProfileData)
        {
            throw new NotImplementedException();
        }
    }
}
