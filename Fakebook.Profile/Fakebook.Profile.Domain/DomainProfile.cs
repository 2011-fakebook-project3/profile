using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Fakebook.Profile.Domain.Utility;

namespace Fakebook.Profile.Domain
{
    /// <summary>
    /// A profile for the domain logic and Api to use.
    /// </summary>
    public class DomainProfile
    {
        //[anything]@[anything].[anything]
        private string _email;

        public List<string> FollowerEmails { get; set; }
        public List<string> FollowingEmails { get; set; }

        /// <summary>
        /// The user's email.
        /// </summary>
        /// <remarks>
        /// Cannot be null, and must match the Regex for email characters.
        /// </remarks>
        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), $"Invalid Email, {value}");
                }

                Regex emailRegex = new(RegularExpressions.EmailCharacters);
                if (!emailRegex.IsMatch(value))
                    throw new ArgumentException("The email doesn't max the regex", nameof(value));
                _email = value;
            }
        }

        /// <summary>
        /// A url that is expected to be an image. Technically though, no checks are done to enforce that.
        /// </summary>
        public Uri ProfilePictureUrl { get; set; }

        /// <summary>
        /// Shortcut to get a user's full name.
        /// </summary>
        public string Name => $"{FirstName} {LastName}";

        private string _firstName;
        /// <summary>
        /// The user's first name.
        /// </summary>
        /// <remarks>
        /// Disallow any prohibited characters, but allow for emojis and any other characters
        /// </remarks>
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), $"Invalid first name, {value}");
                }

                Regex nameRegex = new(RegularExpressions.NoSpecialCharacters);
                // throw null exception if value is null
                if (!nameRegex.IsMatch(value))
                    throw new ArgumentException("First name does not match the regex", nameof(value));
                _firstName = value;
            }
        }

        private string _lastName;
        /// <summary>
        /// The user's last name.
        /// </summary>
        /// <remarks>
        /// A-Z, ', ., - are the only valid characters for first and last name.
        /// </remarks>
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), $"Invalid last name, {value}");
                }

                Regex nameRegex = new(RegularExpressions.NoSpecialCharacters);
                if (!nameRegex.IsMatch(value))
                    throw new ArgumentException("Last name does not match the regex", nameof(value));
                _lastName = value;
            }
        }

        private string _phoneNumber;
        /// <summary>
        /// The user's phone number.
        /// </summary>
        /// <remarks>
        /// any up to 15 digits, can have - or () spaces optional, or be null
        /// </remarks>
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value is not null)
                {
                    Regex phoneRegex = new(RegularExpressions.PhoneNumberCharacters);
                    if (!phoneRegex.IsMatch(value))
                        throw new ArgumentException($"An invalid phone number was passed in (got '{value}').", nameof(value));
                }

                _phoneNumber = value;
            }
        }

        private DateTime _birthDate;
        /// <summary>
        /// The user's birthdate.
        /// </summary>
        /// <remarks>
        /// Can't be null, but will default to all zeros.
        /// </remarks>
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                if (value.Date > DateTime.Now.Date)
                    throw new ArgumentException($"Invalid Date, {value.Date}, is in the future.");
                _birthDate = value;
            }
        }

        private string _status;
        /// <summary>
        /// The user's current status message.
        /// </summary>
        /// <remarks>
        /// can be null, or reasonable text (sanitized so they don't get funky)
        /// </remarks>
        public string Status
        {
            get => _status;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _status = null;
                }
                else
                {
                    Regex regex = new(RegularExpressions.NoSpecialCharacters);
                    if (!regex.IsMatch(value))
                        throw new ArgumentException($"Status passed in is invalid, (got '{value}')", nameof(value));
                    _status = value;
                }
            }
        }

        public void AddFollow(string followingEmail)
        {
            throw new NotImplementedException();
        }

        public void AddFollower(string followerEmail)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Construct a new DomainProfile with all of its properties/backing fields assigned if valid
        /// </summary>
        /// <param name="email">The email of the user; must be unique</param>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="birthDate">The user's birthdate</param>
        /// <param name="profilePictureUri">The URI of the user's profile picture; if null, will default to a valid URI</param>
        /// <param name="phoneNumber">The phone number of the user's profile; it can be null</param>
        /// <param name="status">The status message of the user's profile; it can be null</param>
        public DomainProfile(string email, string firstName, string lastName, DateTime birthDate, Uri profilePictureUri = null, string phoneNumber = null, string status = null)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;

            ProfilePictureUrl = profilePictureUri ?? new Uri(ProfileConfiguration.DefaultUri);
            Status = status;
        }

        /// <summary>
        /// Create a new user with a phone number filled in.
        /// </summary>
        /// <param name="email">The user's email. Must be unique.</param>
        /// <param name="phoneNumber">The user's phone number/</param>
        /// <param name="firstName">The user's first name.</param>
        /// <param name="lastName">The user's last name.</param>
        public DomainProfile(string email, string phoneNumber, string firstName, string lastName)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;

            ProfilePictureUrl = new Uri(ProfileConfiguration.DefaultUri);
        }

        /// <summary>
        /// Create a basic profile
        /// </summary>
        /// <param name="email">The user's email. Must be Unique.</param>
        /// <param name="firstName">The user's firstname.</param>
        /// <param name="lastName">The user's lastname.</param>
        public DomainProfile(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            ProfilePictureUrl = new Uri(ProfileConfiguration.DefaultUri);
            FollowerEmails = new List<string>();
            FollowingEmails = new List<string>();
        }

    }
}
