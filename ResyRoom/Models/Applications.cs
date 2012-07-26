using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Applications
    {
        public Applications()
        {
            Memberships = new HashSet<Memberships>();
            Roles = new HashSet<Roles>();
            Users = new HashSet<User>();
        }
    
        public string ApplicationName { get; set; }
        public Guid ApplicationId { get; set; }
        public string Description { get; set; }
    
        public ICollection<Memberships> Memberships { get; set; }
        public ICollection<Roles> Roles { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
