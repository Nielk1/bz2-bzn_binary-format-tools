using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    public class MultiClass : Entity
    {
        private List<(Entity Object, bool Expected, long Next, string Name)> Candidates;
        public override string ClassLabel { get { return $"[{string.Join(',', Candidates.Select(dr => dr.Object.ClassLabel))}]"; } }


        public MultiClass(BZNGameObjectWrapper preamble, List<(Entity Object, bool Expected, long Next, string Name)> candidates) : base(preamble, null)
        {
            this.Candidates = candidates;
        }
        public override string ToString()
        {
            return $"{base.ToString()} [{string.Join(',', Candidates.Select(dr => dr.Object.ToString()))}]";
        }
    }
}
