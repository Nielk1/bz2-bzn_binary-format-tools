using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    public class ClassUnknownGameObject : ClassGameObject
    {
        private List<(long Offset, IBZNToken Token, long Next)> gameObjectTokens = null;

        public ClassUnknownGameObject(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            base.LoadData(reader);
        }

        internal void AddUnknownTokens(List<(long Offset, IBZNToken Token, long Next)> gameObjectTokens)
        {
            this.gameObjectTokens = gameObjectTokens;
        }
    }
}
