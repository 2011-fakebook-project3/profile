using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Fakebook.Profile.DataAccess.EntityModel;

using Microsoft.EntityFrameworkCore;

namespace Fakebook.Profile.Domain
{
    /// <summary>
    /// Repository for profile storage.
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileDbContext _context;
        public ProfileRepository(ProfileDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Model mapping from entity to domain.
        /// </summary>
        /// <param name="profile">entity model object used</param>
        /// <exception type="ArgumentNullException">If the profile, the profile's email, or the profile's names are null,
        /// this will be thrown </exception>
        /// <returns>A representation of the profile as a Domain Profile.</returns>
        private static DomainProfile ToDomainProfile(EntityProfile profile)
        {
            if (profile == null || profile.Email == null || profile.FirstName == null || profile.LastName == null)
            {
                throw new ArgumentNullException("Must have a entity profile, with an email.");
            }

            DomainProfile convertedProfile = new(profile.Email, profile.FirstName, profile.LastName)
            {
                ProfilePictureUrl = profile.ProfilePictureUrl,
                PhoneNumber = profile.PhoneNumber,
                BirthDate = profile.BirthDate,
                Status = profile.Status
            };

            return convertedProfile;
        }

        /// <summary>
        /// Default model mapping from domain to entity.
        /// </summary>
        /// <param name="profile">domain model object used</param>
        /// <exception type="ArgumentNullException">If the profile, the profile's email, or the profile's names are null,
        /// this will be thrown </exception>
        /// <returns>A representation of the profile as a DB Entity.</returns>
        private static EntityProfile ToEntityProfile(DomainProfile profile)
        {
            if (profile == null || profile.Email == null || profile.FirstName == null || profile.LastName == null)
            {
                throw new ArgumentNullException("Must have a domain profile, with an email and first and last name.");
            }

            EntityProfile convertedProfile = new()
            {
                Email = profile.Email,
                ProfilePictureUrl = profile.ProfilePictureUrl,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                PhoneNumber = profile.PhoneNumber,
                BirthDate = profile.BirthDate,
                Status = profile.Status
            };

            return convertedProfile;
        }

        /// <summary>
        /// Get all users' profiles at once.
        /// </summary>
        /// <returns>A list of all profiles from the database.</returns>
        public async Task<IEnumerable<DomainProfile>> GetAllProfilesAsync()
        {
            var entity = await _context.EntityProfiles
                .ToListAsync();

            // model mapping
            var users = entity.Select(e => ToDomainProfile(e));
            return users;
        }

        /// <summary>
        /// Get one specific user profile using his email
        /// </summary>
        /// <param name="email">email used to find the user</param>
        /// <returns>A specific profile with the matching email.</returns>
        public async Task<DomainProfile> GetProfileAsync(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email), "Cannot get a null email from DB.");
            }

            var entities = _context.EntityProfiles;

            if (!entities.Any())
            {
                throw new ArgumentException("Source is empty", nameof(entities));
            }

            var profile = await _context.EntityProfiles
                .FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());

            if (profile == null)
            {
                throw new ArgumentNullException(nameof(email), "Email not found");
            }

            // model mapping
            var user = ToDomainProfile(profile);
            return user;
        }

        /// <summary>
        /// Get a group of user profiles using their emails
        /// </summary>
        /// <param name="emails">a collection of emails used</param>
        /// <returns>A collection of domain profiles matching the emails provided.</returns>
        public async Task<IEnumerable<DomainProfile>> GetProfilesByEmailAsync(IEnumerable<string> emails)
        {
            var userEntities = _context.EntityProfiles;
            var userEmails = userEntities
                .Select(u => u.Email.ToUpper())
                .ToList();

            if (!emails.All(e => userEmails.Contains(e.ToUpper())))
            {
                throw new ArgumentException("Not all emails requested are present.", nameof(emails));
            }

            var users = await userEntities
                .ToListAsync();

            if (!emails.Any() || !users.Any())
            {
                return new List<DomainProfile>();
            }

            return users
                .Where(u => emails.Contains(u.Email))
                .Select(u => ToDomainProfile(u))
                .ToList();
        }

        /// <summary>
        /// Take in a domain profile and create an entity profile
        /// </summary>
        /// <param name="profileData">domain profile used</param>
        public async Task CreateProfileAsync(DomainProfile profileData)
        {

            // have to have this try catch block to prevent errors from data base
            try
            {
                var newUser = ToEntityProfile(profileData); // convert
                await _context.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new ArgumentException("Failed to create a profile");
            }
        }

        /// <summary>
        /// Updates a user's profile
        /// </summary>
        /// <param name="email">The orignional email of the profile, incase it was chaanged</param>
        /// <param name="domainProfileData">The data for the domain profile to be set to.</param>
        public async Task UpdateProfileAsync(string email, DomainProfile domainProfileData)
        {
            // have to have this try catch block to prevent errors from data base
            try
            {
                var userEntity = ToEntityProfile(domainProfileData);

                var entities = _context.EntityProfiles;
                if (!entities.Any())
                {
                    throw new ArgumentException("Source is empty", nameof(entities));
                }

                var profile = await _context.EntityProfiles
                    .FirstOrDefaultAsync(x => x.Email.ToUpper() == email.ToUpper());

                if (profile == null)
                {
                    throw new ArgumentNullException(nameof(email), "Email not found");
                }

                // assign all the values
                profile.Email = userEntity.Email;
                profile.ProfilePictureUrl = userEntity.ProfilePictureUrl;
                profile.FirstName = userEntity.FirstName;
                profile.LastName = userEntity.LastName;
                profile.PhoneNumber = userEntity.PhoneNumber;
                profile.BirthDate = userEntity.BirthDate;
                profile.Status = userEntity.Status;
                // save changes.
                _context.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                // the user's email is not found
                throw;
            }
            catch
            {
                throw new ArgumentException("Failed to update a profile", nameof(domainProfileData));
            }
        }
    }
}
