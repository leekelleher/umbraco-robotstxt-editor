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

            var response = new RobotsTxtResponseModel();

            //create the file and then write to it
            // TODO: This currently always overrides the existing robots.txt file. This is bad. This needs fixing.
            using (var sw = File.CreateText(filePath))
            {
                var contents = vm.FileContents;

                response.ErrorMessage = ValidateRobotsTxt(contents);

                if (response.ErrorMessage.Count > 0)
                {
                    response.Success = false;
                }
                else
                {
                    response.Success = true;
                    sw.WriteLine(vm.FileContents);
                }


                return response;
            }
        }

        /// <summary>
		/// A very basic Robots.txt validation.
		/// </summary>
		/// <param name="contents">The contents of the Robots.txt file.</param>
		/// <returns>A list of Pair objects, containing the line number and detail of the error.</returns>
		public List<Pair> ValidateRobotsTxt(string contents)
        {
            var lines = new List<string>(contents.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            var errors = new List<Pair>();

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i].Trim().ToUpper();
                var checkLine = line.ToUpper();

                // check if the line is invalid
                if ((!checkLine.StartsWith("#")) &&
                    (!checkLine.StartsWith("USER-AGENT")) &&
                    (!checkLine.StartsWith("DISALLOW")) &&
                    (!checkLine.StartsWith("ALLOW")) &&
                    (!checkLine.StartsWith("SITEMAP")) &&
                    (!checkLine.StartsWith("CRAWL-DELAY")) &&
                    (!checkLine.StartsWith("REQUEST-RATE")) &&
                    (!checkLine.StartsWith("VISIT-TIME")))
                {
                    // invalid command
                    errors.Add(new Pair(i, line));
                }
            }

            return errors;
        }
    }
}