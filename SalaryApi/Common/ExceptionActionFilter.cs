using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Serilog;



namespace SalaryApi.Common
{
    
    public class ExceptionActionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled) return;
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)BaseResult.Error.HttpStatus;


            Log.Error(context.Exception.Message );

            var obj = BaseApiResult.Result(BaseResult.Error);

            context.Result = new JsonResult(obj);
            context.ExceptionHandled = true;
            base.OnException(context);
        }


        
    }
}

