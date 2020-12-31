using System.Collections;
using System.Collections.Generic;

using Fakebook.Profile.Domain;

namespace Fakebook.Profile.UnitTests.TestData.ProfileTestData
{
    public static class Read
    {
        public class Valid : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator() {
                string target = GenerateRandom.Email();

                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = target,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    },
                    target
                };

                target = GenerateRandom.Email();
                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = target,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    },
                    target
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class Invalid : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator() {
                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    },
                    GenerateRandom.Email()
                };

                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    },
                    null
                };

                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile
                        {
                            FirstName = GenerateRandom.String(),
                            LastName = GenerateRandom.String(),
                            ProfilePictureUrl = null,
                            Email = GenerateRandom.Email(),
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    },
                    GenerateRandom.String()
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
