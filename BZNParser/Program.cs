using BZNParser.Battlezone;
using BZNParser.Reader;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

namespace BZNParser
{
    internal class Program
    {
        record struct BznType(int version, bool binary, BZNFormat format);
        static void Main(string[] args)
        {
            BattlezoneBZNHints BZ1Hints = new BattlezoneBZNHints();
            BZ1Hints.Strict = true;
            BZ1Hints.ClassLabels = new Dictionary<string, HashSet<string>>();
            if (File.Exists("ClassLabels_BZ1.txt"))
            {
                HashSet<string> ValidClassLabelsBZ1 = new HashSet<string>();
                foreach (Type type in typeof(BZNFileBattlezone).Assembly.GetTypes())
                {
                    var attrs = type.GetCustomAttributes(typeof(ObjectClassAttribute), true);
                    foreach (ObjectClassAttribute attr in attrs)
                        if (attr.Format == BZNFormat.Battlezone || attr.Format == BZNFormat.BattlezoneN64)
                            ValidClassLabelsBZ1.Add(attr.ClassName);
                }

                foreach (string line in File.ReadAllLines("ClassLabels_BZ1.txt"))
                {
                    string[] parts = line.Split(new char[] { '\t' }, 3);
                    if (parts.Length == 2 || parts.Length == 3)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        if (!ValidClassLabelsBZ1.Contains(value))
                            continue;
                        if (!BZ1Hints.ClassLabels.ContainsKey(key))
                            BZ1Hints.ClassLabels[key] = new HashSet<string>();
                        BZ1Hints.ClassLabels[key].Add(value);
                    }
                }
            }

            BattlezoneBZNHints BZ2Hints = new BattlezoneBZNHints();
            BZ2Hints.Strict = true;
            BZ2Hints.ClassLabels = new Dictionary<string, HashSet<string>>();
            if (File.Exists("ClassLabels_BZ2.txt"))
            {
                foreach (string line in File.ReadAllLines("ClassLabels_BZ2.txt"))
                {
                    string[] parts = line.Split(new char[] { '\t' }, 3);
                    if (parts.Length == 2 || parts.Length == 3)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        if (!BZ2Hints.ClassLabels.ContainsKey(key))
                            BZ2Hints.ClassLabels[key] = new HashSet<string>();
                        BZ2Hints.ClassLabels[key].Add(value);
                    }
                }

