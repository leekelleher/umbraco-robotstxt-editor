using System.Collections.Generic;
using System.Web.UI;

namespace Our.Umbraco.RobotsTxtEditor.Models
{
    public class RobotsTxtResponseModel
    {
        public bool Success { get; set; }
        public List<Pair> ErrorMessages { get; set; }
    }
}
