﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Profile.DataAccess.EntityModel;
using Fakebook.Profile.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Profile.DataAccess
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
        /// <exception type="ArgumentException">If the profile, the profile's email, or the profile's names are null,
        /// this will be thrown </exception>
        /// <returns>A representation of the profile as a Domain Profile.</returns>
        private static DomainProfile ToDomainProfile(EntityProfile profile)
        {
            if (profile == null || profile.Email == null || profile.FirstName == null || profile.LastName == null)
            {
                throw new ArgumentException("Profile is missing required fields.", nameof(profile));
            }

            List<string> followingEmails = new List<string>();
            List<string> followerEmails = new List<string>();

            if (profile.Following != null)
            {
                foreach (var following in profile.Following)
                {
                    followingEmails.Add(following.Following.Email);
                }
            }
            
            if (profile.Followers != null)
            {
                foreach (var follower in profile.Followers)
                {
                    followerEmails.Add(follower.User.Email);
                }
            }

            DomainProfile convertedProfile = new(profile.Email, profile.FirstName, profile.LastName)
            {
                ProfilePictureUrl = profile.ProfilePictureUrl,
                PhoneNumber = profile.PhoneNumber,
                BirthDate = profile.BirthDate,
                Status = profile.Status,
                FollowerEmails = followerEmails,
                FollowingEmails = followingEmails
            };

            return convertedProfile;
        }

        /// <summary>
        /// Default model mapping from domain to entity.
        /// </summary>
        /// <param name="profile">domain model object used</param>
        /// <exception type="ArgumentException">If the profile, the profile's email, or the profile's names are null,
        /// this will be thrown </exception>
        /// <returns>A representation of the profile as a DB Entity.</returns>
        private Task<EntityProfile> ToEntityProfileAsync(DomainProfile profile)
        {
            if (profile == null || profile.Email == null || profile.FirstName == null || profile.LastName == null)
            {
                throw new ArgumentException("Profile is missing required fields.", nameof(profile));
            }

            return ConvertAsync(profile);

            async Task<EntityProfile> ConvertAsync(DomainProfile profile)
            {

                List<Follow> followingEmails = new List<Follow>();
                List<Follow> followerEmails = new List<Follow>();

                if (profile.FollowingEmails.Count != 0)
                {
                    int userId = await GetProfileIdAsync(profile.Email);
                    var followingIds = await GetManyProfileIdsAsync(profile.FollowingEmails);
                    foreach (var following in followingIds)
                    {
                        Follow newFollow = new Follow
                        {
                            UserId = userId,
                            FollowingId = following
                        };
                        followingEmails.Add(newFollow);
                    }
                }

                if (profile.FollowerEmails.Count != 0)
                {
                    int userId = await GetProfileIdAsync(profile.Email);
                    var followerIds = await GetManyProfileIdsAsync(profile.FollowerEmails);
                    foreach (var follower in followerIds)
                    {
                        Follow newFollow = new Follow
                        {
                            UserId = follower,
                            FollowingId = userId
                        };
                        followerEmails.Add(newFollow);
                    }
                }


                EntityProfile convertedProfile = new()
                {
                    Email = profile.Email,
                    ProfilePictureUrl = profile.ProfilePictureUrl,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    PhoneNumber = profile.PhoneNumber,
                    BirthDate = profile.BirthDate,
                    Status = profile.Status,
                    Following = followingEmails,
                    Followers = followerEmails
                };

                return convertedProfile;
            }
        }

        private async Task<int> GetProfileIdAsync(string email)
        {
            var entityId = await _context.EntityProfiles.Where(x => x.Email == email).Select(x => x.Id).FirstAsync();
            return entityId;
        }

        private async Task<List<int>> GetManyProfileIdsAsync(ICollection<string> emails)
        {
            var IDs = await _context.EntityProfiles.Where(x => emails.Contains(x.Email)).Select(x => x.Id).ToListAsync();
            return IDs;
        }

        /// <summary>
        /// Get all users' profiles at once.
        /// </summary>
        /// <returns>A list of all profiles from the database.</returns>
        public async Task<IEnumerable<DomainProfile>> GetAllProfilesAsync()
        {
            var entity = await _context.EntityProfiles
                .Include(x => x.Following)
                    .ThenInclude(x => x.Following)
                .Include(x => x.Followers)
                    .ThenInclude(x => x.User)
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
        public Task<DomainProfile> GetProfileAsync(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email), "Cannot get a null email from DB.");
            }

            return GetProfileInternalAsync(email);
        }

        private async Task<DomainProfile> GetProfileInternalAsync(string email)
        {
            DbSet<EntityProfile> entities = _context.EntityProfiles;

            if (!await entities.AnyAsync())
            {
                throw new ArgumentException("Source is empty", nameof(entities));
            }

            var profile = await _context.EntityProfiles
                .Include(x => x.Following)
                    .ThenInclude(x => x.Following)
                .Include(x => x.Followers)
                    .ThenInclude(x => x.User)
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
            var userEntities = _context.EntityProfiles
                .Include(x => x.Followers)
                .Include(x => x.Following);
            var userEmails = userEntities
                .Include(x => x.Following)
                    .ThenInclude(x => x.Following)
                .Include(x => x.Followers)
                    .ThenInclude(x => x.User)
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

        public async Task<IEnumerable<DomainProfile>> GetProfilesByNameAsync(string name = null)
        {
            var query = await _context.EntityProfiles
                .Include(x => x.Following)
                    .ThenInclude(x => x.Following)
                .Include(x => x.Followers)
                    .ThenInclude(x => x.User)
                .ToListAsync();
            var profiles = query.Select(x => ToDomainProfile(x));
            if(name != null)
            {
                profiles = profiles.Where(x => x.Name.ToUpperInvariant().Contains(name.ToUpperInvariant())).ToList();
            }
            else
            {
                return new List<DomainProfile>();
            }
           
            return profiles;
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
                var newUser = await ToEntityProfileAsync(profileData); // convert
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
        /// <param name="email">The original email of the profile, in case it was chaanged</param>
        /// <param name="domainProfileData">The data for the domain profile to be set to.</param>
        public async Task UpdateProfileAsync(string email, DomainProfile domainProfileData)
        {
            // have to have this try catch block to prevent errors from data base
            try
            {
                var userEntity = await ToEntityProfileAsync(domainProfileData);

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
                profile.Followers = userEntity.Followers;
                profile.Following = userEntity.Following;
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
