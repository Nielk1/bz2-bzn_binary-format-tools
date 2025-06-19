using BZNParser.Battlezone.GameObject;
using BZNParser.Reader;
using BZNParser;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.Reflection;

namespace BZNParser.Battlezone
{
    public class BZNGameObjectWrapper
    {
        public string PrjID { get; set; }
        public UInt32 seqNo { get; set; }
        public Vector3D pos { get; set; }
        public UInt32 team { get; set; }
        public string label { get; set; }
        public UInt32 isUser { get; set; }
        public UInt32 obj_addr { get; set; }
        public Matrix transform { get; set; }

        public ClassGameObject gameObject { get; set; }


        private Dictionary<string, HashSet<string>> LongTermClassLabelLookupCache;
        private Dictionary<string, Type> ClassLabelMap;

        // TODO move this to a factory pattern so we aren't relying on exceptions from constructors
        public BZNGameObjectWrapper(BZNFileBattlezone parent, BZNStreamReader reader, int countLeft, Dictionary<string, HashSet<string>> LongTermClassLabelLookupCache, int depth = 0, Dictionary<long, (BZNGameObjectWrapper Object, long Next)> RecursiveObjectGenreationMemo = null, Dictionary<string, string> ClassLabelTempLookup = null, Dictionary<string, Type> ClassLabelMap = null, HashSet<string> SuccessfulParses = null, HashSet<string> FailedParses = null, BattlezoneBZNHints? Hints = null, bool fake = false)
        {
            SuccessfulParses = SuccessfulParses ?? new HashSet<string>();
            FailedParses = FailedParses ?? new HashSet<string>();

            this.LongTermClassLabelLookupCache = LongTermClassLabelLookupCache;
            this.ClassLabelMap = ClassLabelMap;
            if (ClassLabelTempLookup == null)
                ClassLabelTempLookup = new Dictionary<string, string>();
            IBZNToken tok;
            if (!reader.InBinary)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("GameObject", BinaryFieldType.DATA_UNKNOWN))
                    throw new Exception("Failed to parse [GameObject]");
            }