                HashSet<string> ValidClassLabelsBZ2 = new HashSet<string>();
                foreach (Type type in typeof(BZNFileBattlezone).Assembly.GetTypes())
                {
                    var attrs = type.GetCustomAttributes(typeof(ObjectClassAttribute), true);
                    foreach (ObjectClassAttribute attr in attrs)
                        if (attr.Format == BZNFormat.Battlezone2)
                            ValidClassLabelsBZ2.Add(attr.ClassName);
                }
                foreach (string key in BZ2Hints.ClassLabels.Keys.ToList())
                    if (ValidClassLabelsBZ2.Contains(key))
                        BZ2Hints.ClassLabels[key].Add(key);
                for (; ; )
                {
                    bool found = false;
                    int size = 0;
                    foreach (string key in BZ2Hints.ClassLabels.Keys.ToList())
                    {
                        HashSet<string> classLabels = BZ2Hints.ClassLabels[key];

                        // these classLabels might be other ODF names instead of class labels, so lets expand them
                        foreach (string item in classLabels.ToList()) // make a new list so we can alter it while looping
                        {
                            if (!ValidClassLabelsBZ2.Contains(item))
                            {
                                // if it's not a valid class label and is thus just an ODF name, remove it from the options
                                // we will still try to walk it to valid classlabels below
                                classLabels.Remove(item);
                            }
                            if (BZ2Hints.ClassLabels.ContainsKey(item))
                            {
                                HashSet<string> newClassLabels = BZ2Hints.ClassLabels[item];
                                foreach (string newLabel in newClassLabels)
                                {
                                    if (!found && !classLabels.Contains(newLabel))
                                    {

                                    }
                                    found = classLabels.Add(newLabel) || found;
                                }
                            }
                        }

                        //BZ2Hints.ClassLabels[key] = classLabels;
                        size += classLabels.Count;
                    }
                    if (!found)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Cleaning BZ2 ClassLabels {size}");
                    }
                }
            }

            HashSet<string> Success = new HashSet<string>();
            if (File.Exists("success.txt"))
                foreach (string line in File.ReadAllLines("success.txt"))
                    Success.Add(line);
            Dictionary<BznType, List<(string, bool)>> Files = new Dictionary<BznType, List<(string, bool)>>();

            foreach (string filename in Directory.EnumerateFiles(@"D:\Program Files (x86)\GOG Galaxy\Games\Battlezone Combat Commander\bz2r_res", "*.bzn", SearchOption.AllDirectories)
                .Concat(Directory.EnumerateFiles(@"D:\Program Files (x86)\GOG Galaxy\Games\Battlezone Combat Commander\maps", "*.bzn", SearchOption.AllDirectories))
                .Concat(Directory.EnumerateFiles(@"F:\Programming\BZRModManager\BZRModManager\BZRModManager\bin\steamcmd\steamapps\workshop\content\624970", "*.bzn", SearchOption.AllDirectories))
                .Concat(Directory.EnumerateFiles(@"F:\Programming\BZRModManager\GenerateMultiplayerDataExtract\GenerateMultiplayerDataExtract\bin\Debug\BZ98R", "*.bzn", SearchOption.AllDirectories))
                .Concat(Directory.EnumerateFiles(@"F:\Programming\BZRModManager\BZRModManager\BZRModManager\bin\steamcmd\steamapps\workshop\content\301650", "*.bzn", SearchOption.AllDirectories))
                .Concat(Directory.EnumerateFiles(@"..\..\..\sample", "*", SearchOption.AllDirectories))
                .Concat(Directory.EnumerateFiles(@"..\..\..\..\old\sample", "*", SearchOption.AllDirectories))
                .Concat(Directory.EnumerateFiles(@"..\..\..\..\TempApp\bin\Debug\net8.0\out", "*", SearchOption.AllDirectories))
                )
            //foreach (string filename in Directory.EnumerateFiles(@"F:\Programming\BZRModManager\BZRModManager\BZRModManager\bin\steamcmd\steamapps\workshop\content\301650", "*.bzn", SearchOption.AllDirectories))
            //foreach (string filename in Directory.EnumerateFiles(@"..\..\..\..\old\sample", "*", SearchOption.AllDirectories))
            //foreach (string filename in Directory.EnumerateFiles(@"..\..\..\sample", "*", SearchOption.AllDirectories))
            //foreach (string filename in Directory.EnumerateFiles(@"..\..\..\..\TempApp\bin\Debug\net8.0\out", "*", SearchOption.AllDirectories))
            {
                Console.WriteLine(filename);

                if (Success.Contains(filename))
                    continue;

                if (new FileInfo(filename).Length > 0)
                {
                    using (FileStream file = File.OpenRead(filename))
                    {
                        using (BZNStreamReader reader = new BZNStreamReader(file, filename))
                        {
                            //if (reader.HasBinary)
                            //    continue;

                            switch (reader.Format)
                            {
                                case BZNFormat.Battlezone:
                                case BZNFormat.BattlezoneN64:
                                case BZNFormat.Battlezone2:
                                    {
                                        //bool success = false;
                                        try
                                        {
                                            switch (reader.Format)
                                            {
                                                case BZNFormat.Battlezone:
                                                case BZNFormat.BattlezoneN64:
                                                    new BZNFileBattlezone(reader, Hints: BZ1Hints);
                                                    break;
                                                case BZNFormat.Battlezone2:
                                                    new BZNFileBattlezone(reader, Hints: BZ2Hints);
                                                    break;
                                            }

                                            //success = true;
                                            File.AppendAllText("success.txt", $"{filename}\r\n");
                                            //File.AppendAllText($"{reader.Format.ToString()} {reader.Version.ToString("D4")}.txt", $"{filename}\r\n");
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.ResetColor();
                                            //Console.ReadKey(true);
                                            File.AppendAllText("failed.txt", $"{filename}\r\n");
                                        }
                                        finally
                                        {
                                            /*BznType bznType = new BznType(reader.Version, reader.HasBinary, reader.Format);
                                            if (!Files.ContainsKey(bznType))
                                                Files[bznType] = new List<(string, bool)>();
                                            Files[bznType].Add((filename, success));*/
                                        }
                                    }
                                    break;
                                case BZNFormat.StarTrekArmada:
                                case BZNFormat.StarTrekArmada2:
                                    break;
                            }
                        }
                    }
                    //Console.ReadKey(true);
                }
            }
            //Console.ReadKey(true);
            /*using (var writer = File.CreateText("files.txt"))
            {
                foreach (KeyValuePair<BznType, List<(string, bool)>> entry in Files.OrderBy(dr => dr.Key.version).ThenBy(dr => dr.Key.version).ThenBy(dr => dr.Key.format))
                {
                    foreach ((string, bool) item in entry.Value.OrderBy(dr => dr))
                    {
                        writer.WriteLine($"{entry.Key.version}\t{entry.Key.binary}\t{entry.Key.format}\t{item.Item2}\t{item.Item1}");
                    }
                }
            }*/
        }
    }
}