using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.BlobModel;
using MongoDB.Bson;

namespace Fakebook.Profile.DataAccess.Services
{
    /// <summary>
    /// This class is to 
    /// </summary>
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// Get a Specirfic Profile
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<BlobProfile> GetProfileAsync(string email)

        {
            //get bson from blob service

            //load the bson document.
            BsonDocument bson = new BsonDocument();

            //????? treat the bson document as a mongodb?

            //extract data into collection of blobProfiles

            throw new NotImplementedException();

            //return the blob profiles
        }

        /// <summary>
        /// Get all the profiles.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<BlobProfile>> GetProfiles()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new NotImplementedException();
        }
    }
}
