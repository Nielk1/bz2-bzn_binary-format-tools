using BattlezoneBZNTools.GameObject;
using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools
{
    public class BZNGameObjectWrapper
    {
        public string PrjID { get; set; }
        public UInt16 seqno { get; set; }
        public Vector3D pos { get; set; }
        public UInt32 team { get; set; }
        public string label { get; set; }
        public UInt32 isUser { get; set; }
        public UInt32 obj_addr { get; set; }
        public Matrix transform { get; set; }

        public ClassGameObject gameObject { get; set; }

        public BZNGameObjectWrapper(BZNReader reader)
        {
            IBZNToken tok;
            if (!reader.BinaryMode)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("GameObject", BinaryFieldType.DATA_UNKNOWN)) throw new Exception("Failed to parse [GameObject]");
            }

            if (reader.N64)
            {
                tok = reader.ReadToken();
                UInt16 ItemID = tok.GetUInt16();
                if (!BZNFile.BZn64IdMap.ContainsKey(ItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 PrjID enumeration 0x{0:2X} to string PrjID", ItemID));
                PrjID = BZNFile.BZn64IdMap[ItemID];
            }
            else
            {
                tok = reader.ReadToken();
                if (!tok.Validate("PrjID", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse PrjID/ID");
                PrjID = tok.GetString();
            }

            tok = reader.ReadToken();
            if (!tok.Validate("seqno", BinaryFieldType.DATA_SHORT)) throw new Exception("Failed to parse seqno/SHORT");
            seqno = tok.GetUInt16();

            tok = reader.ReadToken();
            if (!tok.Validate("pos", BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse pos/VEC3D");
            pos = tok.GetVector3D();

            tok = reader.ReadToken();
            if (!tok.Validate("team", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse team/LONG");
            team = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("label", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse label/CHAR");
            label = tok.GetString();

            tok = reader.ReadToken();
            if (!tok.Validate("isUser", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse isUser/LONG");
            isUser = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("obj_addr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse obj_addr/PTR");
            obj_addr = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("transform", BinaryFieldType.DATA_MAT3DOLD)) throw new Exception("Failed to parse transform/MAT3DOLD");
            transform = tok.GetMatrix();

            switch (BZNFile.ClassLabelMap[PrjID])
            {
                case "scrapsilo":
                    gameObject = new ClassScrapSilo();
                    break;
                case "wingman":
                case "turret":
                case "minelayer":
                case "sav":
                    gameObject = new ClassHoverCraft();
                    break;
                case "wpnpower":
                    gameObject = new ClassWeaponPowerup();
                    break;
                case "armory":
                    gameObject = new ClassArmory();
                    break;
                case "recycler":
                    gameObject = new ClassRecycler();
                    break;
                case "factory":
                    gameObject = new ClassFactory();
                    break;
                case "constructionrig":
                    gameObject = new ClassConstructionRig();
                    break;
                case "person":
                    gameObject = new ClassPerson();
                    break;
                case "apc":
                    gameObject = new ClassAPC();
                    break;
                case "scavenger":
                    gameObject = new ClassScavenger();
                    break;
                case "turrettank":
                    gameObject = new ClassTurretTank();
                    break;
                case "howitzer":
                    gameObject = new ClassHowitzer();
                    break;
                case "tug":
                    gameObject = new ClassTug();
                    break;
                case "walker":
                    gameObject = new ClassCraft();
                    break;
                default:
                    gameObject = new ClassGameObject();
                    break;
            }
            gameObject.LoadData(reader);
        }
    }
}
