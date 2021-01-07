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

        // randomly generate URI throws an exception
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
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null,
                    }
                };

                yield return new object[]
                {
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI1),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };

                yield return new object[]
                {
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI2),
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null,
                    }
                };

                yield return new object[]
                {
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// generates 3 variants of invalid user profiles:
        /// invalid first name, last name
        /// invalid email format
        /// invlaid phone number format 
        /// </summary>
        public class InvalidName : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        // cannot set to null, will get exception
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
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI4),
                        // cannot set to a random string, will get exception
                        // PhoneNumber = GenerateRandom.String(), // .PhoneNumber()
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class InvalidEmail : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new DomainProfile
                    {
                        // cannot set to a random string, will get exception
                        // Email = GenerateRandom.String(), // .Email()
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
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
 