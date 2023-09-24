namespace CrossFit.Glack.Staff.ViewResult
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public class AlreadySubmitted : ViewResult
    {
        public AlreadySubmitted()
        {
            ViewName = "AlreadySubmitted";
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}