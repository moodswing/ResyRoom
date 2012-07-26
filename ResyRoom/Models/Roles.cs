using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Roles
    {
        public Roles()
        {
            Users = new HashSet<User>();
        }
    
        public Guid ApplicationId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    
        public Applications Applications { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
