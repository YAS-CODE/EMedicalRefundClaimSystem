using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMedicalClaimRefundSystem.Models;
using EMedicalClaimRefundSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMedicalClaimRefundSystem.Pages.Bank
{
    [Authorize(Roles = "Accountant")]
    public class CRDAprovedModel : PageModel
    {
        private readonly IERefundUserApplicationRepo _userapplication;
        public CRDAprovedModel(IERefundUserApplicationRepo userapplication)
        {
            _userapplication = userapplication;
        }
        public void OnGet()
        {

        }
        public async Task<ActionResult> OnGetRequestedAppsAsync()
        {
            List<UserApplicationViewModel> userapps = new List<UserApplicationViewModel>();
            IEnumerable<ERefundUserApplication> Apps = _userapplication.GetByAppStatus(Convert.ToInt32(ApplicationStatus.CRDAccepted));

            foreach (var App in Apps)
            {
                userapps.Add(new UserApplicationViewModel() { Id = App.Id, Notes = App.Notes, Status = App.Status.ToString(), AppliedByUserId = App.ApplicationUser.Id, AppliedByUserName = App.ApplicationUser.UserName, AppliedDate = App.DateTime, Address = App.Address, PatientName = App.PatientName, PhoneNumber = App.PhoneNumber });
            }

            return new JsonResult(userapps);
            //return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }
    }
}