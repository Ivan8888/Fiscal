using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebShop.Services;

namespace WebShop.Filters
{
    public class ActionResultFilters : ActionFilterAttribute 
    {
        IProductCount _productCount;
        public ActionResultFilters(IProductCount productCount)
        {
            _productCount = productCount;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string action_name = context.ActionDescriptor.RouteValues["action"];
            int product_number = _productCount.GetProductNumber();
            Debug.WriteLine($"OnActionExecuting: {action_name} Product count: {product_number}");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string action_name = context.ActionDescriptor.RouteValues["action"];
            int product_number = _productCount.GetProductNumber();
            Debug.WriteLine($"OnActionExecuted: {action_name} Product count: {product_number}");
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Debug.WriteLine("OnResultExecuting");
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            ContentResult result = (ContentResult)context.Result;
            Debug.WriteLine($"OnResultExecuted result is: {result.Content}");
        }
    }
}
