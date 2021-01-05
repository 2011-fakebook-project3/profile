using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Profile.Domain;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    public class Repository : IRepository
    {
        private ProfileDbContext _context;

        public Repository(ProfileDbContext context)
        {
            throw new NotImplementedException();
        }



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
