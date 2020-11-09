using Microsoft.AspNetCore.Identity;

namespace AdmissionsCommittee.Models
{
    public class User:IdentityUser
    {
        public int Year { get; set; }

        public UserProfile UserProfile { get; set; }
        public UserDocument UserDocument { get; set; }
        public UserDocumentFile UserDocumentFile { get; set; }

    }
}
