using System.Threading.Tasks;
using Fakebook.Profile.Domain;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    public interface IRepository
    {
        Task CreateProfileAsync(DomainProfile profileData);
        Task<DomainProfile> GetProfileAsync(string email);
        Task UpdateProfileAsync(string email, DomainProfile domainProfileData);
    }
}