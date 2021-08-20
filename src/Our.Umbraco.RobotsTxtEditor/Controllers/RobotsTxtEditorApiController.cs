using System;
using System.Collections.Generic;
using Our.Umbraco.RobotsTxtEditor.Models;

#if NET472
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using Umbraco.Web.WebApi.Filters;
#elif NET5_0_OR_GREATER
using Umbraco.Cms.Web.BackOffice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Extensions;
#endif
namespace Our.Umbraco.RobotsTxtEditor.Controllers
{
    //api route backoffice/RobotsTxtEditor/RobotsTxtEditorApi/GetRobotsText
    [PluginController("RobotsTxtEditor")]
#if NET472
    [UmbracoApplicationAuthorize(Constants.Applications.Settings)]
#endif
    public class RobotsTxtEditorApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public RobotsTxtEditorModel GetRobotsText()
        {

            var filePath = string.Empty;
#if NET472
            filePath = IOHelper.MapPath("~/robots.txt");
#elif NET5_0_OR_GREATER
            filePath = AppContext.BaseDirectory + "/robots.txt";
#endif
            
            if (System.IO.File.Exists(filePath) == false)
            {
                return new RobotsTxtEditorModel
                {
                    FileExists = false
                };
            }

            using (var reader = System.IO.File.OpenText(filePath))
            {
                return new RobotsTxtEditorModel
                {
                    FileExists = true,
                    FileContents = reader.ReadToEnd().TrimEnd()
                };
            }
        }

        [HttpPost]
        public RobotsTxtResponseModel SaveRobotsText(RobotsTxtEditorModel vm)
        {
            //do something that would save it here
            var filePath = string.Empty;
#if NET472
            filePath = IOHelper.MapPath("~/robots.txt");
#elif NET5_0_OR_GREATER
            filePath = AppContext.BaseDirectory + "/robots.txt";
#endif

            var contents = vm.FileContents;

            var errorMessages = ValidateRobotsTxt(contents);

            if (errorMessages.Count == 0)
            {
                //create the file and then write to it
                using (var sw = System.IO.File.CreateText(filePath))
                {
                    sw.WriteLine(contents);
                }
            }

            return new RobotsTxtResponseModel
            {
                ErrorMessages = errorMessages,
                Success = errorMessages.Count == 0
            };
        }

        /// <summary>
		/// A very basic Robots.txt validation.
		/// </summary>
		/// <param name="contents">The contents of the Robots.txt file.</param>
		/// <returns>A list of Pair objects, containing the line number and detail of the error.</returns>
		public List<Pair> ValidateRobotsTxt(string contents)
        {
            var lines = new List<string>(contents.Split(Environment.NewLine.ToCharArray()));
            var errors = new List<Pair>();

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Trim();
                var checkLine = line.ToUpper();

                // check if the line is invalid
                if ((!checkLine.StartsWith("#")) &&
                    (!checkLine.StartsWith("USER-AGENT")) &&
                    (!checkLine.StartsWith("DISALLOW")) &&
                    (!checkLine.StartsWith("ALLOW")) &&
                    (!checkLine.StartsWith("SITEMAP")) &&
                    (!checkLine.StartsWith("CRAWL-DELAY")) &&
                    (!checkLine.StartsWith("REQUEST-RATE")) &&
                    (!checkLine.StartsWith("VISIT-TIME")) && 
                    (!checkLine.IsNullOrWhiteSpace()))
                {
                    // invalid command
                    errors.Add(new Pair(i + 1, line));
                }
            }

            return errors;
        }
    }
}
