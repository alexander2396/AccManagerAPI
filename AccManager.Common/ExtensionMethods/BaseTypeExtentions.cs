using System.ComponentModel.DataAnnotations;

namespace AccManager.Common.ExtensionMethods
{
    public static class BaseTypeExtentions
    {
        public static bool IsValidEmail(this string emailString)
        {
            return !string.IsNullOrWhiteSpace(emailString) &&
                new EmailAddressAttribute().IsValid(emailString);
        }
    }
}
