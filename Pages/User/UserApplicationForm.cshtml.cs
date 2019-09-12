using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using EMedicalClaimRefundSystem.Areas.Identity.Data;
using EMedicalClaimRefundSystem.Models;
using EMedicalClaimRefundSystem.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMedicalClaimRefundSystem.Pages
{
    [Authorize (Roles = "User")]
    public class UserApplicationFormModel : PageModel
    {
        private IERefundUserApplicationRepo _refundapp;
        private IHostingEnvironment _hostingEnvironment;
        public UserApplicationFormModel(IERefundUserApplicationRepo refundapp, IHostingEnvironment hostingEnvironment)
        {
            _refundapp = refundapp;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            public string PatientName { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Application Notes")]
            public string AppNote { get; set; }
            public List<IFormFile> SlipsImages { get; set; }

        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public IActionResult OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ERefundUserApplication
                {
                    PatientName = Input.PatientName,
                    PhoneNumber = Input.PhoneNumber,
                    Address = Input.Address,
                    Notes = Input.AppNote,
                    Status = (int)ApplicationStatus.UserClaimed,
                    DateTime = DateTime.Now,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                _refundapp.InsertAsync(user);

                return LocalRedirect(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task<JsonResult> OnPostUploadAsync(List < IFormFile> files,string Patient, string Phone, string Address, string AppNote, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ERefundUserApplication
                {
                    PatientName = Patient,
                    PhoneNumber = Phone,
                    Address = Address,
                    Notes = AppNote,
                    Status = (int)ApplicationStatus.UserClaimed,
                    DateTime = DateTime.Now,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value
                };
                var id = await _refundapp.InsertAsync(user);

                //return LocalRedirect(returnUrl);            

                // If we got this far, something failed, redisplay form
                //return Page();
                //List<IFormFile> files = inputM.SlipsImages;
                if (files != null && files.Count > 0)
                {
                    string folderName = "Attachments";
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string newPath = Path.Combine(webRootPath, folderName, id.ToString());
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }
                    foreach (IFormFile item in files)
                    {
                        if (item.Length > 0)
                        {
                            string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                            string fullPath = Path.Combine(newPath, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);
                            }
                        }
                    }                    
                }
                //return LocalRedirect(returnUrl);
                return new JsonResult(new { result = "Redirect", url = returnUrl });
            }
            return new JsonResult(new { result = "Error", Message = "Error Found in Model State" });
        }


    }
}