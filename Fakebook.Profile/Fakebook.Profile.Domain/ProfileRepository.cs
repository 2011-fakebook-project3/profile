using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;

namespace Fakebook.Profile.Domain
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileDbContext _context;

        public ProfileRepository(ProfileDbContext context)
        {
            _context = context;
            throw new NotImplementedException();
        }

        public async Task CreateProfileAsync(DomainProfile profileData)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DomainProfile>> GetProfilesByEmailAsync(IEnumerable<string> emails)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DomainProfile>> GetAllProfilesAsync()
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
