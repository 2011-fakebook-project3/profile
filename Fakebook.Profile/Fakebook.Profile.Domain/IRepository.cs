using Fakebook.Profile.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    /// <summary>
    /// Interface for data retrieval repository.
    /// </summary>
    public interface IRepository
    {
        Task CreateProfileAsync(DomainProfile profileData);
        Task<DomainProfile> GetProfileAsync(string email);
        Task<IEnumerable<DomainProfile>> GetAllProfilesAsync();
        Task UpdateProfileAsync(string email, DomainProfile domainProfileData);
    }
}

