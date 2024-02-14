namespace API.Helpers
{
    /// <summary>
    /// Helper class for retrieving HTML content for email bodies.
    /// </summary>
    public static class GetHtmlBody
    {
        /// <summary>
        /// Retrieves the HTML content for confirming email.
        /// </summary>
        /// <returns>The HTML content for confirming email.</returns>
        public static string GetBodyForConfirm()
        {
            string htmlPath = "wwwroot/Email.html";
            string htmlContent = File.ReadAllText(htmlPath);
            return htmlContent;
        }
        /// <summary>
        /// Retrieves the HTML content for resetting password.
        /// </summary>
        /// <returns>The HTML content for resetting password.</returns>
        public static string GetBodyForResetPassword()
        {
            string htmlPath = "wwwroot/ForgetPassword.html";
            string htmlContent = File.ReadAllText(htmlPath);
            return htmlContent;
        }
    }

}