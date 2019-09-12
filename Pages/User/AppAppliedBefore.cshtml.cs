using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EMedicalClaimRefundSystem.Models;
using EMedicalClaimRefundSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMedicalClaimRefundSystem.Pages
{
    [Authorize(Roles = "User")]
    public class AppAppliedBeforeModel : PageModel
    {
        private readonly IERefundUserApplicationRepo _userapplication;
        public string ReturnUrl { get; set; }
        public AppAppliedBeforeModel(IERefundUserApplicationRepo userapplication)
        {
            _userapplication = userapplication;
        }
        public void OnGet(string returnUrl = null)
        {
            var UserApps = _userapplication.GetByUser(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ReturnUrl = returnUrl;           
        }

        public IActionResult OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                /*var user = new ERefundUserApplication
                {
                    Notes = Input.AppNote,
                    Status = (int)ApplicationStatus.Claimed,
                    DateTime = DateTime.Now,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };*/
                

                return LocalRedirect(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public ActionResult OnGetUserApplications()
        {
            List<UserApplicationViewModel> userapps = new List<UserApplicationViewModel>();
            IEnumerable<ERefundUserApplication> Apps = _userapplication.GetByUser(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            foreach (var App in Apps)
            {
                userapps.Add(new UserApplicationViewModel() { Id = App.Id, Notes = App.Notes, Status = App.Status.ToString(), AppliedByUserId = App.ApplicationUser.Id, AppliedByUserName = App.ApplicationUser.UserName, AppliedDate = App.DateTime ,Address = App.Address,PatientName=App.PatientName,PhoneNumber=App.PhoneNumber});
            }

            return new JsonResult(userapps);
            //return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }


    }
}