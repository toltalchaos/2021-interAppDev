﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional Namespaces
using System.ComponentModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;
using System.Configuration;
#endregion

namespace WebApp.security
{
    [DataObject]
    public class SecurityController
    {
        #region Constructor & Dependencies
        private readonly ApplicationUserManager UserManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        public SecurityController()
        {
            UserManager = HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        private void CheckResult(IdentityResult result, string item, string action)
        {
            if (!result.Succeeded)
                throw new Exception($"Failed to " + action + $" " + item +
                    $":<ul> {string.Join(string.Empty, result.Errors.Select(x => $"<li>{x}</li>"))}</ul>");
        }
        #endregion
        #region get customer id from security (userREcord) via username
        public int? GetCurrentUserCustomerId(string username)
        {
            int? id = null;
            //get the request object off the current user
            var request = HttpContext.Current.Request;
            if (request.IsAuthenticated) //is the current web app user logged in?
            {
                var manager = request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var appUser = manager.Users.SingleOrDefault(x => x.UserName == username); //return pointer to requested user

                if (appUser != null)
                {
                    id = appUser.CustomerId;
                }
            }

            return id;

        }

        #endregion

    }
}