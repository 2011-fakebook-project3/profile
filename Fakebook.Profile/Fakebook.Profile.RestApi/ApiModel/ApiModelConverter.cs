using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Profile.RestApi.ApiModel
{
    public class ApiModelConverter
    {
        /*
        // from domain to api??
        public static ProfileApiModel ToUserApiModel(DomainProfile user)
        {
            var followerIds = user.Followers
                .Select(f => f.Id)
                .ToList();

            var followeeIds = user.Followees
                .Select(f => f.Id)
                .ToList();

            return new ProfileApiModel
            {               
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Status = user.Status,
                ProfilePictureUrl = user.ProfilePictureUrl,
                PhoneNumber = user.PhoneNumber,
                //FolloweeIds = followeeIds,
                //FollowerIds = followerIds
            };
        }

        public static User ToUser(IUserRepo userRepo, UserApiModel apiModel)
        {
            apiModel.FirstName.EnforceNameCharacters(nameof(apiModel.FirstName));
            apiModel.LastName.EnforceNameCharacters(nameof(apiModel.LastName));
            apiModel.Email.EnforceEmailCharacters(nameof(apiModel.Email));

            // must match phone number regex
            if (!apiModel.PhoneNumber.IsNullOrEmpty())
            {
                apiModel.PhoneNumber.EnforcePhoneNumberCharacters(nameof(apiModel.PhoneNumber));
            }

            if (apiModel.Status is not null)
            {
                apiModel.Status.EnforceNoSpecialCharacters(nameof(apiModel.Status));
            }

            // if status is not null, filter out any non-file allowed characters
            
            List<User> followers = null;
            List<User> followees = null;

            if (apiModel.FolloweeIds is not null && apiModel.FolloweeIds.Any())
            {
                followees = userRepo.GetUsersByIdsAsync(apiModel.FolloweeIds)
                    .Result
                    .ToList();
            }

            if (apiModel.FollowerIds is not null && apiModel.FollowerIds.Any())
            {
                followers = userRepo.GetUsersByIdsAsync(apiModel.FollowerIds)
                    .Result
                    .ToList();
            }

            followees ??= new List<User>();
            followers ??= new List<User>();

            return new User
            {
                Id = apiModel.Id,
                ProfilePictureUrl = apiModel.ProfilePictureUrl,
                FirstName = apiModel.FirstName,
                LastName = apiModel.LastName,
                Email = apiModel.Email,
                PhoneNumber = apiModel.PhoneNumber,
                BirthDate = apiModel.BirthDate,
                Status = apiModel.Status,
                Followers = followers,
                Followees = followees
            };
        }
        */

    }
}
