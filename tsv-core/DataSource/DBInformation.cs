using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tsv_core.Models
{
    public class DBInformation
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AppUser> userManager;
        private Object lockObj = new Object();

        public DBInformation(RoleManager<IdentityRole> rm, UserManager<AppUser> um)
        {
            roleManager = rm;
            userManager = um;
        }

        public List<IdentityRole> Roles
        {
            get
            {
                lock (lockObj)
                {
                    return roleManager.Roles.ToList();
                }
            }
        }

        public List<AppUser> Users
        {
            get
            {
                lock (lockObj)
                {                    
                    return userManager.Users.ToList();
                }
            }
        }      

        public bool IsUserInRole(AppUser user, IdentityRole role)
        {
            lock (lockObj)
            {
                return userManager.IsInRoleAsync(user, role.Name).Result;
            }
        }

        public IdentityRole FindRoleById(String roleId)
        {
            return Roles.Where(r => r.Id == roleId).ToList().FirstOrDefault();
        }
    }
}
