using System.Collections.Generic;

namespace BlogsDEMO.Application.DTOs.Email
{
    public class EmailMultiRequest
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
    }
}