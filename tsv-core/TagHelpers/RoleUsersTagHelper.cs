using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;
using tsv_core.Models;

namespace tsv_core.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private DBInformation dbInformation;
        private object lockObj = new object();

        public RoleUsersTagHelper(UserManager<AppUser> usermgr, RoleManager<IdentityRole> rolemgr, DBInformation dbi)
        {
            userManager = usermgr;
            roleManager = rolemgr;
            dbInformation = dbi;
        }
        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }

        //public override async Task ProcessAsync(TagHelperContext context,
        //TagHelperOutput output)
        //{
        //        IdentityRole role = await roleManager.FindByIdAsync(Role);
        //        List<string> names = new List<string>();

        //        if (role != null)
        //        {
        //            foreach (var user in userManager.Users)
        //            {
        //                if (user != null && await userManager.IsInRoleAsync(user, role.Name))
        //                {
        //                    names.Add(user.UserName);
        //                }
        //            }
        //        }
        //        output.Content.SetContent(names.Count == 0 ?
        //        "No Users" : string.Join(", ", names));
        //}

        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    lock (lockObj)
        //    {
        //        IdentityRole role = roleManager.FindByIdAsync(Role).Result;


        //        List<string> names = new List<string>();

        //        if (role != null)
        //        {
        //            foreach (var user in userManager.Users)
        //            {
        //                if (user != null && userManager.IsInRoleAsync(user, role.Name).Result)
        //                {
        //                    names.Add(user.UserName);
        //                }
        //            }
        //        }
        //        output.Content.SetContent(names.Count == 0 ?
        //        "No Users" : string.Join(", ", names));
        //    }
        //}

        public override void Process(TagHelperContext context,
        TagHelperOutput output)
        {
            IdentityRole role = dbInformation.FindRoleById(Role);
            List<string> names = new List<string>();

            if (role != null)
            {
                foreach (var user in dbInformation.Users)
                {
                    if (user != null && dbInformation.IsUserInRole(user, role))
                    {
                        names.Add(user.UserName);
                        //names.AddRange(user.Roles.Select(r => r.RoleId));
                        //names.Add(user.Roles.Count.ToString());
                    }
                }
            }
            output.Content.SetContent(names.Count == 0 ?
            "No Users" : string.Join(", ", names));
        }
    }
}
