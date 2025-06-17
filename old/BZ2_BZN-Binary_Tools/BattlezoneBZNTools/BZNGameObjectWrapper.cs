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
                if (!BZNFile.BZn64IdMap.ContainsKey(ItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 PrjID enumeration 0x(0:X2} to string PrjID", ItemID));
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
            if (reader.N64)
            {
                if (!tok.Validate("label", BinaryFieldType.DATA_SHORT)) throw new Exception("Failed to parse label/CHAR");
                label = string.Format("bzn64label_{0,4:X4}", tok.GetUInt16());
            }
            else
            {
                if (!tok.Validate("label", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse label/CHAR");
                label = tok.GetString();
            }

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
                    gameObject = new ClassScrapSilo(PrjID, isUser != 0);
                    break;
                case "wingman":
                case "turret":
                case "minelayer":
                case "sav":
                    gameObject = new ClassHoverCraft(PrjID, isUser != 0);
                    break;
                case "wpnpower":
                    gameObject = new ClassWeaponPowerup(PrjID, isUser != 0);
                    break;
                case "armory":
                    gameObject = new ClassArmory(PrjID, isUser != 0);
                    break;
                case "recycler":
                    gameObject = new ClassRecycler(PrjID, isUser != 0);
                    break;
                case "factory":
                    gameObject = new ClassFactory(PrjID, isUser != 0);
                    break;
                case "constructionrig":
                    gameObject = new ClassConstructionRig(PrjID, isUser != 0);
                    break;
                case "person":
                    gameObject = new ClassPerson(PrjID, isUser != 0);
                    break;
                case "apc":
                    gameObject = new ClassAPC(PrjID, isUser != 0);
                    break;
                case "scavenger":
                    gameObject = new ClassScavenger(PrjID, isUser != 0);
                    break;
                case "turrettank":
                    gameObject = new ClassTurretTank(PrjID, isUser != 0);
                    break;
                case "howitzer":
                    gameObject = new ClassHowitzer(PrjID, isUser != 0);
                    break;
                case "tug":
                    gameObject = new ClassTug(PrjID, isUser != 0);
                    break;
                case "walker":
                    gameObject = new ClassCraft(PrjID, isUser != 0);
                    break;
                default:
                    gameObject = new ClassGameObject(PrjID, isUser != 0);
                    break;
            }
            gameObject.LoadData(reader);
        }

        public string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("[GameObject]");
            sb.AppendLine("PrjID [1] =");
            sb.AppendLine(PrjID);
            sb.AppendLine("seqno [1] =");
            sb.AppendLine(seqno.ToString());
            sb.AppendLine("pos [1] =");
            sb.AppendLine("  x [1] =");
            sb.AppendLine(pos.x.ToString());
            sb.AppendLine("  y [1] =");
            sb.AppendLine(pos.y.ToString());
            sb.AppendLine("  z [1] =");
            sb.AppendLine(pos.z.ToString());
            sb.AppendLine("team [1] =");
            sb.AppendLine(team.ToString());
            sb.AppendLine("label = " + label);
            sb.AppendLine("isUser [1] =");
            sb.AppendLine(isUser.ToString());
            sb.AppendLine(string.Format("obj_addr = {0:X8}", obj_addr));
            sb.AppendLine("transform [1] =");
            sb.AppendLine("  right_x [1] =");
            sb.AppendLine(transform.right.x.ToString());
            sb.AppendLine("  right_y [1] =");
            sb.AppendLine(transform.right.y.ToString());
            sb.AppendLine("  right_z [1] =");
            sb.AppendLine(transform.right.z.ToString());
            sb.AppendLine("  up_x [1] =");
            sb.AppendLine(transform.up.x.ToString());
            sb.AppendLine("  up_y [1] =");
            sb.AppendLine(transform.up.y.ToString());
            sb.AppendLine("  up_z [1] =");
            sb.AppendLine(transform.up.z.ToString());
            sb.AppendLine("  front_x [1] =");
            sb.AppendLine(transform.front.x.ToString());
            sb.AppendLine("  front_y [1] =");
            sb.AppendLine(transform.front.y.ToString());
            sb.AppendLine("  front_z [1] =");
            sb.AppendLine(transform.front.z.ToString());
            sb.AppendLine("  posit_x [1] =");
            sb.AppendLine(transform.posit.x.ToString());
            sb.AppendLine("  posit_y [1] =");
            sb.AppendLine(transform.posit.y.ToString());
            sb.AppendLine("  posit_z [1] =");
            sb.AppendLine(transform.posit.z.ToString());

            sb.Append(gameObject.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
