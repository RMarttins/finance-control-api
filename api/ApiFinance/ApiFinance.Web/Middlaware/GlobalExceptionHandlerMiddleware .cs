using ApiFinance.Domain.ClientReponse;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ApiFinance.Web.Middlaware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            var errorCod = DateTime.Now.Ticks.ToString();

            var resultReturn = new DefaultResponse
            {
                Message = exception.Message,
                Status = "error",
                Result = "Identificamos um erro interno. Codigo: " + errorCod,
                Exception = exception
            };
            
            return context.Response.WriteAsync(JsonConvert.SerializeObject(resultReturn));
        }

        private static string RequestBody(HttpContext context)
        {
            using (var bodyStream = new StreamReader(context.Request.Body))
            {
                bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                var bodyText = bodyStream.ReadToEnd();
                return bodyText;
            }
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                context.Request.EnableBuffering();
                await next?.Invoke(context);

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
    }
}