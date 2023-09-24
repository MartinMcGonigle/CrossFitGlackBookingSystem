namespace CrossFit.Glack.Staff.ViewResult
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public class NotFound : ViewResult
    {
        public NotFound()
        {
            ViewName = "NotFound";
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}