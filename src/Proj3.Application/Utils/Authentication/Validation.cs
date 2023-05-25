using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Proj3.Application.Utils.Authentication
{
    public static class Validation
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsValidPassword(string password)
        {

            Regex? hasNumber = new(@"[0-9]+");
            Regex? hasLowerChar = new(@"[a-z]+");
            Regex? hasUpperChar = new(@"[A-Z]+");
            Regex? hasNonAlphaNum = new(@"\W|_");
            Regex? hasMinimum8Chars = new(@".{8,}");

            bool isValidated =
            hasNumber.IsMatch(password) &&
            hasLowerChar.IsMatch(password) &&
            hasUpperChar.IsMatch(password) &&
            hasNonAlphaNum.IsMatch(password) &&
            hasMinimum8Chars.IsMatch(password);

            return isValidated;
        }
    }
}