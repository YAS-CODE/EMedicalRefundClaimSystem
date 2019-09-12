using Dapper.Contrib.Extensions;
using EMedicalClaimRefundSystem.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMedicalClaimRefundSystem.Models
{

    public class ApplicationHistory
    {
        [ExplicitKey]
        [Required]
        public int Id { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
