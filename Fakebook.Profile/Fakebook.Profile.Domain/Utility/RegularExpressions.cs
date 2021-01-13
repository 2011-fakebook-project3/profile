namespace Fakebook.Profile.Domain.Utility
{
    public static class RegularExpressions
    {
        /// <summary>
        /// Regex string representing restrictions on characters.
        /// </summary>
        public const string NoSpecialCharacters = @"^[^0-9]+[^\\\|\<\>\;\:\[\]\{\}\=\+\(\)]+$";
        /// <summary>
        /// Regex string for valid email addresses.
        /// </summary>
        public const string EmailCharacters = @"^[a-zA-Z]+[a-zA-Z0-9\.]*[a-zA-Z0-9]*\@([a-zA-Z]+[a-zA-Z0-9]*\.([a-zA-Z]+){0,253})+$";
        /// <summary>
        /// Characters allowed in a phone number.
        /// </summary>
        public const string PhoneNumberCharacters = @"^(\([0-9]{3}\)|[0-9]{3})?[ -]?[0-9]{3}[ -]?[0-9]{4}$";
    }
}
