namespace API.Helpers
{
    public static class GetHtmlBody
    {
        public static string GetBody()
        {
           string htmlPath = "../API/data/Email.html";
           string htmlContent = File.ReadAllText(htmlPath);
           return htmlContent;
        }
    }
}