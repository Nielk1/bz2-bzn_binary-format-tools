using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



        private readonly MalformationManager _malformationManager;
        public MalformationManager Malformations => _malformationManager;
        public class MalformationManager
        {
            private readonly Entity _parent;
            private readonly Dictionary<string, List<(Malformation, string)>> malformations;

            public (Malformation, string)[] this[string property]
            {
                get
                {
                    if (malformations.TryGetValue(property, out var list))
                    {
                        return list.ToArray();
                    }
                    return Array.Empty<(Malformation, string)>();
                }
            }
            public MalformationManager(Entity parent)
            {
                _parent = parent;
                malformations = new Dictionary<string, List<(Malformation, string)>>();
            }
            public void Add(Malformation malformation, string property)
            {
                if (!malformations.ContainsKey(property))
                {
                    malformations[property] = new List<(Malformation, string)>();
                }
                malformations[property].Add((malformation, _parent.PrjID));
            }
        }
        public virtual string ClassLabel { get { return classLabel; } }
        public Entity(string PrjID, bool isUser, string classLabel)
        {
            this.PrjID = PrjID;
            this.isUser = isUser;
            this.classLabel = classLabel;

            this._malformationManager = new MalformationManager(this);
        }
    }
}
