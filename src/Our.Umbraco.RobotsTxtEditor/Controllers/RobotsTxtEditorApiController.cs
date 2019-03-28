using System.IO;
using System.Web.Http;
using Our.Umbraco.RobotsTxtEditor.Models;
using Umbraco.Core;
using Umbraco.Core.IO;
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
        public RobotsTxtEditorModel GetRobotsText()
        {
            var filePath = IOHelper.MapPath("~/robots.txt");

            if (File.Exists(filePath) == false)
            {
                return new RobotsTxtEditorModel
                {
                    FileExists = false
                };
            }

            using (var reader = File.OpenText(filePath))
            {
                return new RobotsTxtEditorModel
                {
                    FileExists = true,
                    FileContents = reader.ReadToEnd()
                };
            }
        }

        [HttpPost]
        public bool SaveRobotsText(RobotsTxtEditorModel vm)
        {
            //do something that would save it here
            var filePath = IOHelper.MapPath("~/robots.txt");

            //create the file and then write to it
            using (var sw = File.CreateText(filePath))
            {
                sw.WriteLine(vm.FileContents);
            }

            return true;
        }
    }
}