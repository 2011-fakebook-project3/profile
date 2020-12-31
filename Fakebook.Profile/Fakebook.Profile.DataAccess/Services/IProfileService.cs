using System.Collections.Generic;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.StorageModel;

namespace Fakebook.Profile.DataAccess.Services
{
    public interface IProfileService
    {
        /// <summary>
        /// Get a Specirfic Profile
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<StorageProfile> GetProfileAsync(string email);

        /// <summary>
        /// Get all the profiles.
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<StorageProfile>> GetProfiles();
    }
}

