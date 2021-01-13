using System.Collections;
using System.Collections.Generic;

using Fakebook.Profile.Domain;

namespace Fakebook.Profile.UnitTests.TestData.ProfileTestData
{
    public static class Read
    {
        public class Valid : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {

                string target = GenerateRandom.Email();

                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile(target, GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
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
                        new DomainProfile(target, GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
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
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
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
                        new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
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
                        new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
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

        /// <summary>
        /// Test data for GetAllProfiles() onle
        /// </summary>
        public class ValidCollection : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                string target = GenerateRandom.Email();
                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile(target, GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    },
                };

                target = GenerateRandom.Email();
                yield return new object[]
                {
                    new List<DomainProfile>
                    {
                        new DomainProfile(target, GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        },
                        new DomainProfile(GenerateRandom.Email(), GenerateRandom.String(), GenerateRandom.String())
                        {
                            ProfilePictureUrl = null,
                            PhoneNumber = GenerateRandom.PhoneNumber(),
                            BirthDate = GenerateRandom.DateTime(),
                            Status = null
                        }
                    },
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

