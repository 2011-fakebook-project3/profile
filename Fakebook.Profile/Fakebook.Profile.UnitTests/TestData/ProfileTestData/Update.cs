using System;
using System.Collections;
using System.Collections.Generic;

using Fakebook.Profile.Domain;

namespace Fakebook.Profile.UnitTests.TestData.ProfileTestData
{
    public static class Update
    {

        public class Valid : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator() {

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
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
                        Email = targetEmail3,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = null
                    },
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
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
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
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
            public IEnumerator<object[]> GetEnumerator() {

                // email will be used to update the profile
                string targetEmail1 = GenerateRandom.Email();

                yield return new object[]
                {
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
                        Email = targetEmail1,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    },
                    new DomainProfile
                    {
                        FirstName =  null,
                        LastName =  null,
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
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
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
                        Email = targetEmail2,
                        PhoneNumber = GenerateRandom.PhoneNumber(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    },
                    new DomainProfile
                    {
                        FirstName =  GenerateRandom.String(),
                        LastName =  GenerateRandom.String(),
                        ProfilePictureUrl = new Uri(GenerateRandom.String()),
                        Email = targetEmail2,
                        PhoneNumber = GenerateRandom.String(),
                        BirthDate = GenerateRandom.DateTime(),
                        Status = GenerateRandom.String()
                    }
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
