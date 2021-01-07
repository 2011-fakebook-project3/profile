using System;
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
        public ProfileApiModel(DomainProfile p)
        {
            Email = p.Email;
            ProfilePictureUrl = p.ProfilePictureUrl;
            FirstName = p.FirstName;
            LastName = p.LastName;
            PhoneNumber = p.PhoneNumber;
            BirthDate = p.BirthDate;
            Status = p.Status;
        }

        //[anything]@[anything].[anything]
        [Required, RegularExpression(RegularExpressions.EmailCharacters)]
        public string Email { get; set; }

        //should be a url
        //defaults to a default image.
        public Uri ProfilePictureUrl { get; set; }

        [Required, RegularExpression(RegularExpressions.NameCharacters)]
        public string FirstName { get; set; }

        [Required, RegularExpression(RegularExpressions.NameCharacters)]
        public string LastName { get; set; }

        [Required, RegularExpression(RegularExpressions.PhoneNumberCharacters)]
        public string PhoneNumber { get; set; }

        //not future
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        //can be null, or reasonable text (sanitized so they don't get funky)
        public string Status { get; set; }
    }
}
