using System;
using System.Collections;
using System.Collections.Generic;

using Fakebook.Profile.Domain;

namespace Fakebook.Profile.UnitTests.TestData.ProfileTestData
{
    public static class Update
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
        private static string fakeURI6 = "https://i.imgur.com/FAKE6.jpg";
        private static string fakeURI7 = "https://i.imgur.com/FAKE7.jpg";


        public class Valid : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                // email will be used to update the profile
                string targetEmail1 = GenerateRandom.Email();

                yield return new object[]
                {
                    new DomainProfile
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = targetEmail1,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    },
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = targetEmail1,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    }
                };

                // email will be used to update the profile
                string targetEmail2 = GenerateRandom.Email();

                yield return new object[]
                {
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = targetEmail2,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    },
                    new DomainProfile
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = targetEmail2,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    }
                };

                // email will be used to update the profile
                string targetEmail3 = GenerateRandom.Email();

                yield return new object[]
                {
                    new DomainProfile
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        // Invalid URI format
                        // ProfilePictureUrl = new Uri(GenerateRandom.String())
                        ProfilePictureUrl = new Uri(fakeURI1),
                        Email = targetEmail3,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    },
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI2),
                        Email = targetEmail3,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    }
                };

                // email will be used to update the profile
                string targetEmail4 = GenerateRandom.Email();

                yield return new object[]
                {
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI3),
                        Email = targetEmail4,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    },
                    new DomainProfile
                    {
                        FirstName = GenerateRandom.String(),
                        LastName = GenerateRandom.String(),
                        ProfilePictureUrl = null,
                        Email = targetEmail4,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class Invalid : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {


                // email will be used to update the profile
                string targetEmail1 = GenerateRandom.Email();

                // 1st profile is valid, 2nd profile is invalid
                yield return new object[]
                {
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI4),
                        Email = targetEmail1,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    },
                    new DomainProfile
                    {
                        // cannot set to null here, will get exception
                        // FirstName =  null,
                        // LastName =  null,
                        ProfilePictureUrl = new Uri(fakeURI5),
                        Email = targetEmail1,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    }
                };               
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class InvalidPhone: IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                // email will be used to update the profile
                string targetEmail2 = GenerateRandom.Email();

                yield return new object[]
                {
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI6),
                        Email = targetEmail2,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                     },

                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(fakeURI7),
                        Email = targetEmail2,
                        // cannot set here, will get exception
                        // PhoneNumber = GenerateRandom.String(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    }
                };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

