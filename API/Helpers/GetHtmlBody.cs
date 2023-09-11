namespace API.Helpers
{
    public static class GetHtmlBody
    {
        public static string GetBodyForConfirm()
        {
           string htmlPath = "../API/data/Email.html";
           string htmlContent = File.ReadAllText(htmlPath);
           return htmlContent;
        }
         public static string GetBodyForResetPassword()
        {
           string htmlPath = "../API/data/ForgetPassword.html";
           string htmlContent = File.ReadAllText(htmlPath);
           return htmlContent;
        }
    }
   
}