using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Http.Models;


public class HttpResponse<T> : ActionResult
{

    public int StatusCode { get; set; }

    public string Message { get; set; }
    
    public T Object { get; set; }


    public Task ExecuteResultAsync(ActionContext context)
    {
    

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(Object);

        context.HttpContext.Response.StatusCode = StatusCode;
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.WriteAsync(json);
        return Task.CompletedTask;

    }
}
