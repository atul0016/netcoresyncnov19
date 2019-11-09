using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CoreAppWeb.CusrtomFilters
{
    public class AppExceptionFilter : IExceptionFilter
    {
        // the interface that is used to reflect over the Model class
        // that is to be passed to view.
        // If view does not have any model then this interface can use
        // developer defined ViewDataDictionary
        private readonly IModelMetadataProvider modelMetadataProvider;
        public AppExceptionFilter(IModelMetadataProvider m)
        {
            modelMetadataProvider = m;
        }

        public void OnException(ExceptionContext context)
        {
            // .1. Handle Exception
            context.ExceptionHandled = true;

            // 2. Plan for View
            var viewResult = new ViewResult() { ViewName = "CustomError" };
            viewResult.ViewData = new ViewDataDictionary(modelMetadataProvider, context.ModelState);
            viewResult.ViewData["controller"] = context.RouteData.Values["controller"].ToString();
            viewResult.ViewData["action"] = context.RouteData.Values["action"].ToString();
            viewResult.ViewData["message"] = context.Exception.Message;
            context.Result = viewResult;
        }
    }
}
