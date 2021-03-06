using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalManagement.DTO
{
    public class PostDto
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "This field is required!")]
        public string Title { get; set; }
        [Display(Name = "Content")]
        [Required(ErrorMessage = "This field is required!")]
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string CreatedOn { get; set; }
        public string Id { get; set; }
        public IList<string> Tags { get; set; }
        [Display(Name = "Tag")]
        public IList<string> TagNames { get; set; } = new List<string>();
        public eStatus Status { get; set; }
    }
}
