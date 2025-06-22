using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZNParser.Battlezone.GameObject
{

    public class Entity : IMalformable
    {
        protected string PrjID;
        protected bool isUser;
        protected string classLabel;

        private readonly IMalformable.MalformationManager _malformationManager;
        public IMalformable.MalformationManager Malformations => _malformationManager;
        public virtual string ClassLabel { get { return classLabel; } }
        public Entity(string PrjID, bool isUser, string classLabel)
        {
            this.PrjID = PrjID;
            this.isUser = isUser;
            this.classLabel = classLabel;

            this._malformationManager = new IMalformable.MalformationManager(this);
        }
    }
}
