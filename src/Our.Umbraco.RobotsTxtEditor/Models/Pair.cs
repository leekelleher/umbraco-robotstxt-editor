using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.RobotsTxtEditor.Models
{
    public class Pair
    {
        //
        // Summary:
        //     Gets or sets the first object of the object pair.
        public object First;
        //
        // Summary:
        //     Gets or sets the second object of the object pair.
        public object Second;

        //
        // Summary:
        //     Creates a new, uninitialized instance of the System.Web.UI.Pair class.
        //
        public Pair() { }
        // Summary:
        //     Initializes a new instance of the System.Web.UI.Pair class, using the specified
        //     object pair.
        //
        // Parameters:
        //   x:
        //     An object.
        //
        //   y:
        //     An object.
        public Pair(object x, object y) {
            First = x;
            Second = y;
        }
    }
}
