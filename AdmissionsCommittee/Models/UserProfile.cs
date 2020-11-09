using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public string BirthDay { get; set; }
  
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
