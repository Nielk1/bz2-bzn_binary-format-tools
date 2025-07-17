namespace BZNParser.Reader
{
    public enum Malformation
    {
        UNKNOWN = 0,
        INCOMPAT,        // ?????????                         // Not loadable by game
        MISINTERPRET,    // <fieldName>,     <interpretedAs>  // Misinterpreted by game but thus is loadable
        OVERCOUNT,       // <fieldName>                       // Too many objects of this type, maximum may have changed
        NOT_IMPLEMENTED, // <fieldName>                       // Field not implemented, but it probably won't break the BZN read
        INCORRECT,       // <fieldName>,     <incorrectValue> // Value is incorrect and has been corrected
        LINE_ENDING,     // MAL_LINE_ENDING, <incorrectValue> // Line ending is incorrect, "CR" for all "CR"s, "LF" for all "LF"s, "?" for other counts
    }
    public interface IMalformable
    {
        public const string MAL_LINE_ENDING = "ALL:LINE_ENDING";

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
            private readonly Stack<Dictionary<string, List<MalformationData>>> malformations;

            public MalformationData[] this[string property]
            {
                get
                {
                    return malformations
                        .SelectMany(stack => stack)
                        .Where(kvp => kvp.Key == property)
                        .SelectMany(kvp => kvp.Value)
                        .ToArray();
                }
            }
            public string[] Keys => malformations.SelectMany(stack => stack.Keys).ToArray();
            public MalformationManager(IMalformable parent)
            {
                _parent = parent;
                malformations = new Stack<Dictionary<string, List<MalformationData>>>();
                malformations.Push(new Dictionary<string, List<MalformationData>>());
            }
            public void Add(Malformation malformation, string property, params object[] fields)
            {
                if (!malformations.Peek().ContainsKey(property))
                {
                    malformations.Peek()[property] = new List<MalformationData>();
                }
                malformations.Peek()[property].Add(new MalformationData(malformation, property, fields));
            }
            public void Push()
            {
                 malformations.Push(new Dictionary<string, List<MalformationData>>());
            }
            public void Pop()
            {
                if (malformations.Count > 1)
                {
                    Dictionary<string, List<MalformationData>> old = malformations.Pop();
                    foreach (var kvp in old)
                    {
                        if (!malformations.Peek().ContainsKey(kvp.Key))
                        {
                            malformations.Peek()[kvp.Key] = new List<MalformationData>();
                        }
                        malformations.Peek()[kvp.Key].AddRange(kvp.Value);
                    }
                }
            }
            public void Discard()
            {
                if (malformations.Count > 1)
                {
                    malformations.Pop();
                }
            }
        }
    }
}
