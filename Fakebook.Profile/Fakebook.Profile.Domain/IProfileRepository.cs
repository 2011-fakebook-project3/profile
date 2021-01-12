using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    /// <summary>
    /// Interface for data retrieval repository.
    /// </summary>
    public interface IProfileRepository
    {
        /// <summary>
        /// Take in a domain profile and create an entity profile.
        /// </summary>
        /// <param name="profileData">Domain profile used.</param>
        Task CreateProfileAsync(DomainProfile profileData);
        /// <summary>
        /// Get one specific user profile using their email.
        /// </summary>
        /// <param name="email">Email used to find the specific user.</param>

        Task<DomainProfile> GetProfileAsync(string email);
        /// <summary>
        /// Get a group of user profiles using their emails.
        /// </summary>
        /// <param name="emails">A collection of emails used to find users.</param>
        /// <returns>A collection of domain profiles matching the emails provided.</returns>
        Task<IEnumerable<DomainProfile>> GetProfilesByEmailAsync(IEnumerable<string> emails);
        /// <summary>
        /// Get all users' profiles at once.
        /// </summary>
        /// <returns>A list of all matching profiles from the database.</returns>
        Task<IEnumerable<DomainProfile>> GetAllProfilesAsync();
        /// <summary>
        /// Updates a user's profile with the information provided.
        /// </summary>
        /// <param name="email">The orignional email of the profile, incase it was changed.</param>
        /// <param name="domainProfileData">The data for the domain profile to be set to.</param>
        Task UpdateProfileAsync(string email, DomainProfile domainProfileData);
    }
}