            if (reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                UInt16 ItemID = tok.GetUInt16();
                if (!BZNFile.BZn64IdMap.ContainsKey(ItemID))
                    throw new InvalidCastException(string.Format("Cannot convert n64 PrjID enumeration 0x(0:X2} to string PrjID", ItemID));
                PrjID = BZNFile.BZn64IdMap[ItemID];
            }
            else if (reader.Format == BZNFormat.Battlezone)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("PrjID", BinaryFieldType.DATA_ID))
                    throw new Exception("Failed to parse PrjID/ID");
                //if (!tok.Validate("PrjID", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse PrjID/ID");
                PrjID = tok.GetString();
                if (reader.Version == 1001)
                {
                    PrjID = PrjID.Split('\0')[0];
                }
            }
            else if (reader.Format == BZNFormat.Battlezone2)
            {
                //if (reader.HasBinary && reader.Version > 1105) // not sure the version for this one
                //{
                //    tok = reader.ReadToken();
                //    if (!tok.Validate(null, BinaryFieldType.DATA_CHAR))
                //        throw new Exception("Failed to parse ?/CHAR");
                //    //string odfName = tok.GetString();
                //    byte odfLength = tok.GetUInt8();
                //}

                //if (reader.Version < 1145)
                if (reader.Version < 1155)
                {
                    PrjID = reader.ReadGameObjectClass_BZ2("config");
                }
                else
                {
                    if (reader.SaveType == 3)
                    {

                    }
                    else
                    {
                        if (reader.Version == 1180)
                        {
                            PrjID = reader.ReadGameObjectClass_BZ2("GetClass()");
                        }
                        //else if (reader.Version <= 1192 && reader.Version >= 1187)
                        //else if (reader.Version <= 1192 && reader.Version >= 1183)
                        //else if (reader.Version <= 1192 && reader.Version >= 1178)
                        //else if (reader.Version <= 1192 && reader.Version >= 1171)
                        else
                        {
                            // 1183 1187 1188 1192
                            PrjID = reader.ReadGameObjectClass_BZ2("objClass");
                        }
                    }
                }
            }

            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.InBinary)
                {
                    seqNo = reader.ReadCompressedNumberFromBinary();
                }
                else if (reader.Version >= 1145 && reader.InBinary && false)// && omitBinarySaveHeadsers)
                {
                    tok = reader.ReadToken();
                    if (tok.Validate("seqno", BinaryFieldType.DATA_SHORT))
                        throw new Exception("Failed to parse seqno/SHORT");
                    UInt16 seqno2a = tok.GetUInt16();

                    tok = reader.ReadToken();
                    if (!tok.Validate("seqno", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse seqno/CHAR");
                    byte seqno2b = tok.GetUInt8();

                    seqNo = (uint)((seqno2b << 16) | (seqno2a));
                }
                else
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("seqno", BinaryFieldType.DATA_LONG))
                        throw new Exception("Failed to parse seqno/SHORT");
                    seqNo = tok.GetUInt32H();
                }
            }
            else if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("seqno", BinaryFieldType.DATA_SHORT))
                    throw new Exception("Failed to parse seqno/SHORT");
                seqNo = tok.GetUInt16();
            }

            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("pos", BinaryFieldType.DATA_VEC3D))
                    throw new Exception("Failed to parse pos/VEC3D");
                pos = tok.GetVector3D();
            }

            tok = reader.ReadToken();
            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                if (!tok.Validate("team", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse team/LONG");
                team = tok.GetUInt32();
            }
            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version < 1145)
                {
                    if (!tok.Validate("team", BinaryFieldType.DATA_LONG))
                        throw new Exception("Failed to parse team/LONG");
                    team = tok.GetUInt32();
                }
                else
                {
                    if (!tok.Validate("team", BinaryFieldType.DATA_CHAR))
                        throw new Exception("Failed to parse team/CHAR");
                    team = tok.GetUInt8(); // does this include perceived team in the high nybble? probably
                }
            }

            if (reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("label", BinaryFieldType.DATA_SHORT))
                    throw new Exception("Failed to parse label/CHAR");
                label = string.Format("bzn64label_{0,4:X4}", tok.GetUInt16());
            }
            else if (reader.Format == BZNFormat.Battlezone)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("label", BinaryFieldType.DATA_CHAR))
                    throw new Exception("Failed to parse label/CHAR");
                label = tok.GetString();
            }
            else if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.Version < 1145)
                {
                    //tok = reader.ReadToken();
                    //if (!tok.Validate("label", BinaryFieldType.DATA_CHAR))
                    //    throw new Exception("Failed to parse label/CHAR");
                    //label = tok.GetString();

                    label = reader.ReadSizedString_BZ2_1145("label", 40);
                }
                else
                {
                    bool noLabel = (seqNo & 0x800000) != 0;
                    seqNo &= ~(UInt32)0x800000;

                    if (noLabel)
                    {

                    }
                    else
                    {
                        //tok = reader.ReadToken();
                        //if (!tok.Validate(null, BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse ?/CHAR");
                        //
                        //tok = reader.ReadToken();
                        //if (!tok.Validate("label", BinaryFieldType.DATA_CHAR))
                        //    throw new Exception("Failed to parse label/CHAR");
                        //label = tok.GetString();
                        label = reader.ReadSizedString_BZ2_1145("label", 40);
                    }
                }
            }

            if (reader.Format == BZNFormat.Battlezone2)
            {
                if (reader.SaveType != 2)
                {
                    tok = reader.ReadToken();
                    if (reader.Version < 1145)
                    {
                        if (!tok.Validate("isUser", BinaryFieldType.DATA_LONG))
                            throw new Exception("Failed to parse isUser/LONG");
                        isUser = tok.GetUInt32();
                    }
                    else
                    {
                        if (!tok.Validate("isUser", BinaryFieldType.DATA_BOOL))
                            throw new Exception("Failed to parse isUser/BOOL");
                        isUser = tok.GetBoolean() ? 1u : 0u;
                    }
                }
                //else{}
            }
            else if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("isUser", BinaryFieldType.DATA_LONG))
                    throw new Exception("Failed to parse isUser/LONG");
                isUser = tok.GetUInt32();
            }
            
            if (reader.Format == BZNFormat.Battlezone || reader.Format == BZNFormat.BattlezoneN64)
            {
                if (reader.Format == BZNFormat.BattlezoneN64 || reader.Version < 1002)
                {
                    obj_addr = reader.ReadBZ1_PtrDepricated("obj_addr"); // string name unconfirmed
                }
                else
                {
                    obj_addr = reader.ReadBZ1_Ptr("obj_addr");
                }
            }
            else if (reader.Format == BZNFormat.Battlezone2)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("objAddr", BinaryFieldType.DATA_PTR))
                    throw new Exception("Failed to parse objAddr/PTR");
                obj_addr = tok.GetUInt32H();
            }

            if (reader.Format == BZNFormat.Battlezone2)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("transform", BinaryFieldType.DATA_MAT3D))
                    throw new Exception("Failed to parse transform/MAT3D");
                transform = tok.GetMatrix();
            }
            if ((reader.Format == BZNFormat.Battlezone && reader.Version > 1001) || reader.Format == BZNFormat.BattlezoneN64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("transform", BinaryFieldType.DATA_MAT3DOLD))
                    throw new Exception("Failed to parse transform/MAT3DOLD");
                transform = tok.GetMatrixOld();
            }

            if (fake)
                return;
            // other save types here

            //Console.WriteLine($"DEBUG {new string('>', depth)} Trying to parse [{countLeft}] {PrjID} {reader.BaseStream.Position}");
            gameObject = ParseGameObject(parent, reader, countLeft, depth, RecursiveObjectGenreationMemo, ClassLabelTempLookup, SuccessfulParses, FailedParses, Hints);
            //Console.WriteLine($"DEBUG {new string('<', depth)} return [{countLeft}] {PrjID} {reader.BaseStream.Position}");
        }

        private ClassGameObject ParseGameObject(BZNFileBattlezone parent, BZNStreamReader reader, int countLeft, int depth, Dictionary<long, (BZNGameObjectWrapper Object, long Next)> RecursiveObjectGenreationMemo, Dictionary<string, string> ClassLabelTempLookup, HashSet<string> SuccessfulParses, HashSet<string> FailedParses, BattlezoneBZNHints Hints)
        {
            List<string>? ValidClassLabels = null;

            if (Hints?.ClassLabels != null)
            {
                ValidClassLabels = new List<string>();
                string lookupKey = PrjID.ToLowerInvariant();
                if (Hints.ClassLabels.ContainsKey(lookupKey))
                {
                    var possibleClasses = Hints.ClassLabels[lookupKey];
                    foreach (string possibleClass in possibleClasses)
                    {
                        if (possibleClass != null && (ClassLabelMap?.ContainsKey(possibleClass) ?? false))
                        {
                            ValidClassLabels.Add(possibleClass);
                        }
                    }
                }
            }

            ClassGameObject? gameObject = null;

            if (ValidClassLabels != null && ValidClassLabels.Count == 1)
            {
                string label = ValidClassLabels.First();
                Type t = ClassLabelMap[label];
                ConstructorInfo? info = t.GetConstructor(new Type[] { typeof(string), typeof(bool), typeof(string) });
                gameObject = (info?.Invoke(new object[] { PrjID, isUser != 0, label })) as ClassGameObject;
            }

            // we think we know what type of GameObject this is based on data about the class
            if (gameObject != null)
            {
                long pos = reader.BaseStream.Position;
                try
                {
                    gameObject.LoadData(reader);
                    SuccessfulParses.Add(PrjID);
                    return gameObject; // success! Keep the stream position where it is
                }
                catch
                {
                    FailedParses?.Add(PrjID);
                    // roll back to the start of the object
                    reader.BaseStream.Position = pos;
                    gameObject = null;
                }
            }

            // if we're here, we failed to load the game object so now recursive hell starts
            //if (gameObject == null)
            {
                // try every possible object
                long pos = reader.BaseStream.Position;
                ClassGameObject tempGameObject;

                
                List<(ClassGameObject Object, bool Expected, long Next, string Name)> Candidates = new List<(ClassGameObject Object, bool Expected, long Next, string Name)>();
                string classLableTempHolder = null;
                bool firstParseSuccess = false;

                foreach (var kv in ClassLabelMap.OrderBy(dr => dr.Key))
                {
                    classLableTempHolder = kv.Key;
                    if (!LongTermClassLabelLookupCache.ContainsKey(PrjID.ToLowerInvariant()) || LongTermClassLabelLookupCache[PrjID.ToLowerInvariant()].Contains(classLableTempHolder))
                    {
                        if (!(Hints?.Strict ?? false) || ValidClassLabels == null || ValidClassLabels.Count == 0 || ValidClassLabels.Contains(classLableTempHolder))
                            try
                            {
                                ConstructorInfo? info = kv.Value.GetConstructor(new Type[] { typeof(string), typeof(bool), typeof(string) });
                                tempGameObject = (info?.Invoke(new object[] { PrjID, isUser != 0, kv.Key })) as ClassGameObject;
                                if (tempGameObject != null)
                                {
                                    //Console.ForegroundColor = ConsoleColor.Green;
                                    //Console.WriteLine($"DEBUG {new string('>', depth)} Candidate [{countLeft}] {PrjID} {tempGameObject}");
                                    //Console.ResetColor();
                                    tempGameObject.LoadData(reader); ClassLabelTempLookup[PrjID.ToLowerInvariant()] = classLableTempHolder;
                                    firstParseSuccess = true;
                                    if (CheckNext(parent, reader, countLeft - 1, depth + 1, RecursiveObjectGenreationMemo, ClassLabelTempLookup, SuccessfulParses, FailedParses, Hints))
                                        Candidates.Add((tempGameObject, ValidClassLabels?.Contains(classLableTempHolder) ?? false, reader.BaseStream.Position, classLableTempHolder));
                                }
                                else
                                {
                                    //Console.WriteLine($"DEBUG {new string('>', depth)} Candidate [{countLeft}] {PrjID} {tempGameObject}");
                                }
                            }
                            catch
                            {
                                //Console.ForegroundColor = ConsoleColor.Red;
                                //Console.WriteLine($"DEBUG {new string('>', depth)} Candidate [{countLeft}] {PrjID} {kv.Value.Name}");
                                //Console.ResetColor();
                            }
                        reader.BaseStream.Position = pos;
                    }
                }

                ClassLabelTempLookup.Remove(PrjID.ToLowerInvariant());

                if (!LongTermClassLabelLookupCache.ContainsKey(PrjID.ToLowerInvariant()))
                    LongTermClassLabelLookupCache[PrjID.ToLowerInvariant()] = new HashSet<string>(Candidates.Select(dr => dr.Name));
                LongTermClassLabelLookupCache[PrjID.ToLowerInvariant()] = new HashSet<string>(LongTermClassLabelLookupCache[PrjID.ToLowerInvariant()].Intersect(Candidates.Select(dr => dr.Name)));

                if (Candidates.Count > 0)
                {
                    if (Candidates.Count == 1)
                    {
                        reader.BaseStream.Position = Candidates[0].Next;
                        return Candidates[0].Object;
                    }
                    else
                    {
                        reader.BaseStream.Position = Candidates.Min(dr => dr.Next);
                        return new MultiClass(PrjID, isUser != 0, Candidates);
                    }
                }
                else
                {
                    if (firstParseSuccess)
                    {
                        if (SuccessfulParses != null && FailedParses != null)
                        {
                            var items = FailedParses.Except(SuccessfulParses).ToList();
                            if (items.Count > 0)
                            {
                                if (items.Count == 1)
                                    throw new Exception($"Failed to parse GameObject {items.First()}");
                                throw new Exception($"Failed to parse GameObject {PrjID}, failure after on one of: {string.Join(",", items)}");
                            }
                        }
                        throw new Exception($"Failed to parse GameObject {PrjID}, failure likely after this object");
                    }
                    throw new Exception($"Failed to parse GameObject {PrjID}");
                }
            }

            return null;
        }

        private bool CheckNext(BZNFileBattlezone parent, BZNStreamReader reader, int countLeft, int depth, Dictionary<long, (BZNGameObjectWrapper? Object, long Next)> RecursiveObjectGenreationMemo, Dictionary<string, string> ClassLabelTempLookup, HashSet<string> SuccessfulParses, HashSet<string> FailedParses, BattlezoneBZNHints Hints)
        {
            long pos = reader.BaseStream.Position;
            try
            {
                if (countLeft == 0)
                {
                    try
                    {
                        parent.TailParse(reader);
                    }
                    catch
                    {
                        reader.BaseStream.Position = pos;
                        return false;
                    }
                    reader.BaseStream.Position = pos;
                    //if (depth > 50)
                    return true; // Maximum Depth
                    /*
                    if (reader.InBinary)
                    {
                        return true; // TODO figure out how to determine we're at the end of the ODF section in binary mode, maybe test parse the next section?
                    }
                    else
                    {
                        IBZNToken tok = reader.ReadToken();
                        if (reader.Format == BZNFormat.Battlezone)
                        {
                            if (tok.Validate("name", BinaryFieldType.DATA_UNKNOWN))
                            {
                                long pos2 = reader.BaseStream.Position;
                                IBZNToken tok2 = reader.ReadToken();
                                if (tok2.Validate("sObject", BinaryFieldType.DATA_UNKNOWN))
                                    return true;
                                return false;
                            }
                            return false;
                        }
                        else if (reader.Format == BZNFormat.Battlezone2)
                        {
                            // 1123 1124 1128 1141 1143 1151
                            if (reader.Version <= 1151 && tok.Validate("dllName", BinaryFieldType.DATA_UNKNOWN)) return true;
                            // 1171, 1187, 1188, 1192
                            if (tok.Validate("groupTargets", BinaryFieldType.DATA_UNKNOWN)) return true;
                            return false;
                        }
                    }*/
                }
                else
                {
                    // early aborts
                    if (!reader.InBinary)
                    {
                        long offset = reader.BaseStream.Position;
                        IBZNToken tok = reader.ReadToken();
                        reader.BaseStream.Position = offset;
                        if (!tok.IsValidationOnly() || !tok.Validate("GameObject", BinaryFieldType.DATA_UNKNOWN))
                        {
                            // next field isn't the start of a GameObject
                            return false;
                        }
                        //if (depth > 50)
                        return true; // Maximum Depth
                    }
                    else
                    {
                        long offset = reader.BaseStream.Position;
                        try
                        {
                            BZNGameObjectWrapper tmp = new BZNGameObjectWrapper(parent, reader, countLeft, LongTermClassLabelLookupCache, depth, RecursiveObjectGenreationMemo, ClassLabelTempLookup, ClassLabelMap, SuccessfulParses, FailedParses, Hints, fake: true);
                        }
                        catch
                        {
                            // next field isn't the start of a GameObject (since a shallow gameobject crashed)
                            reader.BaseStream.Position = offset;
                            return false;
                        }
                        reader.BaseStream.Position = offset;
                        return true;
                    }

                    // just parse the next object and if it works, we're good
                    // (yes, this can become recursive and result in n! object building, but only if we're dealing with a lot of missing data)
                    // TODO move this to a factory pattern so we aren't relying on exceptions from constructors
                    try
                        {
                            long offset = reader.BaseStream.Position;
                            if (RecursiveObjectGenreationMemo.ContainsKey(offset))
                            {
                                reader.BaseStream.Position = RecursiveObjectGenreationMemo[offset].Next;
                                return RecursiveObjectGenreationMemo[offset].Object != null;
                            }

                            BZNGameObjectWrapper? tmpWrap = null;
                            try
                            {
                                tmpWrap = new BZNGameObjectWrapper(parent, reader, countLeft, LongTermClassLabelLookupCache, depth, RecursiveObjectGenreationMemo, ClassLabelTempLookup, ClassLabelMap, SuccessfulParses, FailedParses, Hints);
                                SuccessfulParses?.Add(tmpWrap.PrjID);
                            }
                            catch { }
                            RecursiveObjectGenreationMemo[offset] = (tmpWrap, reader.BaseStream.Position);
                            return tmpWrap != null;
                        }
                        catch { }
                    return false;
                }
            }
            finally
            {
                reader.BaseStream.Position = pos;
            }
            return false;
        }
    }
}
