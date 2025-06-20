using BZNParser.Battlezone.GameObject;
using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace BZNParser.Battlezone
{
    public class BattlezoneBZNHints
    {
        /// <summary>
        /// Is the reader in strict mode where the class MUST be in the hint list?
        /// </summary>
        public bool Strict { get; set; }
        public Dictionary<string, HashSet<string>>? ClassLabels { get; set; }
    }
    public class BZNFileBattlezone
    {
        private BattlezoneBZNHints? Hints;
        public BZNFileBattlezone(BZNStreamReader reader, BattlezoneBZNHints? Hints = null)
        {
            this.Hints = Hints;

            IBZNToken tok;
            //if(!tok.Validate("version", BinaryFieldType.DATA_UNKNOWN)) throw new Exception("Failed to parse version/UNKNOWN");
            //Console.WriteLine($"Version: {tok.GetUInt32()}");
            /*if (!tok.Validate("version", BinaryFieldType.DATA_UNKNOWN))
            {
                if (!tok.Validate("aversion", BinaryFieldType.DATA_UNKNOWN))
                {
                    throw new Exception("Failed to parse version/UNKNOWN");
                }
                else
                {
                    Console.WriteLine($"version: {tok.GetUInt32()}");
                }
            }
            else
            {
                Console.WriteLine($"Version: {tok.GetUInt32()}");
            }*/
            Console.WriteLine($"Format: {reader.Format}");

            if (reader.Format != BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                Console.WriteLine($"Version: {tok.GetUInt32()}"); // don't bother validating first field maybe?
            }

            if (reader.Format == BZNFormat.Battlezone2 && reader.Version != 1041 && reader.Version != 1047) // version is special case for bz2001.bzn
            {
                tok = reader.ReadToken();
                if (!tok.Validate("saveType", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse saveType/UNKNOWN");
                Console.WriteLine($"saveType: {tok.GetUInt32()}");
            }

            //if (reader.Format == BZNFormat.BattlezoneN64)
            //{
            //    tok = reader.ReadToken();
            //    if (!tok.Validate("binarySave", BinaryFieldType.DATA_BOOL))
            //        throw new Exception("Failed to parse binarySave/BOOL");
            //    Console.WriteLine($"binarySave: {tok.GetBoolean()}");
            //
            //    tok = reader.ReadToken();
            //    if (!tok.Validate("msn_filename", BinaryFieldType.DATA_CHAR))
            //        throw new Exception("Failed to parse msn_filename/CHAR");
            //    Console.WriteLine($"msn_filename: \"{tok.GetString()}\"");
            //}
            if (reader.Format == BZNFormat.Battlezone)
            {
                if (reader.Version > 1022)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("binarySave", BinaryFieldType.DATA_BOOL))
                        throw new Exception("Failed to parse binarySave/BOOL");
                    Console.WriteLine($"binarySave: {tok.GetBoolean()}");

                    tok = reader.ReadToken();
                    if (!tok.Validate("msn_filename", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse msn_filename/CHAR");
                    Console.WriteLine($"msn_filename: \"{tok.GetString()}\"");
                }
            }
            if (reader.Format == BZNFormat.Battlezone2)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("binarySave", BinaryFieldType.DATA_BOOL))
                    throw new Exception("Failed to parse binarySave/BOOL");
                Console.WriteLine($"binarySave: {tok.GetBoolean()}");

                string msnFilename = reader.ReadSizedString_BZ2_1145("msn_filename", 16);
                Console.WriteLine($"msn_filename: \"{msnFilename}\"");
            }

            if (reader.Format == BZNFormat.BattlezoneN64 || (reader.Format == BZNFormat.Battlezone && reader.Version <= 1001))
            {
                tok = reader.ReadToken();
                if (!tok.Validate("seq_count", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse seq_count/LONG");
                Int32 seq_count = tok.GetInt32();
                Console.WriteLine($"seq_count: {seq_count}");
            }
            else if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.Battlezone2)
            {
                // Why does SeqCount exist if there's a GameObject counter too?
                // It appears to be the next seqno so we can calculate it for BZN64 via MAX+1.
                tok = reader.ReadToken();
                if (!tok.Validate("seq_count", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse seq_count/LONG");
                Int32 seq_count = tok.GetInt32();
                Console.WriteLine($"seq_count: {seq_count}");

                if (reader.Format == BZNFormat.Battlezone2)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("saveType", BinaryFieldType.DATA_LONG))
                        throw new Exception("Failed to parse saveType/LONG");
                    Int32 saveType2 = tok.GetInt32();
                    Console.WriteLine($"saveType (redundant?): {saveType2}");
                }
            }

            //bool strangeDefaultBZ1BZN = false;
            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                if (reader.Format == BZNFormat.Battlezone && reader.Version < 1016)
                {
                    bool missionSave = false;
                }
                //if ((1017 <= reader.Version && reader.Version <= 1037) || reader.Version == 1043 || reader.Version == 1045 || reader.Version == 2003 || reader.Version == 2016)
                else
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("missionSave", BinaryFieldType.DATA_BOOL))
                        throw new Exception("Failed to parse missionSave/BOOL");
                    bool missionSave = tok.GetBoolean();
                    Console.WriteLine($"missionSave: {missionSave}");
                }
            }

            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version != 1001)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("TerrainName", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse TerrainName/CHAR");
                    string TerrainName = tok.GetString();
                    Console.WriteLine($"TerrainName: {TerrainName}");
                }
            }
            else if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version < 1171)
                {
                    // BZ2: 1123 1124
                    tok = reader.ReadToken();
                    if (!tok.Validate("TerrainName", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse TerrainName/CHAR");
                    string TerrainName = tok.GetString();
                    Console.WriteLine($"TerrainName: {TerrainName}");
                }
                else if (reader.Version == 1171)
                {
                    // seems to be able to go either way
                    tok = reader.ReadToken();
                    if (!tok.Validate("g_TerrainName", BinaryFieldType.DATA_CHAR))
                    {
                        if (!tok.Validate("TerrainName", BinaryFieldType.DATA_CHAR)) // saw this on a 1171 once, why?
                            throw new Exception("Failed to parse g_TerrainName/CHAR");
                    }
                    string TerrainName = tok.GetString();
                    Console.WriteLine($"TerrainName: {TerrainName}");
                }
                else
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("g_TerrainName", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse g_TerrainName/CHAR");
                    string TerrainName = tok.GetString();
                    Console.WriteLine($"TerrainName: {TerrainName}");
                }
            }

            if (reader.Format == BZNFormat.Battlezone)
            {
                if (reader.Version == 1011 || reader.Version == 1012)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("start_time", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse start_time/FLOAT");
                    float start_time = tok.GetSingle();
                    Console.WriteLine($"start_time: {start_time}");
                }
            }

            // get count of GameObjects
            tok = reader.ReadToken();
            if (!tok.Validate("size", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse size/LONG");
            Int32 CountItems = tok.GetInt32();
            Console.WriteLine($"size: {CountItems}");

            BZNGameObjectWrapper[] GameObjects = new BZNGameObjectWrapper[CountItems];

            Dictionary<long, (BZNGameObjectWrapper Object, long Next)> RecursiveObjectGenreationMemo = new Dictionary<long, (BZNGameObjectWrapper Object, long Next)>();

            Dictionary<string, IClassFactory> ClassLabelMap = new Dictionary<string, IClassFactory>();
            foreach (Type type in this.GetType().Assembly.GetTypes())
            {
                // Only consider types that implement IClassFactory and are not interfaces or abstract
                if (!typeof(IClassFactory).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract)
                    continue;

                var attrs = type.GetCustomAttributes(typeof(ObjectClassAttribute), true);
                foreach (ObjectClassAttribute attr in attrs)
                    if (attr.Format == reader.Format)
                        if (ClassLabelMap.ContainsKey(attr.ClassName))
                            throw new Exception($"Duplicate class label: {attr.ClassName} ({type})");
                        else
                            ClassLabelMap[attr.ClassName] = (IClassFactory)Activator.CreateInstance(type)!;
            }

            int CntPad = CountItems.ToString().Length;
            Dictionary<string, HashSet<string>> LongTermClassLabelLookupCache = new Dictionary<string, HashSet<string>>();
            for (int gameObjectCounter = 0; gameObjectCounter < CountItems; gameObjectCounter++)
            {
                //GameObjects[gameObjectCounter] = new BZNGameObjectWrapper(reader, (gameObjectCounter + 1) == CountItems);
                GameObjects[gameObjectCounter] = new BZNGameObjectWrapper(this, reader, CountItems - gameObjectCounter, LongTermClassLabelLookupCache, RecursiveObjectGenreationMemo, null, ClassLabelMap: ClassLabelMap, Hints: Hints);
                Console.WriteLine($"GameObject[{gameObjectCounter.ToString().PadLeft(CntPad)}]: {GameObjects[gameObjectCounter].seqNo.ToString("X8")} {GameObjects[gameObjectCounter].PrjID.ToString().PadRight(16)} {(GameObjects[gameObjectCounter].gameObject.ClassLabel ?? string.Empty).PadRight(16)} {GameObjects[gameObjectCounter].gameObject.ToString().Replace(@"BZNParser.Battlezone.GameObject.", string.Empty)}");
            }

            TailParse(reader);
        }

        public void TailParse(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version > 1165)
                {
                    // 1187, 1188, 1192
                    tok = reader.ReadToken();
                    if (!tok.Validate("groupTargets", BinaryFieldType.DATA_VOID))
                        throw new Exception("Failed to parse groupTargets/VOID");
                }
                if (reader.Version == 1100 || reader.Version == 1041 || reader.Version == 1047 || reader.Version == 1070) // not sure what versions this happens
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("name", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse dllName/CHAR");
                    Console.WriteLine($"Mission: {tok.GetString()}");
                }
                else if (reader.Version < 1145)
                {
                    // max length 40
                    tok = reader.ReadToken();
                    if (!tok.Validate("dllName", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse dllName/CHAR");
                    Console.WriteLine($"Mission: {tok.GetString()}");
                }
                else
                {
                    if (reader.InBinary)
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate(null, BinaryFieldType.DATA_CHAR))
                            throw new Exception("Failed to parse dllName/CHAR");
                    }
                    tok = reader.ReadToken();
                    if (!tok.Validate("dllName", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse dllName/CHAR");
                    Console.WriteLine($"Mission: {tok.GetString()}");
                }
            }
            if (reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                string mission = string.Format("BZn64Mission_{0,4:X4}", tok.GetUInt16());
                Console.WriteLine($"Mission: {mission}");

                UInt32 sObject = reader.ReadBZ1_PtrDepricated("sObject");
            }
            if (reader.Format == BZNFormat.Battlezone)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("name", BinaryFieldType.DATA_CHAR))
                    throw new Exception("Failed to parse name/CHAR");
                Console.WriteLine($"Mission: {tok.GetString()}");

                // read the old sObject ptr, not sure what can be done with it
                if (reader.Version < 1002)
                {
                    UInt32 sObject = reader.ReadBZ1_PtrDepricated("sObject");
                }
                else
                {
                    UInt32 sObject = reader.ReadBZ1_Ptr("sObject");
                }

                // BZ1 sometimes has a bool here?
                // looks like it's based on what mission is used
                // but it looks like AIMission starts with the header, so no idea what this extra bool is for
                if (reader.Version == 1044)
                {
                    long pos = reader.BaseStream.Position;
                    tok = reader.ReadToken();
                    if (!tok.Validate("undefbool", BinaryFieldType.DATA_BOOL))
                    {
                        // unknown what this is or why it happens
                        //throw new Exception("Failed to parse undefboolBOOL");
                        reader.BaseStream.Position = pos;
                    }
                    else
                    {
                        // if this never happens now we can remove it
                    }
                }
            }

            if (!reader.InBinary)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AiMission", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse [AiMission]");
            }

            if (reader.Format == BZNFormat.Battlezone && (reader.Version == 1001 || reader.Version == 1011 || reader.Version == 1012))
            {
                tok = reader.ReadToken();
                if (!tok.Validate("size", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse size/LONG");
                Int32 UnknownSize = tok.GetInt32();
            }

            if (reader.Format == BZNFormat.Battlezone && (reader.Version == 1011 || reader.Version == 1012))
            {
                // this might also be due to the above count being 1 instead of 0, unknown, for now we're using the version

                tok = reader.ReadToken();
                if (!tok.Validate("name", BinaryFieldType.DATA_CHAR))
                    throw new Exception("Failed to parse name/CHAR");
                //tok.GetBytes(); // "AiMission"

                // read the old sObject ptr, not sure what can be done with it
                if (reader.Version < 1002)
                {
                    UInt32 sObject = reader.ReadBZ1_PtrDepricated("sObject");
                }
                else
                {
                    UInt32 sObject = reader.ReadBZ1_Ptr("sObject");
                }

                if (!reader.InBinary)
                {
                    tok = reader.ReadToken();
                    if (!tok.IsValidationOnly() || !tok.Validate("UserProcess", BinaryFieldType.DATA_UNKNOWN))
                        throw new Exception("Failed to parse [UserProcess]");
                }

                tok = reader.ReadToken();
                if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR))
                    throw new Exception("Failed to parse undefptr/PTR");
                //tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("cycle", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse cycle/UNKNOWN");
                //tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("cycleMax", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse cycleMax/UNKNOWN");
                //tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("selectList", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse selectList/UNKNOWN");
                //tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR))
                    throw new Exception("Failed to parse undefptr/PTR");
                //tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR))
                    throw new Exception("Failed to parse undefptr/PTR");
                //tok.GetUInt32H();

                tok = reader.ReadToken();
                if (!tok.Validate("exited", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse exited/UNKNOWN");
                //tok.GetUInt32H();
            }

            // if reader.SaveType != 0

            if (!reader.InBinary)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AOIs", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse [AOIs]");
            }

            tok = reader.ReadToken();
            if (!tok.Validate("size", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse size/LONG");
            Int32 CountAOIs = tok.GetInt32();

            for (int i = 0; i < CountAOIs; i++)
            {
                if (!reader.InBinary)
                {
                    tok = reader.ReadToken();
                    if (!tok.IsValidationOnly() || !tok.Validate("AOI", BinaryFieldType.DATA_UNKNOWN))
                        throw new Exception("Failed to parse [AOI]");
                }
                //if (reader.Format == BZNFormat.Battlezone2)
                //{
                //    tok = reader.ReadToken();
                //    if (!tok.Validate("name", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
                //    string name = tok.GetString();
                //    if (name != "AOI")
                //    {
                //        throw new Exception("Failed to parse AOI"); // untested/unconfirmed assumption
                //    }
                //}

                if (reader.Format == BZNFormat.Battlezone)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR))
                        throw new Exception("Failed to parse undefptr/PTR");
                    //tok.GetUInt32H();
                }
                if (reader.Format == BZNFormat.Battlezone2)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("path", BinaryFieldType.DATA_PTR))
                        throw new Exception("Failed to parse path/PTR");
                    //tok.GetUInt32H();
                }

                tok = reader.ReadToken();
                if (!tok.Validate("team", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse team/LONG");
                //tok.GetUInt32();

                tok = reader.ReadToken();
                if (!tok.Validate("interesting", BinaryFieldType.DATA_BOOL))
                    throw new Exception("Failed to parse interesting/BOOL");
                //tok.GetBool();

                tok = reader.ReadToken();
                if (!tok.Validate("inside", BinaryFieldType.DATA_BOOL))
                    throw new Exception("Failed to parse inside/BOOL");
                //tok.GetBool();

                tok = reader.ReadToken();
                if (!tok.Validate("value", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse value/LONG");
                //tok.GetUInt32();

                tok = reader.ReadToken();
                if (!tok.Validate("force", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse force/LONG");
                //tok.GetUInt32();
            }

            if (!reader.InBinary)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AiPaths", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse [AiPaths]");
            }

            tok = reader.ReadToken();
            if (!tok.Validate("count", BinaryFieldType.DATA_LONG))
                throw new Exception("Failed to parse count/LONG");
            Int32 CountPaths = tok.GetInt32();

            for (int i = 0; i < CountPaths; i++)
            {
                if (!reader.InBinary && reader.Format == BZNFormat.Battlezone)
                {
                    tok = reader.ReadToken();
                    if (!tok.IsValidationOnly() || !tok.Validate("AiPath", BinaryFieldType.DATA_UNKNOWN))
                        throw new Exception("Failed to parse [AiPath]");
                }
                if (reader.Format == BZNFormat.Battlezone2)
                {
                    string name = reader.ReadSizedString_BZ2_1145("name", 40);
                    if (name != "AiPath")
                    {
                        throw new Exception("Failed to parse AiPath");
                    }
                }

                if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
                {
                    if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version >= 2016)
                    {
                        // 2016
                        tok = reader.ReadToken();
                        if (!tok.Validate("old_ptr", BinaryFieldType.DATA_PTR))
                            throw new Exception("Failed to parse old_ptr/PTR");
                    }
                    else
                    {
                        // 1030 1032 1034 1035 1037 1038 1039 1040 1043 1044 1045 1049 2003 2004 2010 2011
                        tok = reader.ReadToken();
                        if (!tok.Validate("old_ptr", BinaryFieldType.DATA_VOID))
                            throw new Exception("Failed to parse old_ptr/VOID");
                    }
                    //Int32 x = tok.GetUInt32H();
                }
                else if (reader.Format == BZNFormat.Battlezone2)
                {
                    long pos = reader.BaseStream.Position;
                    tok = reader.ReadToken();
                    if (!tok.Validate("sObject", BinaryFieldType.DATA_PTR))
                    {
                        reader.BaseStream.Position = pos;
                        //throw new Exception("Failed to parse sObject/PTR");
                    }
                    else
                    {
                        //Int32 x = tok.GetUInt32H();
                    }
                }

                string? label = null;
                if (reader.Format == BZNFormat.BattlezoneN64)
                {
                    tok = reader.ReadToken();
                    label = string.Format("bzn64path_{0,4:X4}", tok.GetUInt16());
                }
                else
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("size", BinaryFieldType.DATA_LONG))
                        throw new Exception("Failed to parse size/LONG");
                    int labelSize = tok.GetInt32();

                    if (labelSize > 0)
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate("label", BinaryFieldType.DATA_CHAR))
                            throw new Exception("Failed to parse label/CHAR");
                        label = tok.GetString();
                        if (label.Length > labelSize)
                            label = label.Substring(0, labelSize);
                    }
                }
                Console.WriteLine($"AiPath[{i.ToString().PadLeft(CountPaths.ToString().Length)}]: {(label ?? string.Empty)}");

                tok = reader.ReadToken();
                if (!tok.Validate("pointCount", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse pointCount/LONG");
                int pointCount = tok.GetInt32();

                tok = reader.ReadToken();
                if (!tok.Validate("points", BinaryFieldType.DATA_VEC2D))
                    throw new Exception("Failed to parse point/VEC2D");
                for (int j = 0; j < pointCount; j++)
                {
                    Vector2D point = tok.GetVector2D(j);
                    Console.WriteLine($"{new string(' ', CountPaths.ToString().Length + 9)} [{j.ToString().PadLeft(pointCount.ToString().Length)}] {point.x}, {point.z}");
                }

                tok = reader.ReadToken();
                if (!tok.Validate("pathType", BinaryFieldType.DATA_VOID))
                    throw new Exception("Failed to parse pathType/VOID");
                //Int32 pathType = tok.GetUInt32H();
            }

            if (reader.Format == BZNFormat.Battlezone2)
            {
                // SatellitePanel
                if (reader.Version >= 1125) // version 1169 failed to read this, might need a walk back for malformed
                {
                    long pos = reader.BaseStream.Position;

                    // 1188 1192
                    tok = reader.ReadToken();
                    if (!tok.Validate("hasEntered", BinaryFieldType.DATA_BOOL))
                    {
                        if (tok.Validate("PadData", BinaryFieldType.DATA_VOID))
                        {
                            // SatellitePanel data is missing when it must exist, deal with damaged BZN?
                            reader.BaseStream.Position = pos;
                        }
                        else
                        {
                            throw new Exception("Failed to parse hasEntered/BOOL");
                        }
                    }
                    else
                    {

                        for (int i = 0; i < 3/*MAX_WORLDS*/; i++)
                        {
                            tok = reader.ReadToken();
                            if (!tok.Validate("ownerObj", BinaryFieldType.DATA_LONG))
                                throw new Exception("Failed to parse ownerObj/LONG");
                            //Int32 pathType = tok.GetUInt32H();
                        }
                    }
                }
            }

            if (reader.Format == BZNFormat.Battlezone && (reader.Version == 1001 || reader.Version == 1011 || reader.Version == 1012))
            {
                if (!reader.InBinary)
                {
                    tok = reader.ReadToken();
                    if (!tok.IsValidationOnly() || !tok.Validate("AiTasks", BinaryFieldType.DATA_UNKNOWN))
                        throw new Exception("Failed to parse [AiTasks]");
                }

                tok = reader.ReadToken();
                if (!tok.Validate("count", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse count/LONG");
                Int32 CountAiTasks = tok.GetInt32();

                for (int i = 0; i < CountAiTasks; i++)
                {
                }
            }

            if (reader.Format == BZNFormat.Battlezone2)
            {
                // code says this has to be SaveType != 0 to load, but they do always exist, very strange
                if (reader.Version >= 1115)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("PadData", BinaryFieldType.DATA_VOID))
                        throw new Exception("Failed to parse PadData/VOID");

                    if (reader.Version >= 1119)
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate("PadData2", BinaryFieldType.DATA_VOID))
                            throw new Exception("Failed to parse PadData2/VOID");
                    }
                }
            }

            if (reader.Format == BZNFormat.Battlezone && (reader.Version == 1001 || reader.Version == 1011 || reader.Version == 1012))
            {
                if (reader.Version == 1001)
                {
                    if (!reader.InBinary)
                    {
                        tok = reader.ReadToken();
                        if (!tok.IsValidationOnly() || !tok.Validate("Terrain", BinaryFieldType.DATA_UNKNOWN))
                            throw new Exception("Failed to parse [Terrain]");
                    }

                    tok = reader.ReadToken();
                    string TerrainName = tok.GetString();
                    if (!tok.Validate("Name", BinaryFieldType.DATA_UNKNOWN))
                        throw new Exception("Failed to parse Name/UNKNOWN");
                    Console.WriteLine($"TerrainName: {TerrainName}");
                }
            }

            if (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (reader.Format == BZNFormat.Battlezone)
                {
                    // odd extra VEC2D at the end of the file with 0,0
                    tok = reader.ReadToken(); // returns null if the stream ends after chewing extra lines
                    if (tok != null)
                    {
                        if (!tok.Validate(null, BinaryFieldType.DATA_VEC2D))
                        {
                            throw new Exception("Tokens left after last known token");
                        }
                        Vector2D point = tok.GetVector2D();
                        if (point.x != 0 || point.z != 0)
                            throw new Exception("Tokens left after last known token");
                    }
                }

                if (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    throw new Exception("Tokens left after last known token");
                }
            }

            // BZ1 version 2016 binary extra DATA_VEC2D at the end, not sure if this is universal
        }
    }
}
