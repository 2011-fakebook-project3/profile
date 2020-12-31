using System.Collections.Generic;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.BlobModel;

namespace Fakebook.Profile.DataAccess.Services
{
    public interface IProfileService
    {
        /// <summary>
        /// Get a Specirfic Profile
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<BlobProfile> GetProfileAsync(string email);

        /// <summary>
        /// Get all the profiles.
        /// </summary>
        /// <returns></returns>
        public Task<ICollection<BlobProfile>> GetProfiles();
    }
}

