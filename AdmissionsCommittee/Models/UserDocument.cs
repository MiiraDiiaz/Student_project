using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdmissionsCommittee.Models
{
    public class UserDocument
    {
        public int Id { get; set; }
        public string TypeDocument { get; set; }
        public string SeriesNamberDocument { get; set; }
        public string DateIssuanceDicument { get; set; }
        public string IssuanceOffice { get; set; }
        public string BirthPlace { get; set; }
        public string Phone { get; set; }
        

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
