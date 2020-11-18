using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data;
using TestApp.Services;

namespace TestApp.Filters
{
    public class ActionResultFilter : ActionFilterAttribute
    {
        ICustomerCount _customerCount;
        public ActionResultFilter(ICustomerCount customerCount)
        {
            _customerCount = customerCount;
        }

        private void GenerateMessage(FilterContext context, string event_name)
        {
            string controller = context.ActionDescriptor.RouteValues["controller"];
            string action = context.ActionDescriptor.RouteValues["action"];

            string message = $"{event_name}: this is {controller} controller, {action} action, current number of customers is: {_customerCount.GetCustomerNumber()}";
            Debug.WriteLine(message);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            GenerateMessage(context, "OnActionExecuting");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            GenerateMessage(context, "OnActionExecuted");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            GenerateMessage(context, "OnResultExecuting");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            GenerateMessage(context, "OnResultExecuted");
        }
    }
}
