using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreProjectsList.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            RestoreError();
        }

        private void RestoreError()
        {
            if(TempData["Error"] != null)
            {
                var error = (string) TempData["Error"];
                ModelState.AddModelError(string.Empty, error);
            }
        }

        protected void AddError(string errorMessage)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
        }

        protected void AddTemporaryError(string errorMessage)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            TempData["Error"] = errorMessage;
        }
    }
}