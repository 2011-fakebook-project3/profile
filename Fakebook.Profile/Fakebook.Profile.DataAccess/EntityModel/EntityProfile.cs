using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fakebook.Profile.Domain.Utility;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    /// <summary>
    /// Database model of a profile.
    /// </summary>
    [Table("Profile")]
    public class EntityProfile
    {
        #nullable enable

        /// <summary>
        /// Create a profile with default profile picture
        /// </summary>
        /// <param name="email">The user's email. Must be unique.</param>
        /// <param name="firstName">The user's firstname.</param>
        /// <param name="lastName">The user's lastname.</param>
        /// <param name="dob">The user's date of birth.</param>
        /// <param name="phoneNumber">The user's phone number.</param>
        /// <param name="status">The user's status.</param>
        /// <param name="profilePictureUrl">The url of the user's profile picture.</param>
        public EntityProfile(string email, string firstName, string lastName, DateTime dob, string? phoneNumber = null, string? status = null, IList<string>? followerEmails = null, IList<string>? followingEmails = null)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = dob;
            PhoneNumber = phoneNumber;
            FollowerEmails = followerEmails;
            FollowingEmails = followingEmails;
            ProfilePictureUrl = new Uri(ProfileConfiguration.DefaultUri);
        }

        /// <summary>
        /// Create a profile with user-specified profile picture
        /// </summary>
        /// <param name="email">The user's email. Must be unique.</param>
        /// <param name="firstName">The user's firstname.</param>
        /// <param name="lastName">The user's lastname.</param>
        /// <param name="dob">The user's date of birth.</param>
        /// <param name="phoneNumber">The user's phone number.</param>
        /// <param name="status">The user's status.</param>
        /// <param name="profilePictureUrl">The url of the user's profile picture.</param>
        public EntityProfile(string email, string firstName, string lastName, DateTime dob, Uri profilePictureUrl, string? phoneNumber = null, string? status = null, IList<string>? followerEmails = null, IList<string>? followingEmails = null)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = dob;
            PhoneNumber = phoneNumber;
            FollowerEmails = followerEmails;
            FollowingEmails = followingEmails;
            ProfilePictureUrl = profilePictureUrl;
        }

        /// <summary>
        /// The user's ID.  The primary key.  Cannot be null.
        /// </summary>
        [Key]
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The user's unique email. Cannot be null.
        /// </summary>
        [Required]
        [MaxLength(45),MinLength(5)]
        public string Email { get; set; }

        /// <summary>
        /// The user's followers. Can be null.
        /// </summary>
        [NotMapped]
        public IList<string>? FollowerEmails { get; set; } 

        /// <summary>
        /// The user's followees. Can be null.
        /// </summary>
        [NotMapped]
        public IList<string>? FollowingEmails { get; set; } 

        /// <summary>
        /// The Uri of the user's profile picture. Can be null.
        /// </summary>
        public Uri? ProfilePictureUrl { get; set; }

        /// <summary>
        /// The user's first name. Cannot be null.
        /// </summary>
        [MaxLength(25)]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name. Cannot be null.
        /// </summary>
        [Required]
        [MaxLength(25),MinLength(2)]
        public string LastName { get; set; }

        /// <summary>
        /// The user's phone number. Can be 10 - 15 characters. Can be null.
        /// </summary>
        [MaxLength(15),MinLength(10)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// The user's birthdate. Cannot be null.
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The user's current status message. Can be null.
        /// </summary>
        [MaxLength(300)]
        public string? Status { get; set; }
    }
}
