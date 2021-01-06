namespace Fakebook.Profile.Domain.Utility
{
    public static class RegularExpressions
    {
        public const string NoSpecialCharacters = @"^[A-Za-z0-9 '\-_%,.#?:!/]*$";
        public const string NameCharacters = @"^[A-Za-z0-9 -]*$";
        public const string EmailCharacters = @"^[a-zA-Z]+[a-zA-Z0-9\.]*[a-zA-Z0-9]*\@[a-zA-Z]+[a-zA-Z0-9]*\.([a-zA-Z]+){0,253}$";
        public const string PhoneNumberCharacters = @"^(\([0-9]{3}\)|[0-9]{3})?[ -]?[0-9]{3}[ -]?[0-9]{4}$";
    }
}
