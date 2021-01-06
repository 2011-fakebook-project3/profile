using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;

namespace Fakebook.Profile.Domain
{
    public class Repository : IRepository
    {
        private readonly ProfileDbContext _context;

        public Repository(ProfileDbContext context)
        {
            _context = context;
        }

        public async Task CreateProfileAsync(DomainProfile profileData)
        {
            // nothing needs to be returned, at the moment
        }

        public async Task<IEnumerable<DomainProfile>> GetAllProfilesAsync()
        {
            // return a new list in place of actual results
            return new List<DomainProfile>();
        }

        public async Task<DomainProfile> GetProfileAsync(string email)
        {
            // return something, instead of throwing an exception
            return default;
        }

        public async Task UpdateProfileAsync(string email, DomainProfile domainProfileData)
        {
            // nothing can be done here, at the moment, since the method doesn't return anything (from the programmer)
        }
    }
}
