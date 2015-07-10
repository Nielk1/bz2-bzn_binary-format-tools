using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassGameObject
    {
        public float illumination { get; set; }
        public Vector3D pos { get; set; }
        public Euler euler { get; set; }
        public UInt32 seqNo { get; set; }
        public string name { get; set; }
        public bool isObjective { get; set; }
        public bool isSelected { get; set; }
        public UInt32 isVisible { get; set; }
        public UInt32 seen { get; set; }
        public float healthRatio { get; set; }
        public UInt32 curHealth { get; set; }
        public UInt32 maxHealth { get; set; }
        public float ammoRatio { get; set; }
        public UInt32 curAmmo { get; set; }
        public UInt32 maxAmmo { get; set; }
        public UInt32 priority { get; set; }
        public UInt32 what { get; set; }
        //public UInt32 who { get; set; }
        public Int32 who { get; set; }
        public UInt32 where { get; set; }
        public UInt32 param { get; set; }
        public bool aiProcess { get; set; }
        public bool isCargo { get; set; }
        public UInt32 independence { get; set; }
        public string curPilot { get; set; }
        public Int32 perceivedTeam { get; set; }

        public ClassGameObject() { }
        public virtual void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("illumination", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse illumination/FLOAT");
            illumination = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("pos", BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse pos/VEC3D");
            pos = tok.GetVector3D();

            if (reader.BinaryMode)
            {
                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                float euler_mass = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                float euler_mass_inv = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                float euler_v_mag = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                float euler_v_mag_inv = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                float euler_I = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler's FLOAT");
                float euler_k_i = tok.GetSingle();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse euler's EC3D");
                Vector3D euler_v = tok.GetVector3D();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse euler's EC3D");
                Vector3D euler_omega = tok.GetVector3D();

                tok = reader.ReadToken();
                if (!tok.Validate(null, BinaryFieldType.DATA_VEC3D)) throw new Exception("Failed to parse euler's EC3D");
                Vector3D euler_Accel = tok.GetVector3D();

                euler = new Euler()
                {
                    mass = euler_mass,
                    mass_inv = euler_mass_inv,
                    v_mag = euler_v_mag,
                    v_mag_inv = euler_v_mag_inv,
                    I = euler_I,
                    k_i = euler_k_i,
                    v = euler_v,
                    omega = euler_omega,
                    Accel = euler_Accel
                };
            }
            else
            {
                tok = reader.ReadToken();
                if (!tok.Validate("euler", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse euler");
                euler = tok.GetEuler();
            }

            tok = reader.ReadToken();
            if (!tok.Validate("seqNo", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse seqNo/SHORT");
            seqNo = tok.GetUInt16();

            if (reader.N64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("name", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
                name = tok.GetString();
            }
            else
            {
                if (reader.Version > 1030)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("name", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
                    name = tok.GetString();
                }
            }

            tok = reader.ReadToken();
            if (!tok.Validate("isObjective", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse isObjective/BOOL");
            isObjective = tok.GetBoolean();

            tok = reader.ReadToken();
            if (!tok.Validate("isSelected", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse isSelected/BOOL");
            isSelected = tok.GetBoolean();

            tok = reader.ReadToken();
            if (!tok.Validate("isVisible", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse isVisible/LONG");
            isVisible = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("seen", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse seen/LONG");
            seen = tok.GetUInt32H();

            if (reader.N64)
            {

            }
            else
            {
                //[10:03:38 PM] Kenneth Miller: I think I may have figured out what that stuff is, maybe
                //[10:03:50 PM] Kenneth Miller: They're timestamps
                //[10:04:04 PM] Kenneth Miller: playerShot, playerCollide, friendShot, friendCollide, enemyShot, groundCollide
                //[10:04:13 PM] Kenneth Miller: the default value is -HUGE_NUMBER (-1e30)
                //[10:04:26 PM] Kenneth Miller: And due to the nature of the game, groundCollide is the most likely to get set first
                //[10:05:02 PM] Kenneth Miller: Old versions of the mission format used to contain those values but later versions only include them in the savegame
                //[10:05:05 PM] Kenneth Miller: (not the mission)
                //[10:05:31 PM] Kenneth Miller: (version 1033 was where they were removed from the mission)
                if (reader.Version < 1033)
                {
                    tok = reader.ReadToken(); // float (-HUGE_NUMBER)
                    tok = reader.ReadToken(); // float (-HUGE_NUMBER)
                    tok = reader.ReadToken(); // float (-HUGE_NUMBER)
                    tok = reader.ReadToken(); // float (-HUGE_NUMBER)
                    tok = reader.ReadToken(); // float (-HUGE_NUMBER)
                    tok = reader.ReadToken(); // float
                }
            }

            tok = reader.ReadToken();
            if (!tok.Validate("healthRatio", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse healthRatio/FLOAT");
            healthRatio = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("curHealth", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse curHealth/LONG");
            curHealth = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("maxHealth", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse maxHealth/LONG");
            maxHealth = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("ammoRatio", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse ammoRatio/FLOAT");
            ammoRatio = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("curAmmo", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse curAmmo/LONG");
            curAmmo = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("maxAmmo", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse maxAmmo/LONG");
            maxAmmo = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("priority", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse priority/LONG");
            priority = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("what", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse what/VOID");
            what = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("who", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse who/LONG");
            who = tok.GetInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("where", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse where/PTR");
            where = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("param", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse param/LONG");
            param = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("aiProcess", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse aiProcess/BOOL");
            aiProcess = tok.GetBoolean();

            tok = reader.ReadToken();
            if (!tok.Validate("isCargo", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse isCargo/BOOL");
            isCargo = tok.GetBoolean();

            tok = reader.ReadToken();
            if (!tok.Validate("independence", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse independence/LONG");
            independence = tok.GetUInt32();

            if (reader.N64)
            {
                tok = reader.ReadToken();
                UInt16 curPilotID = tok.GetUInt16();
                if (!BZNFile.BZn64IdMap.ContainsKey(curPilotID)) throw new InvalidCastException(string.Format("Cannot convert n64 curPilotID enumeration 0x{0:2X} to string curPilotID", curPilotID));
                curPilot = BZNFile.BZn64IdMap[curPilotID];
            }
            else
            {
                tok = reader.ReadToken();
                if (!tok.Validate("curPilot", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse curPilot/ID");
                curPilot = tok.GetString();
            }

            if (reader.N64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("perceivedTeam", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse perceivedTeam/LONG");
                perceivedTeam = tok.GetInt32();
            }
            else
            {
                if (reader.Version > 1030)
                {
                    tok = reader.ReadToken();
                    if (!tok.Validate("perceivedTeam", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse perceivedTeam/LONG");
                    perceivedTeam = tok.GetInt32();
                }
                else
                {
                    perceivedTeam = -1;
                }
            }
        }
    }
}
