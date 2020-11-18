using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using System.Text;
using FiscalServer.Models;

namespace FiscalServer.CustomFormaters.Output
{
    public class CustomProductOutputFormater : TextOutputFormatter
    {
        public CustomProductOutputFormater()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/customperson"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(Product).IsAssignableFrom(type) || typeof(IEnumerable<Product>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            StringBuilder builder = new StringBuilder();

            if(context.Object is IEnumerable<Product>)
            {
                foreach(var p in context.Object as IEnumerable<Product>)
                {
                    WriteProduct(builder, p);
                }
            }
            else
            {
                Product p = context.Object as Product;
                WriteProduct(builder, p);
            }

           await context.HttpContext.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(builder.ToString()));
        }

        private void WriteProduct(StringBuilder builder, Product product)
        {
            builder.AppendLine("BEGIN:CUSTOM_PRODUCT");
            builder.AppendLine($"ID:{product.ProductId};Name:{product.Name};Price:{product.Price}");
            builder.AppendLine("END:CUSTOM_PRODUCT");
        }
    }
}
