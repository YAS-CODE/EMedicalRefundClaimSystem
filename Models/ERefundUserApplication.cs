using Dapper.Contrib.Extensions;
using EMedicalClaimRefundSystem.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMedicalClaimRefundSystem.Models
{
    enum ApplicationStatus {UserClaimed,CRDAccepted,CRDRejected,BankRejected,RefundedSucessfully,UserCenceled}
    public class ERefundUserApplication
    {
        [ExplicitKey]
        [Required]
        public int Id { get; set; }
        [Required]
        public String Notes { get; set; }
        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
