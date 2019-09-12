using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMedicalClaimRefundSystem.Models;
using EMedicalClaimRefundSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMedicalClaimRefundSystem.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserApplicationsModel : PageModel
    {
        private readonly IERefundUserApplicationRepo _userapplication;
        public UserApplicationsModel(IERefundUserApplicationRepo userapplication)
        {
            _userapplication = userapplication;
        }
        public void OnGet()
        {

        }
        public async Task<ActionResult> OnGetApplicationsAsync()
        {
            List<UserApplicationViewModel> userapps = new List<UserApplicationViewModel>();
            IEnumerable<ERefundUserApplication> Apps= await _userapplication.GetAllAsync();

            foreach (var App in Apps)
            {
                userapps.Add(new UserApplicationViewModel() { Id = App.Id, Notes = App.Notes, Status = App.Status.ToString(), AppliedByUserId = App.ApplicationUser.Id, AppliedByUserName = App.ApplicationUser.UserName, AppliedDate = App.DateTime, Address = App.Address, PatientName = App.PatientName, PhoneNumber = App.PhoneNumber });
            }

            return new JsonResult(userapps);
            //return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }
        public ActionResult OnPostDelApp([FromBody] List<int> appids)
        {
            foreach (var appid in appids)
            {
                _userapplication.DeleteAsync(appid);
            }
            
            return new JsonResult("Ok");
        }
    }
}