using Fakebook.Profile.Domain;

using System;
using System.Collections;
using System.Collections.Generic;

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
            public IEnumerator<object[]> GetEnumerator() {
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
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
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
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
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
            public IEnumerator<object[]> GetEnumerator() {
                yield return new object[]
                {
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        FirstName = null,
                        LastName = null,
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
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
            public IEnumerator<object[]> GetEnumerator() {
                yield return new object[]
                {
                    new DomainProfile
                    {
                        Email = GenerateRandom.Email(),
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
                        PhoneNumber = GenerateRandom.String(), // .PhoneNumber()
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String(),
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }


        public class InvalidEmail : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator() {
                yield return new object[]
                {
                    new DomainProfile
                    {
                        Email = GenerateRandom.String(), // .Email()
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
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
