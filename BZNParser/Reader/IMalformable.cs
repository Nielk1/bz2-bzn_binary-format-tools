namespace BZNParser.Reader
{
    public enum Malformation
    {
        UNKNOWN = 0,
        INCOMPAT,       // fieldName                 // Not loadable by game
        MISINTERPRET,   // fieldName, interpretedAs  // Misinterpreted by game but thus is loadable
        OVERCOUNT,      // fieldName                 // Too many objects of this type, maximum may have changed
        NOTIMPLEMENTED, // fieldName                 // Field not implemented, but it probably won't break the BZN read
        INCORRECT,      // fieldName, incorrectValue // Value is incorrect and has been corrected
    }
    public interface IMalformable
    {
        public MalformationManager Malformations { get; }
        public class MalformationManager
        {
            private readonly IMalformable _parent;
            private readonly Dictionary<string, List<(Malformation, string, object[])>> malformations;

            public (Malformation, string, object[])[] this[string property]
            {
                get
                {
                    if (malformations.TryGetValue(property, out var list))
                    {
                        return list.ToArray();
                    }
                    return Array.Empty<(Malformation, string, object[])>();
                }
            }
            public MalformationManager(IMalformable parent)
            {
                _parent = parent;
                malformations = new Dictionary<string, List<(Malformation, string, object[])>>();
            }
            public void Add(Malformation malformation, string property, params object[] fields)
            {
                if (!malformations.ContainsKey(property))
                {
                    malformations[property] = new List<(Malformation, string, object[])>();
                }
                malformations[property].Add((malformation, property, fields));
            }
        }
    }
}
