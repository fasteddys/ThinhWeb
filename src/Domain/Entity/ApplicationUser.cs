using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<PersonalTask> PersonalTasks { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}
