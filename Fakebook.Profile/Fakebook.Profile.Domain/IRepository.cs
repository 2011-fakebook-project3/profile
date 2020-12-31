using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    /// <summary>
    /// Interface for data retrieval repository.
    /// </summary>
    public interface IRepository
    {
        public Task CreateProfileAsync(DomainProfile profileData);
        public Task<DomainProfile> GetProfileAsync(string email);
        public Task UpdateProfileAsync(string email, DomainProfile domainProfileData);
    }
}
