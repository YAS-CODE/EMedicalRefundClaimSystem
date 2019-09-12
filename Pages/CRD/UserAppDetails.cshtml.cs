using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EMedicalClaimRefundSystem.Models;
using EMedicalClaimRefundSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMedicalClaimRefundSystem.Pages.CRD
{
    [Authorize(Roles = "CRD")]
    public class UserAppDetailsModel : PageModel
    {
        public int appid { get; set; }

        private IHostingEnvironment _hostingEnvironment;

        public IERefundUserApplicationRepo UserAppService { get; set; }
        public UserApplicationViewModel UserAppViewModel { get; set; }

        public UserAppDetailsModel(IERefundUserApplicationRepo _UserAppService, IHostingEnvironment hostingEnvironment)
        {
            UserAppService = _UserAppService;
            UserAppViewModel = new UserApplicationViewModel();
            _hostingEnvironment = hostingEnvironment;
        }
        public async void OnGetAsync(int appid)
        {
            this.appid = appid;

            ERefundUserApplication userapp = await UserAppService.GetAsync(this.appid);
            UserAppViewModel.Id = userapp.Id;
            UserAppViewModel.PatientName = userapp.PatientName;
            UserAppViewModel.PhoneNumber = userapp.PhoneNumber;
            UserAppViewModel.Address = userapp.Address;
            UserAppViewModel.Notes = userapp.Notes;
            UserAppViewModel.Status = userapp.Status.ToString();
            UserAppViewModel.AppliedDate = userapp.DateTime;
            UserAppViewModel.AppliedByUserId = userapp.ApplicationUser.Id;
            UserAppViewModel.AppliedByUserName = userapp.ApplicationUser.UserName;

            string folderName = "Attachments";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName, appid.ToString());
            if (Directory.Exists(newPath))
            {
                UserAppViewModel.Attachs = Directory.GetFiles(newPath);
                for (int i = 0; i < UserAppViewModel.Attachs.Length; i++)
                {
                    UserAppViewModel.Attachs[i] = Path.GetFileName(UserAppViewModel.Attachs[i]);
                }
            }
        }

        public FileResult OnPostDownloadFile(string slipname, UserApplicationViewModel UserAppViewModel)
        {
            string folderName = "Attachments";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName, UserAppViewModel.Id.ToString(), slipname);

            //var newPath = System.IO.Path.Combine(Server.MapPath("/Attachments/"), Id.ToString(), SlipName);
            return PhysicalFile(newPath, "image/jpeg", slipname);
        }

        public IActionResult OnPostAccepted(UserApplicationViewModel UserAppViewModel)
        {
            UserAppService.UpdateStatusAsync(UserAppViewModel.Id, Convert.ToInt32(ApplicationStatus.CRDAccepted));
            return LocalRedirect("~/SucessfullChangesPop");
        }
        public IActionResult OnPostRejected(UserApplicationViewModel UserAppViewModel)
        {
            UserAppService.UpdateStatusAsync(UserAppViewModel.Id, Convert.ToInt32(ApplicationStatus.CRDRejected));
            return LocalRedirect("~/SucessfullChangesPop");
        }
    }
}