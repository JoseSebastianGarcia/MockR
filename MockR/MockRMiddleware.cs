using Microsoft.AspNetCore.Http;
using MockR.Dtos;
using MockR.Request;
using MockR.Service;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MockR
{
    public class MockRMiddleware
    {
        private readonly RequestDelegate _next;

        public MockRMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IMockRService service)
        {
            //Si no empieza por este segmento, no me sirve, siguiente.
            if (context.Request.Path != "/mockr") { await _next(context); return; }

            ProcessPostRequest(context, service);
            await ProcessGetRequest(context, service);

        }

        private async Task ProcessGetRequest(HttpContext context, IMockRService service)
        {
            if (context.Request.Method == HttpMethods.Get)
            {
                string htmlCode = $@"
<!DOCTYPE html>
<html>
    <head>
        <title>MockR</title>

        <style>
            *
            {{
                margin: 0px;
                padding: 10px;
                font-family: Verdana;
                font-size: 14px;
                color: #636363;
                border-radius: 10px;
            }}

            .btn
            {{
                border: none;
                background-color: #5B58E6;
                color: white;
                cursor: pointer;
            }}

            .method.GET
            {{
                background-color: #00BFFF;
                color: white;
            }}
            .method.POST
            {{
                background-color: #09B309;
                color: white;
            }}
            .method.PUT
            {{
                background-color: #FFA500;
                color: white;
            }}
            .method.DELETE
            {{
                background-color: #E65858;
                color: white;
            }}
            .items
            {{
                position: fixed; 
                right: 10px; 
                width: 400px; 
                top: 10px;
                bottom: 10px;
                box-shadow: 0px 0px 6px 0px #c9c9c9;
                display: block;
                overflow-y: scroll;
                overflow-y: auto;
            }}
            .item
            {{
                position: relative;
                width: 100%;
                display: block;
            }}
            textarea
            {{
                font-size: 12px;
            }}
        </style>
    </head>
    <body>
        <form action=""/mockr"" method=""POST"" enctype=""application/x-www-form-urlencoded"">
            <table cellspacing=""0"" cellspadding=""0"" border=""0"">
                <tr>
                    <td align=""right"">Path</td>
                    <td>/v1/mocks <input type=""text"" name=""Path"" id=""Path"" placeholder=""/users/me"" /></td>
                </tr>
                <tr>
                    <td align=""right"">Method</td>
                    <td>
                        <select name=""Method"" id=""Method"">
                            <option value=""GET"">GET</option>
                            <option value=""POST"">POST</option>
                            <option value=""DELETE"">DELETE</option>
                            <option value=""PUT"">PUT</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td align=""right"">Body</td>
                    <td>
                        <textarea name=""Value"" id=""Value"" cols=""50"" rows=""15"">
{{
    ""name"":""Juan"",
    ""lastName"":""Dracula""
}}
                        </textarea>
                    </td>
                </tr>
                <tr>
                    <td colspan=""2"" align=""right""><input type=""submit"" value=""Guardar"" class=""btn""/></td>
                </tr>   
            </table>
        </form>
        <div class=""items"">
            <table cellspacing=""0"" cellspadding=""0"" border=""0"" width=""100%"">
                [CONTENT]
            </table>
        </div>
    </body>
</html>
";
                string items = string.Empty;

                
                
                service.GetAll().ForEach(x => {
                    items += RenderControl(x);
                });

                htmlCode = htmlCode.Replace("[CONTENT]", items);

                context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, OPTIONS, PUT, PATCH");
                context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type, Accept");

                context.Response.ContentType = "text/html";
                context.Response.ContentLength = htmlCode.Length;

                await context.Response.WriteAsync(htmlCode, UTF8Encoding.UTF8);
            }
        }

        private string RenderControl(PageDto dto) => @$"
            <tr>
                <td align=""center""><span class=""method {dto.Method}"">{dto.Method}</span></td>
                <td align=""left""><a href=""{dto.AbsolutePath}"" target=""_blank"">{dto.AbsolutePathReduced}</a></td>
                <td align=""right"">
                    <form action=""/mockr"" method=""POST"" enctype=""application/x-www-form-urlencoded"">
                        <input type=""hidden"" name=""Method"" id=""Method"" value=""DELETE"" /> 
                        <input type=""hidden"" name=""Id"" id=""Id"" value=""{dto.Id}"" /> 
                        <input type=""submit"" value=""Eliminar"" class=""btn"" /> 
                    </form>        
                </td>
            </tr>
        ";
        

        private void ProcessPostRequest(HttpContext context, IMockRService service)
        {
            if (context.Request.Method == HttpMethods.Post)
            {
                string method = context.Request.Form["Method"];

                if (method == "DELETE") 
                {
                    Guid id = Guid.Parse(context.Request.Form["Id"]);
                    service.Delete(id);
                    context.Response.Redirect("/mockr");
                }
                else
                {
                    PathString path = context.Request.Form["Path"].ToString();
                    string value = context.Request.Form["Value"];

                    if (!ValidJson(value))
                    {
                        context.Response.Redirect("/mockr");
                        return;
                    }

                    service.Create(new CreateRequest()
                    {
                        Method = method,
                        AbsolutePath = $"/v1/mocks{path}",
                        Body = value
                    });

                }
                context.Response.Redirect("/mockr");
            }
        }

        private bool ValidJson(string json) 
        {
            bool isValid;

            try
            {
                JsonDocument.Parse(json);
                isValid = true;
            }
            catch (Exception ex) 
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
