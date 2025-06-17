using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZNParser.Battlezone
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ObjectClassAttribute : Attribute
    {
        public BZNFormat Format { get; set; }
        public string ClassName { get; set; }

        public ObjectClassAttribute(BZNFormat format, string className)
        {
            Format = format;
            ClassName = className;
        }
    }
}
