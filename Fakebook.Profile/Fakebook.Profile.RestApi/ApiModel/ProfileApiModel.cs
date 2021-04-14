using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Fakebook.Profile.Domain;
using Fakebook.Profile.Domain.Utility;

namespace Fakebook.Profile.RestApi.ApiModel
{
    /// <summary>
    /// A model to store the data that the REST API will serve to any requests
    /// </summary>
    public class ProfileApiModel
    {
        /// <summary>
        /// The user's email. The primary key.
        /// [anything]@[anything].[anything]
        /// </summary>
        [Required, RegularExpression(RegularExpressions.EmailCharacters)]
        public string Email { get; set; }

        /// <summary>
        /// The Uri of the user's profile picture
        /// Can be null and default to an image
        /// </summary>
        public Uri ProfilePictureUrl { get; set; }

        /// <summary>
        /// The user's first name. Cannot be null.
        /// </summary>
        [Required, RegularExpression(RegularExpressions.NoSpecialCharacters)]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name, cannot be null.
        /// </summary>
        [Required, RegularExpression(RegularExpressions.NoSpecialCharacters)]
        public string LastName { get; set; }

        /// <summary>
        /// The user's phone number. can be upto 15 characters and can be null.
        /// </summary>
        [Required, RegularExpression(RegularExpressions.PhoneNumberCharacters)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The user's birthdate. Can't be null and can't in the future
        /// </summary>
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The user's current status message. Can be null, but sanitized so they don't get funky
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// List of users following the current user
        /// </summary>
        public IList<string> Followers { get; set; }

        /// <summary>
        /// List of users the current user is following
        /// </summary>
        public IList<string> Following { get; set; }

        public ProfileApiModel()
        {
        }

        public ProfileApiModel(DomainProfile p)
        {
            Email = p.Email;
            ProfilePictureUrl = p.ProfilePictureUrl;
            FirstName = p.FirstName;
            LastName = p.LastName;
            PhoneNumber = p.PhoneNumber;
            BirthDate = p.BirthDate;
            Status = p.Status;
            Followers = p.FollowerEmails;
            Following = p.FollowingEmails;
        }

        public DomainProfile ToDomainProfile()
        {
            return new(Email, FirstName, LastName, BirthDate, ProfilePictureUrl, PhoneNumber, Status, Followers, Following);
        }
    }
}
