using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;

namespace Fakebook.Profile.Domain
{
    public class Repository : IRepository
    {
        private ProfileDbContext _context;

        public Repository(ProfileDbContext context) {
            throw new NotImplementedException();
        }

        public async Task CreateProfileAsync(DomainProfile profileData) {
            throw new NotImplementedException();

        }

        public Task<IEnumerable<DomainProfile>> GetAllProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<DomainProfile> GetProfileAsync(string email) 
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProfileAsync(string email, DomainProfile domainProfileData) 
        {
            throw new NotImplementedException();
        }
    }
}
