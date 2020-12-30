using System;

namespace Fakebook.Profile.UnitTests.TestData
{
    public static class RegularExpressions
    {
        public static readonly const string NoSpecialCharacters = @"^[A-Za-z0-9 '\-_%,.#?:!/]*$";
        public static readonly const string NameCharacters = @"^[A-Za-z0-9 -]*$";
        public static readonly const string EmailCharacters = @"^[a-zA-Z]+[a-zA-Z0-9\.]*[a-zA-Z0-9]*\@[a-zA-Z]+[a-zA-Z0-9]*\.([a-zA-Z]+){0,253}$";
        public static readonly const string PhoneNumberCharacters = @"^(\([0-9]{3}\)|[0-9]{3})?[ -]?[0-9]{3}[ -]?[0-9]{4}$";
    }
}
