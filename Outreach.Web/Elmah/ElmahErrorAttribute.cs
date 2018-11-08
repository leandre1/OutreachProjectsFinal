using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
//using System.Web.Mvc;

namespace Outreach.Web.Elmah
{
    public class ElmahErrorAttribute:HandleErrorAttribute
    {
       

        public override void OnException(ExceptionContext filterContext)
        {
            ///

            //Elmah.
            base.OnException(filterContext);
            if (!filterContext.ExceptionHandled)
                return;
            var httpContext = filterContext.HttpContext.ApplicationInstance.Context;
            var signal = ErrorSignal.FromContext(httpContext);
            signal.Raise(filterContext.Exception, httpContext);
        }
        /// <summary>
        /// Attempt to use ELMAH error signalling prior to raising an exception
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        //private static bool TryRaiseErrorSignal(ExceptionContext context)
        //{
        //    var httpContext = ErrorHandler.
        //}


    }
}