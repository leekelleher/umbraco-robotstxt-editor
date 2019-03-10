using System.Web.Http;
using System.IO;
using System.Web;
using Umbraco.Core;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Umbraco.Web.WebApi.Filters;
using Our.Umbraco.RobotsTxtEditor.Models;
using System;

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
            var filePath = HttpContext.Current.Server.MapPath("~/robots.txt");

            if (!File.Exists(filePath))
            {
                return new RobotsTxtEditorModel
                {
                    FileExists = false
                };
            }

            using (var reader = File.OpenText(filePath))
            {
                var vm = new RobotsTxtEditorModel
                {
                    FileExists = true,
                    FileContents = reader.ReadToEnd()
                };
                return vm;
            }
        }

        [HttpPost]
        public bool SaveRobotsText(RobotsTxtEditorModel vm)
        {
            //do something that would save it here
            var filePath = HttpContext.Current.Server.MapPath("~/robots.txt");

            //create the file and then write to it
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine(vm.FileContents);
            }
           
            return true;
        }

    }

}
