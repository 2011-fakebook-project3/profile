using System;
using System.Collections;
using System.Collections.Generic;

using Fakebook.Profile.Domain;

namespace Fakebook.Profile.UnitTests.TestData.ProfileTestData
{
    public static class Create
    {
        /*
         * User:
         * - Email: string
         * - ProfilePictureUrl: string 
         * - Name : string
         * - FirstName: string
         * - Lastname: string              
         * - PhoneNumber: string
         * - BirthDate: DateTime
         * - Status: string
         */

        // randomly generated URI throws an exception
        private static string fakeURI1 = "https://i.imgur.com/FAKE1.jpg";
        private static string fakeURI2 = "https://i.imgur.com/FAKE2.jpg";
        private static string fakeURI3 = "https://i.imgur.com/FAKE3.jpg";
        private static string fakeURI4 = "https://i.imgur.com/FAKE4.jpg";
        private static string fakeURI5 = "https://i.imgur.com/FAKE5.jpg";

        public class Valid : IEnumerable<object[]>
        {
            /// <summary>
            /// generates 4 variants of valid user profiles:
            /// profile picture, status
            ///        -           -
            ///        +           +
            ///        +           -
            ///        -           +
            /// </summary>
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                    {
                        ProfilePictureUrl = null,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null,
                    }
                };

                yield return new object[]
                {
                    new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                    {
                        ProfilePictureUrl = new Uri(fakeURI1),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };

                yield return new object[]
                {
                    new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                    {
                        ProfilePictureUrl = new Uri(fakeURI2),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null,
                    }
                };

                yield return new object[]
                {
                    new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                    {
                        ProfilePictureUrl = null,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = "✔λΔΩ",
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// generates 2 variants of invalid user profiles:
        /// invalid email format
        /// invlaid phone number format 
        /// </summary>
        /// <remarks>
        /// NOTE: Does currently have a valid placeholder name, should be replaced with null
        /// or a string with invalid characters for testing.
        /// </remarks>
        public class InvalidName : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                    {
                        // cannot set to null here, will get exception
                        // FirstName = null,
                        // LastName = null,
                        ProfilePictureUrl = new Uri(fakeURI3),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class InvalidPhoneNumber : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                    {
                        ProfilePictureUrl = new Uri(fakeURI4),
                        // cannot set to a random string here, will get exception
                        // PhoneNumber = GenerateRandom.String(), // .PhoneNumber()
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// Users with placeholder emails for testing invalid emails.
        /// </summary>
        /// <remarks>
        /// The placeholder emails are valid because it's impossible to create a domain user with an invalid email.
        /// Instead user generate random string in place of the valid email.
        /// </remarks>
        public class InvalidEmail : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                    {
                        // cannot set to a random string here, will get exception
                        // Email = GenerateRandom.String(), // .Email()
                        ProfilePictureUrl = new Uri(fakeURI5),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
