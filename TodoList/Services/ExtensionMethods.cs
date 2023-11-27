using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TodoList.Services
{
    public static class ExtensionMethods
    {
        /// <summary>
         /// User ID
         /// </summary>
         /// <param name="user"></param>
         /// <returns></returns>
        public static string getUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
