using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.UI;
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
        public RobotsTxtResponseModel SaveRobotsText(RobotsTxtEditorModel vm)
        {
            //do something that would save it here
            var filePath = IOHelper.MapPath("~/robots.txt");

            var contents = vm.FileContents;

            var errorMessages = ValidateRobotsTxt(contents);

            if (errorMessages.Count == 0)
            {
                //create the file and then write to it
                using (var sw = File.CreateText(filePath))
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