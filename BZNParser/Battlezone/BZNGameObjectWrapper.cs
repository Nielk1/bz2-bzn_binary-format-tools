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

        public Entity gameObject { get; set; }


        private Dictionary<string, HashSet<string>> LongTermClassLabelLookupCache;
        private Dictionary<string, IClassFactory> ClassLabelMap;

        // TODO move this to a factory pattern so we aren't relying on exceptions from constructors
        public BZNGameObjectWrapper(BZNFileBattlezone parent, BZNStreamReader reader, int countLeft, Dictionary<string, HashSet<string>> LongTermClassLabelLookupCache, Dictionary<long, (Entity Object, long Next)> RecursiveObjectGenreationMemo = null, Dictionary<string, string> ClassLabelTempLookup = null, Dictionary<string, IClassFactory> ClassLabelMap = null, BattlezoneBZNHints? Hints = null, bool fake = false)
        {
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

            gameObject = ParseGameObject(parent, reader, countLeft, RecursiveObjectGenreationMemo, ClassLabelTempLookup, Hints);
        }

        private Entity ParseGameObject(BZNFileBattlezone parent, BZNStreamReader reader, int countLeft, Dictionary<long, (Entity Object, long Next)> RecursiveObjectGenreationMemo, Dictionary<string, string> ClassLabelTempLookup, BattlezoneBZNHints Hints)
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

            Entity? gameObject = null;

            {
                reader.Bookmark.Push();
                try
                {
                    if (ValidClassLabels != null && ValidClassLabels.Count == 1)
                    {
                        string label = ValidClassLabels.First();
                        IClassFactory classFactory = ClassLabelMap[label];
                        if (classFactory.Create(reader, PrjID, isUser != 0, label, out gameObject))
                        {
                            reader.Bookmark.Discard();
                            return gameObject; // success! Keep the stream position where it is
                        }
                        else
                        {
                            reader.Bookmark.Pop();
                            gameObject = null;
                        }
                    }
                    else
                    {
                        reader.Bookmark.Discard();
                    }
                }
                catch
                {
                    // roll back to the start of the object
                    reader.Bookmark.Pop();
                    gameObject = null;
                }
            }

            // if we're here, we failed to load the game object so now recursive hell starts
            //if (gameObject == null)
            {
                // try every possible object
                reader.Bookmark.Push();
                Entity tempGameObject;

                
                List<(Entity Object, bool Expected, long Next, string Name)> Candidates = new List<(Entity Object, bool Expected, long Next, string Name)>();
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
                                IClassFactory classFactory = kv.Value;
                                if (classFactory.Create(reader, PrjID, isUser != 0, classLableTempHolder, out tempGameObject) && tempGameObject != null)
                                {
                                    firstParseSuccess = true;
                                    if (CheckNext(parent, reader, countLeft - 1, RecursiveObjectGenreationMemo, ClassLabelTempLookup, Hints))
                                        Candidates.Add((tempGameObject, ValidClassLabels?.Contains(classLableTempHolder) ?? false, reader.Bookmark.Get(), classLableTempHolder));
                                }
                                else
                                {
                                    tempGameObject = null;
                                }
                            }
                            catch
                            {
                            }
                        reader.Bookmark.Peek();
                    }
                }
                reader.Bookmark.Pop();

                ClassLabelTempLookup.Remove(PrjID.ToLowerInvariant());

                if (!LongTermClassLabelLookupCache.ContainsKey(PrjID.ToLowerInvariant()))
                    LongTermClassLabelLookupCache[PrjID.ToLowerInvariant()] = new HashSet<string>(Candidates.Select(dr => dr.Name));
                LongTermClassLabelLookupCache[PrjID.ToLowerInvariant()] = new HashSet<string>(LongTermClassLabelLookupCache[PrjID.ToLowerInvariant()].Intersect(Candidates.Select(dr => dr.Name)));

                if (Candidates.Count > 0)
                {
                    if (Candidates.Count == 1)
                    {
                        reader.Bookmark.Set(Candidates[0].Next);
                        return Candidates[0].Object;
                    }
                    else
                    {
                        reader.Bookmark.Set(Candidates.Min(dr => dr.Next));
                        return new MultiClass(PrjID, isUser != 0, Candidates);
                    }
                }
                else
                {
                    throw new Exception($"Failed to parse GameObject {PrjID}");
                }
            }

            return null;
        }

        private bool CheckNext(BZNFileBattlezone parent, BZNStreamReader reader, int countLeft, Dictionary<long, (Entity? Object, long Next)> RecursiveObjectGenreationMemo, Dictionary<string, string> ClassLabelTempLookup, BattlezoneBZNHints Hints)
        {
            if (countLeft == 0)
            {
                reader.Bookmark.Push();
                try
                {
                    parent.TailParse(reader);
                }
                catch
                {
                    reader.Bookmark.Pop();
                    return false;
                }
                reader.Bookmark.Pop();
                return true;
            }
            else
            {
                if (!reader.InBinary)
                {
                    reader.Bookmark.Push();
                    IBZNToken tok = reader.ReadToken();
                    reader.Bookmark.Pop();
                    if (!tok.IsValidationOnly() || !tok.Validate("GameObject", BinaryFieldType.DATA_UNKNOWN))
                    {
                        // next field isn't the start of a GameObject
                        return false;
                    }
                    return true;
                }
                else
                {
                    reader.Bookmark.Push();
                    try
                    {
                        BZNGameObjectWrapper tmp = new BZNGameObjectWrapper(parent, reader, countLeft, LongTermClassLabelLookupCache, RecursiveObjectGenreationMemo, ClassLabelTempLookup, ClassLabelMap, Hints, fake: true);
                        reader.Bookmark.Pop();
                        return true;
                    }
                    catch
                    {
                        // next field isn't the start of a GameObject (since a shallow gameobject crashed)
                        reader.Bookmark.Pop();
                        return false;
                    }
                }
            }
        }
    }
}
