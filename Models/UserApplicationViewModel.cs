using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMedicalClaimRefundSystem.Models
{
    public class UserApplicationViewModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string AppliedByUserName { get; set; }
        public string AppliedByUserId { get; set; }
        public DateTime AppliedDate { get; set; }

        public string[] Attachs { get; set; }
    }
}
