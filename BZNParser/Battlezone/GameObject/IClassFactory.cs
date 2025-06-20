using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    public interface IClassFactory
    {
        bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true);
    }
}
