using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZNParser.Battlezone.GameObject
{
    public class Entity
    {
        protected string PrjID;
        protected bool isUser;
        protected string classLabel;
        public virtual string ClassLabel { get { return classLabel; } }
        public Entity(string PrjID, bool isUser, string classLabel)
        {
            this.PrjID = PrjID;
            this.isUser = isUser;
            this.classLabel = classLabel;
        }
    }
}
