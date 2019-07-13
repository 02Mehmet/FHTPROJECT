using Employee.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Employee.UI.session
{
    public class helper
    {
        public static user add_user(HttpContext ctx, user usr)
        {
            ctx.Session["user"] = usr;
            return usr;
        }
        public static user get_user(HttpContext ctx)
        {
            try
            {
                if (ctx.Session["user"] != null)
                    return ctx.Session["user"] as user;
            }
            catch (Exception)
            {

            }
            return null;
        }
        public static void remove_user(HttpContext ctx)
        {
            ctx.Session["user"] = null;
        }
    }
}