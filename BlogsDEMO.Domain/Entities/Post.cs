using BlogsDEMO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogsDEMO.Domain.Entities
{
    public class Post : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string AvatarUrl { get; set; }
        public string MetaTitle { get; set; } // Tiêu đề hiển thị các kết quả tìm kiếm trên các công cụ tìm kiếm
        public string Slug { get; set; } // abc.com/{day-la-slug}
        public string Summary { get; set; }
        public bool Published { get; set; }
        public string PublishUserId { get; set; }
        public string PublishDate { get; set; }
        public string ViewCount { get; set; }
    }
}
