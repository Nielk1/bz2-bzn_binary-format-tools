using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    public class MultiClass : ClassGameObject
    {
        private List<(ClassGameObject Object, bool Expected, long Next, string Name)> Candidates;
        public override string ClassLabel { get { return $"[{string.Join(',', Candidates.Select(dr => dr.Object.ClassLabel))}]"; } }


        public MultiClass(string PrjID, bool isUser, List<(ClassGameObject Object, bool Expected, long Next, string Name)> candidates) : base(PrjID, isUser, null)
        {
            this.Candidates = candidates;
        }
        public void LoadData(BZNStreamReader reader)
        {
            reader.BaseStream.Position = Candidates[0].Next;
            //base.LoadData(reader);
        }
        public override string ToString()
        {
            return $"{base.ToString()} [{string.Join(',', Candidates.Select(dr => dr.Object.ToString()))}]";
        }
    }
}
