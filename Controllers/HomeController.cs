using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.SpaServices.Prerendering;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;

namespace new_renderer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index2()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var nodeServices = Request.HttpContext.RequestServices.GetRequiredService<INodeServices>();
            var hostEnv = Request.HttpContext.RequestServices.GetRequiredService<IHostingEnvironment>();

            var applicationBasePath = hostEnv.ContentRootPath;
            var requestFeature = Request.HttpContext.Features.Get<IHttpRequestFeature>();
            var unencodedPathAndQuery = requestFeature.RawTarget;
            var unencodedAbsoluteUrl = $"{Request.Scheme}://{Request.Host}{unencodedPathAndQuery}";

            // ** TransferData concept **
            // Here we can pass any Custom Data we want !

            // By default we're passing down Cookies, Headers, Host from the Request object here
            //TransferData transferData = new TransferData();
            //transferData.request = AbstractHttpContextRequestInfo(Request);
            //transferData.thisCameFromDotNET = "Hi Angular it's asp.net :) " + DateTime.Now.ToString();
            // Add more customData here, add it to the TransferData class

            var transferData = new { msg = "Hello Tariq", entryHtml = "<cc-my-dir></cc-my-dir>" };

            var tick1 = Environment.TickCount;
            // Prerender / Serialize application (with Universal)
            var prerenderResult = await Prerenderer.RenderToString(
                "/",
                nodeServices,
                new JavaScriptModuleExport(applicationBasePath + "/build/app.bundle"),
                unencodedAbsoluteUrl,
                unencodedPathAndQuery,
                transferData, // Our simplified Request object & any other CustommData you want to send!
                30000,
                Request.PathBase.ToString()
            );

            ViewData["SpaHtml"] = prerenderResult.Html; // our <app> from Angular
            //ViewData["Title"] = prerenderResult.Globals["title"]; // set our <title> from Angular
            //ViewData["Styles"] = prerenderResult.Globals["styles"]; // put styles in the correct place
            //ViewData["Meta"] = prerenderResult.Globals["meta"]; // set our <meta> SEO tags
            //ViewData["Links"] = prerenderResult.Globals["links"]; // set our <link rel="canonical"> etc SEO tags
            ViewData["TransferData"] = prerenderResult.Globals["transferData"]; // our transfer data set to window.TRANSFER_CACHE = {};

            var tick2 = Environment.TickCount - tick1;

            ViewData["TimeTaken"] = tick2;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
