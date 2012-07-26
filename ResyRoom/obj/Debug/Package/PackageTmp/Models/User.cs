using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public partial class User
    {
        public User()
        {
            Bandas = new HashSet<Banda>();
            Estudios = new HashSet<Estudio>();
            Roles = new HashSet<Roles>();
        }
    
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDate { get; set; }
    
        public Applications Applications { get; set; }
        public Memberships Memberships { get; set; }
        public Profiles Profiles { get; set; }
        public ICollection<Banda> Bandas { get; set; }
        public ICollection<Estudio> Estudios { get; set; }
        public ICollection<Roles> Roles { get; set; }
    }
}
