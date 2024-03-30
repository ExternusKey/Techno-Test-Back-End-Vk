namespace AuthService.Validation
{
    public class PassValidator
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsWeakPassword(string password)
        {

            return password.Length < 8;
        }

        public static bool IsPerfectPassword(string password)
        {
           
            return password.Length >= 12 && password.Any(char.IsDigit) && password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(c => !char.IsLetterOrDigit(c));
        }
    }
}
