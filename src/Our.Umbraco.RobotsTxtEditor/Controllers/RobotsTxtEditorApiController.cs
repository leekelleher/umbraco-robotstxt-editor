using System.Web.Http;
using System.IO;
using System.Web;
using Umbraco.Core;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Umbraco.Web.WebApi.Filters;

namespace Our.Umbraco.RobotsTxtEditor.Controllers
{
    //api route backoffice/RobotsTxtEditor/RobotsTxtEditorApi/GetRobotsText
    [PluginController("RobotsTxtEditor")]
    [UmbracoApplicationAuthorize(Constants.Applications.Settings)]
    public class RobotsTxtEditorApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public string GetRobotsText()
        {
            var filePath = HttpContext.Current.Server.MapPath("~/robots.txt");

            if (!File.Exists(filePath))
            {
                return null;
            }

            using (var reader = File.OpenText(filePath))
            {
                return reader.ReadToEnd();
            }
        }
    }

}
