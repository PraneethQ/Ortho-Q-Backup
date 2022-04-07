using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Member.API.Model
{
    public class MemberInformation
    {
            public int Id { get; set; }
            public Guid? MemberId { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public int? Username { get; set; }
            public int? MaritalStatus { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public string DeletedBy { get; set; }
            public DateTime? DeleteOn { get; set; }
            public bool? IsActive { get; set; }
    }
}
