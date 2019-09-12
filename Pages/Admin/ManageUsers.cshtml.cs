using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMedicalClaimRefundSystem.Areas.Identity.Data;
using EMedicalClaimRefundSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMedicalClaimRefundSystem.Pages
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private EMedicalClaimRefundSystemContext _context;
        public ManageUsersModel(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, EMedicalClaimRefundSystemContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public void OnGet()
        {

        }
        public async Task<ActionResult> OnGetUsersAsync()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            foreach (ApplicationUser user in _userManager.Users)
            {
                users.Add(new UserViewModel { Id = user.Id, FullName = user.Name, Email = user.Email, Roles = await _userManager.GetRolesAsync(user) , Blocked = user.Blocked });
            }
            
            return new JsonResult((users));
            //return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(users));
        }

        public async Task<ActionResult> OnPostDeleteUsersAsync([FromBody] List<string> userids)
        {
            foreach(var userid in userids)
            {
                await DeleteUserAsync(userid);
            }

            return new JsonResult("Ok");
        }
        public async Task<ActionResult> OnPostBlockUsersAsync([FromBody] List<string> userids)
        {
            foreach (var userid in userids)
            {
                // await DeleteUserAsync(userid);
                ApplicationUser user = await _userManager.FindByIdAsync(userid);
                if(user.Email != "system@admin.com")
                {
                    user.Blocked = true;
                    await _userManager.UpdateAsync(user);
                }
                
            }

            return new JsonResult("Ok");
        }

        public async Task<ActionResult> OnPostUnBlockUsersAsync([FromBody] string userid)
        {
            
                ApplicationUser user = await _userManager.FindByIdAsync(userid);
                user.Blocked = false;
                await _userManager.UpdateAsync(user);
            

            return new JsonResult("Ok");
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            var logins = await _userManager.GetLoginsAsync(user);
            var rolesForUser = await _userManager.GetRolesAsync(user);
            IdentityResult result = IdentityResult.Success;

            using (var transaction = _context.Database.BeginTransaction())
            {                
                foreach (var login in logins)
                {
                    result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                    if (result != IdentityResult.Success)
                        break;
                }
                if (result == IdentityResult.Success)
                {
                    foreach (var item in rolesForUser)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, item);
                        if (result != IdentityResult.Success)
                            break;
                    }
                }
                if (result == IdentityResult.Success)
                {
                    result = await _userManager.DeleteAsync(user);
                    if (result == IdentityResult.Success)
                        transaction.Commit(); //only commit if user and all his logins/roles have been deleted  
                }
            }
            if (result == IdentityResult.Success)
                return true;
            else
                return false;
        }

        public string GetRolesbyUser(ApplicationUser user)
        {
            string rolestring = "";
            _userManager.GetRolesAsync(user);
            return rolestring;
        }
    }
}