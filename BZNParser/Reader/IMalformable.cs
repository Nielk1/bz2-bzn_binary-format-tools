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
        public struct MalformationData
        {
            public Malformation Type { get; }
            public string Property { get; }
            public object[] Fields { get; }
            public MalformationData(Malformation type, string property, object[] fields)
            {
                Type = type;
                Property = property;
                Fields = fields;
            }
        }

        public MalformationManager Malformations { get; }
        public class MalformationManager
        {
            private readonly IMalformable _parent;
            private readonly Dictionary<string, List<MalformationData>> malformations;

            public MalformationData[] this[string property]
            {
                get
                {
                    if (malformations.TryGetValue(property, out var list))
                    {
                        return list.ToArray();
                    }
                    return Array.Empty<MalformationData>();
                }
            }
            public string[] Keys => malformations.Keys.ToArray();
            public MalformationManager(IMalformable parent)
            {
                _parent = parent;
                malformations = new Dictionary<string, List<MalformationData>>();
            }
            public void Add(Malformation malformation, string property, params object[] fields)
            {
                if (!malformations.ContainsKey(property))
                {
                    malformations[property] = new List<MalformationData>();
                }
                malformations[property].Add(new MalformationData(malformation, property, fields));
            }
            public void Clear()
            {
                malformations.Clear();
            }
        }
    }
}
