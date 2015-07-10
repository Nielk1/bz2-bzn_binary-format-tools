using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryBZNFile
{
    public class ProcessedBZN
    {
        #region ClassLabelMap
        public static readonly Dictionary<string, string> ClassLabelMap = new Dictionary<string, string>
        {
            {"abbar7", "i76building"},
            {"abbarr", "barracks"},
            {"abcafe", "i76building"},
            {"abcomm", "commtower"},
            {"abhang", "repairdepot"},
            {"abhqcp", "i76building"},
            {"abhqc3", "i76building"},
            {"ablpad", "i76building"},
            {"ablpow", "powerplant"},
            {"abmbld", "supplydepot"},
            {"abshld", "i76building"},
            {"absilo", "scrapsilo"},
            {"abspow", "powerplant"},
            {"abstor", "i76building"},
            {"absupp", "supplydepot"},
            {"abtowe", "turret"},
            {"abwpow", "powerplant"},
            {"aspilo", "person"},
            {"asuser", "person"},
            {"avapc", "apc"},
            {"avartl", "howitzer"},
            {"avcns9", "constructionrig"},
            {"avcnst", "constructionrig"},
            {"avfigh", "wingman"},
            {"avhaul", "tug"},
            {"avhraz", "wingman"},
            {"avland", "turret"},
            {"avlamp", "turrettank"},
            {"avltnk", "wingman"},
            {"avmine", "minelayer"},
            {"avmuf", "factory"},
            {"avmuf4", "factory"},
            {"avrec2", "recycler"},
            {"avrec3", "recycler"},
            {"avrec4", "recycler"},
            {"avrec5", "recycler"},
            {"avrec6", "recycler"},
            {"avrec8", "recycler"},
            {"avrecy", "recycler"},
            {"avret3", "recycler"},
            {"avret4", "recycler"},
            {"avsav", "sav"},
            {"avscav", "scavenger"},
            {"avsl7", "armory"},
            {"avslf", "armory"},
            {"avslf5", "armory"},
            {"avtank", "wingman"},
            {"avturr", "turrettank"},
            {"avtest", "turrettank"},
            {"avwalk", "walker"},

            {"bvapc", "apc"},
            {"bvartl", "howitzer"},
            {"bvcnst", "constructionrig"},
            {"bvfigh", "wingman"},
            {"bvhaul", "tug"},
            {"bvhraz", "wingman"},
            {"bvltnk", "wingman"},
            {"bvmine", "minelayer"},
            {"bvmuf", "factory"},
            {"bvrec8", "recycler"},
            {"bvrecy", "recycler"},
            {"bvscav", "scavenger"},
            {"bvslf", "armory"},
            {"bvtank", "wingman"},
            {"bvturr", "turrettank"},
            {"bvwalk", "walker"},
            
            {"savmf", "factory"},
            {"sbbarr", "barracks"},
            {"sbcafe", "i76building"},
            {"sbcom7", "commtower"},
            {"sbcomm", "commtower"},
            {"sbhang", "repairdepot"},
            {"sbhqcp", "i76building"},
            {"sbhqt1", "i76building"},
            {"sbhqt2", "i76building"},
            {"sblpad", "i76building"},
            {"sblpd2", "i76building"},
            {"sblpow", "powerplant"},
            {"sbshld", "i76building"},
            {"sbsilo", "scrapsilo"},
            {"sbspow", "powerplant"},
            {"sbsupp", "supplydepot"},
            {"sbtowe", "turret"},
            {"sbwpow", "powerplant"},
            {"sspilo", "person"},
            {"svap2", "tug"},
            {"svap7", "tug"},
            {"svapc", "apc"},
            {"svartl", "howitzer"},
            {"svcnst", "constructionrig"},
            {"svfigh", "wingman"},
            {"svhaul", "tug"},
            {"svhraz", "wingman"},
            {"svltnk", "wingman"},
            {"svmine", "minelayer"},
            {"svmuf", "factory"},
            {"svmuf13", "factory"},
            {"svrecy", "recycler"},
            {"svsav", "sav"},
            {"svscav", "scavenger"},
            {"svslf", "armory"},
            {"svtank", "wingman"},
            {"svturr", "turrettank"},
            {"svwalk", "walker"},

            {"cbport", "portal"},

            // powerups
            {"apammo", "ammopack"},
            {"spammo", "ammopack"},
            {"aprepa", "repairkit"},
            {"sprepa", "repairkit"},
            //daywreckers
            {"apwrck", "daywrecker"},
            {"spwrck", "daywrecker"},
            //camerapods
            {"apcamr", "camerapod"},
            {"spcamr", "camerapod"},
            {"apbase", "camerapod"},
            {"apscrap", "camerapod"},
            //weapon powerups
            {"apstab", "wpnpower"},
            {"spstab", "wpnpower"},
            {"appopg", "wpnpower"},
            {"aptech", "wpnpower"},
            {"apflsh", "wpnpower"},
            {"apsand", "wpnpower"},

            {"boltmine", "weaponmine"},
            {"eggeizr1", "geyser"},
            {"flare", "flare"},
            {"magpull", "magnet"},
            {"nparr", "i76sign"},

            {"oblema", "i76building"},
            {"oblemc", "i76building"},
            {"obcycl", "i76building"},
            {"obheph", "i76building"},
            {"obstp1", "i76building"},
            {"obstp3", "i76building"},
            {"obstp8", "i76building"},

            {"hbblda", "i76building"},
            {"hbbldb", "i76building"},
            {"hbbldc", "i76building"},
            {"hbbldd", "i76building"},
            {"hbblde", "i76building"},
            {"hbbldf", "i76building"},
            {"hbcerb", "i76building"},
            {"hbchar", "i76building"},
            {"hbcrys", "i76building"},
            {"hbtran00", "i76building"},
            {"hbtrn200", "i76building"},
            {"hbfact", "i76building"},
            {"hvsrb", "artifact"},
            {"hvsav", "sav"},
            {"hvsat", "wingman"},

            {"ubtart", "i76building"},

            {"npscr1", "scrap"},
            {"npscr2", "scrap"},
            {"npscr3", "scrap"},
            {"sscr_1", "scrap"},

            {"obdata", "artifact"},
            {"player", "wingman"},
            {"playrt", "wingman"},
            {"playsv", "wingman"},
            {"proxmine", "proximity"},
            {"pspwn_1", "spawnpnt"},
            {"sfield", "scrapfield"},
            {"splintbm", "spraybomb"},
            {"waspmsl", "torpedo"},

            {"bvarmo", "armory"},//{"UKN_BVARMO", "armory"}, // or factory
            {"bvrckt", "wingman"},//{"UKN_BVRCKT", "wingman"},
            {"bvcons", "constructionrig"},
            {"sbmbld", "powerplant"},//{"sbmbld", "supplydepot"},//{"UKN_SBMBLD", "powerplant"},
            {"svtug", "tug"},//{"UKN_SVTUG", "tug"}, // or person?
            {"svrckt", "wingman"},//{"UKN_SVRCKT", "wingman"},
            {"avtug", "tug"},//{"UKN_AVTUG", "tug"},
            {"avrckt", "wingman"},//{"UKN_AVRCKT", "wingman"},
            
            // n64 items
            {"avcnst_n64_1", "constructionrig"},
            {"avcnst_n64_3", "constructionrig"},
            {"avcnst_n64_4", "constructionrig"},
            {"avcnst_n64_5", "constructionrig"},
            
            {"avmuf_n64_1", "factory"},
            {"avmuf_n64_2", "factory"},
            {"avmuf_n64_3", "factory"},
            {"avmuf_n64_4", "factory"},
            {"avmuf_n64_5", "factory"},
            
            {"avrecy_n64_1", "recycler"},
            {"avrecy_n64_2", "recycler"},
            {"avrecy_n64_3", "recycler"},
            {"avrecy_n64_4", "recycler"},
            {"avrecy_n64_5", "recycler"},
            {"avrecy_n64_6", "recycler"},
            {"avrecy_n64_7", "recycler"},
            {"avrecy_n64_8", "recycler"},
            {"avrecy_n64_9", "recycler"},
            
            {"bvrecy_n64_1", "recycler"},
            {"bvrecy_n64_2", "recycler"},
            {"bvrecy_n64_3", "recycler"},
            
            {"svmuf_n64_1", "factory"},
            {"svmuf_n64_2", "factory"},
            {"svmuf_n64_3", "factory"},
            
            {"svrecy_n64_1", "recycler"},
            {"svrecy_n64_2", "recycler"},
            {"svrecy_n64_3", "recycler"},
            {"svrecy_n64_4", "recycler"},
            {"svrecy_n64_5", "recycler"},
            {"svrecy_n64_6", "recycler"},
            {"svrecy_n64_7", "recycler"},
            
            {"svslf_n64_ammo_hull_only", "armory"},

            {"svscav_n64_1", "scavenger"},

            {"svmammoth", "wingman"},
        };
        #endregion

        public static readonly string[] KnownItemsFromBZ1 = new string[]
        {

        };
        public static int MASTERIDX = 0;

        #region BZn64IdMap
        public static readonly Dictionary<UInt16, string> BZn64IdMap = new Dictionary<UInt16, string>
        {
            {0x0000, string.Empty},
            {0x0001, "asuser"},
            {0x0003, "abbarr"},
            {0x0004, "abcafe"},
            {0x0005, "abcomm"},
            {0x0006, "abhang"},
            {0x0007, "abhqc3"},
            {0x0008, "abhqcp"},
            {0x0009, "ablpad"},
            {0x000A, "ablpow"},
            {0x000B, "abmbld"},
            {0x000C, "abshld"},
            {0x000D, "absilo"},
            {0x000E, "abspow"},
            {0x000F, "abstor"},
            {0x0010, "absupp"},
            {0x0011, "abtowe"},
            {0x0015, "abwpow"},
            {0x0017, "apammo"},
            {0x0019, "apbase"},
            {0x001D, "apcamr"},
            {0x0021, "apflsh"},
            {0x002B, "appopg"},
            {0x002E, "aprepa"},
            {0x0030, "apsand"},
            {0x0031, "apscrap"},
            {0x0038, "aptech"},
            {0x003B, "aspilo"},
            {0x0040, "avartl"},
            {0x0042, "avcnst_n64_1"},
            {0x0043, "avcns9"},
            {0x0044, "avcnst_n64_3"},
            {0x0045, "avcnst_n64_4"},
            {0x0047, "avcnst_n64_5"},
            {0x0048, "avfigh"},
            {0x004A, "avhaul"},
            {0x004B, "avhraz"},
            {0x004E, "avland"},
            {0x0050, "avltnk"},
            {0x0054, "avmuf_n64_1"},
            {0x0058, "avmuf_n64_2"},
            {0x0059, "avmuf_n64_3"},
            {0x005A, "avmuf_n64_4"},
            {0x005B, "avmuf_n64_5"},
            {0x005C, "avmuf4"},
            {0x005F, "avrckt"},
            {0x0060, "avrec2"},
            {0x0061, "avrec3"},
            {0x0062, "avrec4"},
            {0x0063, "avrec5"},
            {0x0064, "avrec6"},
            {0x0066, "avrecy_n64_1"},
            {0x0067, "avrecy_n64_2"},
            {0x0068, "avrecy_n64_3"},
            {0x0069, "avrecy_n64_4"},
            {0x006A, "avrecy_n64_5"},
            {0x006B, "avrecy_n64_6"},
            {0x006C, "avrecy_n64_7"},
            {0x006D, "avrecy_n64_8"},
            {0x006E, "avret3"},
            {0x006F, "avret4"},
            {0x0072, "avscav"},
            {0x0073, "avsl7"},
            {0x0075, "avslf"},
            {0x0076, "avslf5"},
            {0x0079, "avtank"},
            {0x007A, "avtest"},
            {0x007C, "avturr"},
            {0x007D, "avwalk"},
            {0x009A, "boltmine"},
            {0x00A0, "bvapc"},
            {0x00A2, "avtug"},
            {0x00A3, "bvhraz"},
            {0x00A4, "bvltnk"},
            {0x00A6, "bvmuf"},
            {0x00A8, "bvfigh"},
            {0x00A9, "bvrckt"},
            {0x00AA, "bvrec8"},
            {0x00AB, "bvrecy"},
            {0x00AD, "bvscav"},
            {0x00AE, "bvarmo"},
            {0x00AF, "bvtank"},
            {0x00B2, "bvturr"},
            {0x00B3, "bvwalk"},
            {0x00BD, "eggeizr1"},
            {0x00EC, "hbblda"},
            {0x00ED, "hbbldb"},
            {0x00EE, "hbbldc"},
            {0x00EF, "hbbldd"},
            {0x00F0, "hbblde"},
            {0x00F1, "hbbldf"},
            {0x00F2, "hbcerb"},
            {0x00F3, "hbchar"},
            {0x00F4, "hbcrys"},
            {0x00F7, "hbfact"},
            {0x0100, "hbtran00"},
            {0x0101, "hbtrn200"},
            {0x0104, "hvsat"},
            {0x0105, "hvsav"},
            {0x0106, "hvsrb"},
            {0x010B, "nparr"},
            {0x010C, "npscr1"},
            {0x010D, "npscr2"},
            {0x010E, "npscr3"},
            {0x0112, "obcycl"},
            {0x0113, "obdata"},
            {0x0115, "obheph"},
            {0x0119, "oblema"},
            {0x011B, "oblemc"},
            {0x0120, "obstp1"},
            {0x0122, "obstp3"},
            {0x0127, "obstp8"},
            {0x012A, "player"},
            {0x012B, "playrt"},
            {0x012C, "playsv"},
            {0x012F, "proxmine"},
            {0x0135, "savmf"},
            {0x0137, "svsav"},
            {0x0138, "sbbarr"},
            {0x0139, "sbcafe"},
            {0x013A, "sbcom7"},
            {0x013B, "sbcomm"},
            {0x013C, "sbhang"},
            {0x013D, "sbhqcp"},
            {0x013E, "sbhqt1"},
            {0x013F, "sbhqt2"},
            {0x0140, "sblpad"},
            {0x0141, "sblpd2"},
            {0x0142, "sblpow"},
            {0x0143, "sbmbld"},
            {0x0144, "sbshld"},
            {0x0145, "sbsilo"},
            {0x0146, "sbspow"},
            {0x0147, "sbsupp"},
            {0x0148, "sbtowe"},
            {0x0149, "sbwpow"},
            {0x014B, "sfield"},
            {0x014E, "spammo"},
            {0x0152, "spcamr"},
            {0x0164, "sprepa"},
            {0x016F, "sscr_1"},
            {0x0170, "sspilo"},
            {0x0172, "ssuser"},
            {0x0173, "svap2"},
            {0x0174, "svap7"},
            {0x0175, "svapc"},
            {0x0178, "svartl"},
            {0x017A, "svcnst"},
            {0x017D, "svfigh"},
            {0x017F, "svtug"},
            {0x0181, "svhraz"},
            {0x0184, "svltnk"},
            {0x0187, "svmuf_n64_1"},
            {0x0188, "svmuf_n64_2"},
            {0x0189, "svmuf_n64_3"},
            {0x018A, "svmuf13"},
            {0x018B, "svrckt"},
            {0x018C, "svrecy_n64_1"},
            {0x018D, "svrecy_n64_2"},
            {0x018E, "svrecy_n64_3"},
            {0x018F, "svrecy_n64_6"},
            {0x0190, "svrecy_n64_5"},
            {0x0191, "svrecy_n64_6"},
            {0x0193, "svscav_n64_1"},
            {0x0194, "svslf"},
            {0x0195, "svtank"},
            {0x019A, "svturr"},
            {0x019B, "svwalk"},
            {0x01A0, "ubtart"},
            {0x01B1, "abbar7"},
            {0x01B3, "avrecy_n64_9"},
            {0x01B4, "svrecy_n64_7"},
            {0x01B6, "bvcons"},
            {0x01B9, "bvrecy_n64_1"},
            {0x01BB, "bvrecy_n64_2"},
            {0x01BC, "bvrecy_n64_3"},
            {0x01BD, "svmammoth"},
            {0x01C0, "svslf_n64_ammo_hull_only"},
        };
        #endregion

        public int version { get; set; }
        public bool binarySave { get; set; }

        public string msn_filename { get; set; }
        public Int32 seq_count { get; set; }
        public bool missionSave { get; set; }
        public string TerrainName { get; set; }

        public List<BZNGameObjectWrapper> GameObjects { get; private set; }

        public ProcessedBZN(IRawBZN bzn)
        {
            int idx = 0;

            bool bzn64 = bzn.asciiFields == null || bzn.asciiFields.Count == 0;

            if (bzn64)
            {
                version = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                binarySave = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                version = int.Parse(bzn.asciiFields[0].value[0]);
                binarySave = bool.Parse(bzn.asciiFields[1].value[0]);
            }

            if(binarySave)
            {
                msn_filename = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                msn_filename = msn_filename.IndexOf('\0') > -1 ? msn_filename.Substring(0, msn_filename.IndexOf('\0')) : msn_filename;

                if(bzn64)
                {
                    seq_count = -1;
                    missionSave = false;
                    TerrainName = null;
                }
                else
                {
                    seq_count = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                    missionSave = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                    TerrainName = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                    TerrainName = TerrainName.IndexOf('\0') > -1 ? TerrainName.Substring(0, TerrainName.IndexOf('\0')) : TerrainName;
                }

                Int32 CountItems;
                if(bzn64)
                {
                    CountItems = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                }
                else
                {
                    CountItems = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                }

                GameObjects = new List<BZNGameObjectWrapper>(CountItems);

                for (int i = 0; i < CountItems; i++)
                {
                    GameObjects.Add(new BZNGameObjectWrapper(bzn, ref idx, version, bzn64));
                }
            }
        }
    }

    public struct Vector3D
    {
        public float x;
        public float y;
        public float z;
    }

    public struct Vector2D
    {
        public float x;
        public float z;
    }

    public struct Euler
    {
        public float mass;
        public float mass_inv;

        public float v_mag;
        public float v_mag_inv;

        public float I;
        public float k_i;

        public Vector3D v;
        public Vector3D omega;
        public Vector3D Accel;
    }

    public struct Matrix
    {
        public Vector3D right;
        public Vector3D up;
        public Vector3D front;
        public Vector3D posit;
    }

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

        public BZNGameObject gameObject { get; set; }

        public BZNGameObjectWrapper(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if (bzn64)
            {
                UInt16 ItemID = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);

                if (ProcessedBZN.BZn64IdMap.ContainsKey(ItemID))
                {
                    PrjID = ProcessedBZN.BZn64IdMap[ItemID];
                    //                    Console.WriteLine("            {" + string.Format("0x{0,4:X4}", ItemID) + ", \"" + PrjID + "\"}");
                }
                else
                {
                    PrjID = ProcessedBZN.KnownItemsFromBZ1[ProcessedBZN.MASTERIDX];

                    while ((new string[] { "sscr_1", "npscr1", "npscr2", "npscr3" }.Contains(PrjID) // Matches Scrap in list
                        && !new UInt16[] { 0x016F, 0x010C, 0x010D, 0x010E }.Contains(ItemID)) // Not a scrap ID
                        )//|| (ProcessedBZN.BZn64IdMap.ContainsValue(PrjID))) // already mapped this, must be an error, skip
                    {
                        ProcessedBZN.MASTERIDX++;
                        PrjID = ProcessedBZN.KnownItemsFromBZ1[ProcessedBZN.MASTERIDX];
                    }

                    //                    Console.WriteLine("            {" + string.Format("0x{0,4:X4}", ItemID) + ", \"" + PrjID + "\"},");
                }

                ProcessedBZN.MASTERIDX++;

                seqno = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);

                float posx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float posy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                float posz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                idx++;
                pos = new Vector3D() { x = posx, y = posy, z = posz };

                team = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);

                label = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                label = label.IndexOf('\0') > -1 ? label.Substring(0, label.IndexOf('\0')) : label;

                isUser = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);

                obj_addr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);

                transform = new Matrix();// bzn.fields[idx++].GetRawRef().Reverse().ToArray();
            }
            else
            {
                PrjID = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                PrjID = PrjID.IndexOf('\0') > -1 ? PrjID.Substring(0, PrjID.IndexOf('\0')) : PrjID;

                //Console.WriteLine(PrjID);

                seqno = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().ToArray(), 0);

                float posx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                float posy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                float posz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                idx++;
                pos = new Vector3D() { x = posx, y = posy, z = posz };

                team = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);

                label = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                label = label.IndexOf('\0') > -1 ? label.Substring(0, label.IndexOf('\0')) : label;

                isUser = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);

                obj_addr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);

                transform = new Matrix();// bzn.fields[idx++].GetRawRef().ToArray();
            }

            switch (ProcessedBZN.ClassLabelMap[PrjID])
            {
                case "scrapsilo":
                    gameObject = new BZNScrapSilo();
                    break;
                case "wingman":
                case "turret":
                case "minelayer":
                case "sav":
                    gameObject = new BZNHoverCraft();
                    break;
                case "wpnpower":
                    gameObject = new BZNWeaponPowerup();
                    break;
                case "armory":
                    gameObject = new BZNArmory();
                    break;
                case "recycler":
                    gameObject = new BZNRecycler();
                    break;
                case "factory":
                    gameObject = new BZNFactory();
                    break;
                case "constructionrig":
                    gameObject = new BZNConstructionRig();
                    break;
                case "person":
                    gameObject = new BZNPerson();
                    break;
                case "apc":
                    gameObject = new BZNAPC();
                    break;
                case "scavenger":
                    gameObject = new BZNScavenger();
                    break;
                case "turrettank":
                    gameObject = new BZNTurretTank();
                    break;
                case "howitzer":
                    gameObject = new BZNHowitzer();
                    break;
                case "tug":
                    gameObject = new BZNTug();
                    break;
                case "walker":
                    gameObject = new BZNCraft();
                    break;
                default:
                    gameObject = new BZNGameObject();
                    break;
            }
            gameObject.LoadData(bzn, ref idx, version, bzn64);
        }

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
                if (!ProcessedBZN.BZn64IdMap.ContainsKey(ItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 PrjID enumeration 0x{0:2X} to string PrjID", ItemID));
                PrjID = ProcessedBZN.BZn64IdMap[ItemID];
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

            switch (ProcessedBZN.ClassLabelMap[PrjID])
            {
                case "scrapsilo":
                    gameObject = new BZNScrapSilo();
                    break;
                case "wingman":
                case "turret":
                case "minelayer":
                case "sav":
                    gameObject = new BZNHoverCraft();
                    break;
                case "wpnpower":
                    gameObject = new BZNWeaponPowerup();
                    break;
                case "armory":
                    gameObject = new BZNArmory();
                    break;
                case "recycler":
                    gameObject = new BZNRecycler();
                    break;
                case "factory":
                    gameObject = new BZNFactory();
                    break;
                case "constructionrig":
                    gameObject = new BZNConstructionRig();
                    break;
                case "person":
                    gameObject = new BZNPerson();
                    break;
                case "apc":
                    gameObject = new BZNAPC();
                    break;
                case "scavenger":
                    gameObject = new BZNScavenger();
                    break;
                case "turrettank":
                    gameObject = new BZNTurretTank();
                    break;
                case "howitzer":
                    gameObject = new BZNHowitzer();
                    break;
                case "tug":
                    gameObject = new BZNTug();
                    break;
                case "walker":
                    gameObject = new BZNCraft();
                    break;
                default:
                    gameObject = new BZNGameObject();
                    break;
            }
            gameObject.LoadData(reader);
        }
    }

    public class BZNGameObject
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

        public BZNGameObject(){}
        public virtual void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                illumination = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);

                float posx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float posy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                float poxz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                idx++;
                pos = new Vector3D() { x = posx, y = posy, z = posy };

                float euler_mass = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_mass_inv = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_v_mag = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_v_mag_inv = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_I = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_k_i = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_vx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_vy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                float euler_vz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                idx++;
                float euler_omegax = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_omegay = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                float euler_omegaz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                idx++;
                float euler_Accelx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                float euler_Accely = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                float euler_Accelz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                idx++;
                euler = new Euler()
                {
                    mass = euler_mass,
                    mass_inv = euler_mass_inv,
                    v_mag = euler_v_mag,
                    v_mag_inv = euler_v_mag_inv,
                    I = euler_I,
                    k_i = euler_k_i,
                    v = new Vector3D() { x = euler_vx, y = euler_vy, z = euler_vz },
                    omega = new Vector3D() { x = euler_omegax, y = euler_omegay, z = euler_omegaz },
                    Accel = new Vector3D() { x = euler_Accelx, y = euler_Accely, z = euler_Accelz }
                };

                seqNo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                name = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                name = name.IndexOf('\0') > -1 ? name.Substring(0, name.IndexOf('\0')) : name;
                isObjective = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                isSelected = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                isVisible = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                seen = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                healthRatio = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                curHealth = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                maxHealth = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                ammoRatio = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Reverse().Take(4).ToArray(), 0);
                curAmmo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                maxAmmo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                priority = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                what = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                who = (Int32)BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                where = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                param = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                aiProcess = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                isCargo = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                independence = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);

                UInt16 curPilotID = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                curPilot = ProcessedBZN.BZn64IdMap[curPilotID];

                perceivedTeam = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                illumination = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);

                float posx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                float posy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                float poxz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                idx++;
                pos = new Vector3D() { x = posx, y = posy, z = posy };

                float euler_mass = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                float euler_mass_inv = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                float euler_v_mag = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                float euler_v_mag_inv = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                float euler_I = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                float euler_k_i = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                float euler_vx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                float euler_vy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                float euler_vz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                idx++;
                float euler_omegax = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                float euler_omegay = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                float euler_omegaz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                idx++;
                float euler_Accelx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                float euler_Accely = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                float euler_Accelz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                idx++;
                euler = new Euler()
                {
                    mass = euler_mass,
                    mass_inv = euler_mass_inv,
                    v_mag = euler_v_mag,
                    v_mag_inv = euler_v_mag_inv,
                    I = euler_I,
                    k_i = euler_k_i,
                    v = new Vector3D() { x = euler_vx, y = euler_vy, z = euler_vz },
                    omega = new Vector3D() { x = euler_omegax, y = euler_omegay, z = euler_omegaz },
                    Accel = new Vector3D() { x = euler_Accelx, y = euler_Accely, z = euler_Accelz }
                };

                seqNo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);

                if (version > 1030)
                {
                    name = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                    name = name.IndexOf('\0') > -1 ? name.Substring(0, name.IndexOf('\0')) : name;
                }

                isObjective = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                isSelected = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                isVisible = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);

                seen = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);

                //[10:03:38 PM] Kenneth Miller: I think I may have figured out what that stuff is, maybe
                //[10:03:50 PM] Kenneth Miller: They're timestamps
                //[10:04:04 PM] Kenneth Miller: playerShot, playerCollide, friendShot, friendCollide, enemyShot, groundCollide
                //[10:04:13 PM] Kenneth Miller: the default value is -HUGE_NUMBER (-1e30)
                //[10:04:26 PM] Kenneth Miller: And due to the nature of the game, groundCollide is the most likely to get set first
                //[10:05:02 PM] Kenneth Miller: Old versions of the mission format used to contain those values but later versions only include them in the savegame
                //[10:05:05 PM] Kenneth Miller: (not the mission)
                //[10:05:31 PM] Kenneth Miller: (version 1033 was where they were removed from the mission)
                if(version < 1033)
                {
                    idx++; // float (-HUGE_NUMBER)
                    idx++; // float (-HUGE_NUMBER)
                    idx++; // float (-HUGE_NUMBER)
                    idx++; // float (-HUGE_NUMBER)
                    idx++; // float (-HUGE_NUMBER)
                    idx++; // float
                }
                
                healthRatio = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                curHealth = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                maxHealth = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                ammoRatio = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                curAmmo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                maxAmmo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                priority = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                what = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                who = (Int32)BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                where = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                param = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                aiProcess = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                isCargo = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                independence = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                curPilot = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                curPilot = curPilot.IndexOf('\0') > -1 ? curPilot.Substring(0, curPilot.IndexOf('\0')) : curPilot;
                if (version > 1030)
                {
                    perceivedTeam = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                }
                else
                {
                    perceivedTeam = -1;
                }
            }
        }

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

            if(reader.N64)
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

            if(reader.N64)
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
                if (!ProcessedBZN.BZn64IdMap.ContainsKey(curPilotID)) throw new InvalidCastException(string.Format("Cannot convert n64 curPilotID enumeration 0x{0:2X} to string curPilotID", curPilotID));
                curPilot = ProcessedBZN.BZn64IdMap[curPilotID];
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

    public class BZNScrapSilo : BZNGameObject
    {
        public UInt32 undefptr { get; set; }

        public BZNScrapSilo() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }

        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse undefptr/LONG");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }
    }

    public class BZNCraft : BZNGameObject
    {
        public UInt32 abandoned { get; set; }

        public BZNCraft() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                abandoned = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                abandoned = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("abandoned", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse abandoned/LONG");
            abandoned = tok.GetUInt32();

            base.LoadData(reader);
        }
    }

    public class BZNHoverCraft : BZNCraft
    {
        public BZNHoverCraft() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            base.LoadData(reader);
        }
    }

    public class BZNTug : BZNHoverCraft
    {
        public UInt32 undefptr { get; set; }

        public BZNTug() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse undefptr/LONG");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }
    }

    public class BZNTurretTank : BZNHoverCraft
    {
        public float undeffloat1 { get; set; }
        public float undeffloat2 { get; set; }
        public float undeffloat3 { get; set; }
        public float undeffloat4 { get; set; }
        public UInt32 undefraw { get; set; }
        public float undeffloat5 { get; set; }
        public bool undefbool { get; set; }

        public BZNTurretTank() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                undeffloat1 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                undeffloat2 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                undeffloat3 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                undeffloat4 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                undefraw = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                undeffloat5 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                undefbool = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                undeffloat1 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                undeffloat2 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                undeffloat3 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                undeffloat4 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                undefraw = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                undeffloat5 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                undefbool = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;
            
            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat1 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat2 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat3 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat4 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undefraw", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse undefraw/VOID");
            undefraw = tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("undeffloat", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse undeffloat/FLOAT");
            undeffloat5 = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undefbool", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse undefbool/BOOL");
            undefbool = tok.GetBoolean();

            base.LoadData(reader);
        }
    }

    public class BZNHowitzer : BZNTurretTank
    {
        public BZNHowitzer() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            base.LoadData(reader);
        }
    }

    public class BZNScavenger : BZNHoverCraft
    {
        public UInt32 scrapHeld { get; set; }

        public BZNScavenger() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {

            }
            else if (version > 1035)
            {
                scrapHeld = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            throw new NotImplementedException();
            //base.LoadData(reader);
        }
    }


    public class BZNPerson : BZNCraft
    {
        public UInt32 nextScream { get; set; }

        public BZNPerson() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                nextScream = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                nextScream = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            throw new NotImplementedException();
            //base.LoadData(reader);
        }
    }

    public class BZNAPC : BZNHoverCraft
    {
        public UInt32 soldierCount { get; set; }
        public UInt32 state { get; set; }

        public BZNAPC() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                soldierCount = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                state = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                soldierCount = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                state = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            throw new NotImplementedException();
            //base.LoadData(reader);
        }
    }

    public class BZNProducer : BZNHoverCraft
    {
        public float timeDeploy { get; set; }
        public float timeUndeploy { get; set; }
        public UInt32 undefptr2 { get; set; }
        public UInt32 state { get; set; }
        //public UInt32 delayTimer { get; set; }
        public float delayTimer { get; set; }
        public float nextRepair { get; set; }
        public string buildClass { get; set; }
        //public UInt32 buildDoneTime { get; set; }
        public float buildDoneTime { get; set; }

        public BZNProducer() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                timeDeploy = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                timeUndeploy = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                undefptr2 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                state = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                delayTimer = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                nextRepair = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);

                UInt16 buildClassID = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                buildClass = ProcessedBZN.BZn64IdMap[buildClassID];

                buildDoneTime = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                timeDeploy = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                timeUndeploy = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                undefptr2 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                state = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                delayTimer = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                nextRepair = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                buildClass = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                buildClass = buildClass.IndexOf('\0') > -1 ? buildClass.Substring(0, buildClass.IndexOf('\0')) : buildClass;
                buildDoneTime = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("timeDeploy", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse timeDeploy/FLOAT");
            timeDeploy = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("timeUndeploy", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse timeUndeploy/FLOAT");
            timeUndeploy = tok.GetSingle();

            tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            undefptr2 = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("state", BinaryFieldType.DATA_VOID)) throw new Exception("Failed to parse state/VOID");
            state = tok.GetUInt32H();

            tok = reader.ReadToken();
            if (!tok.Validate("delayTimer", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse delayTimer/FLOAT");
            delayTimer = tok.GetSingle();//tok.GetUInt32();

            tok = reader.ReadToken();
            if (!tok.Validate("nextRepair", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse nextRepair/FLOAT");
            nextRepair = tok.GetSingle();

            if (reader.N64)
            {
                tok = reader.ReadToken();
                UInt16 buildClassItemID = tok.GetUInt16();
                if (!ProcessedBZN.BZn64IdMap.ContainsKey(buildClassItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 buildClass enumeration 0x{0:2X} to string buildClass", buildClassItemID));
                buildClass = ProcessedBZN.BZn64IdMap[buildClassItemID];
            }
            else
            {
                tok = reader.ReadToken();
                if (!tok.Validate("buildClass", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse buildClass/ID");
                buildClass = tok.GetString();
            }

            tok = reader.ReadToken();
            if (!tok.Validate("buildDoneTime", BinaryFieldType.DATA_FLOAT)) throw new Exception("Failed to parse buildDoneTime/FLOAT");
            buildDoneTime = tok.GetSingle();
            
            base.LoadData(reader);
        }
    }

    public class BZNRecycler : BZNProducer
    {
        public UInt32 undefptr { get; set; }

        public BZNRecycler() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
            }
            else
            {
                undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok = reader.ReadToken();
            if (!tok.Validate("undefptr", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse undefptr/PTR");
            undefptr = tok.GetUInt32H();

            base.LoadData(reader);
        }
    }

    public class BZNFactory : BZNProducer
    {
        public BZNFactory() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            base.LoadData(reader);
        }
    }

    public class BZNArmory : BZNProducer
    {
        public BZNArmory() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            base.LoadData(reader);
        }
    }

    public class BZNConstructionRig : BZNProducer
    {
        public Matrix dropMat { get; set; }
        public string dropClass { get; set; }

        public BZNConstructionRig() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            if(bzn64)
            {
                dropMat = new Matrix();// bzn.fields[idx++].GetRawRef().Reverse().ToArray();
                UInt16 dropClassID = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                dropClass = ProcessedBZN.BZn64IdMap[dropClassID];
            }
            else
            {
                if (version > 1030)
                {
                    dropMat = new Matrix();//bzn.fields[idx++].GetRawRef().ToArray();
                    dropClass = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                    dropClass = dropClass.IndexOf('\0') > -1 ? dropClass.Substring(0, dropClass.IndexOf('\0')) : dropClass;
                }
            }

            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            if (reader.N64 || reader.Version > 1030)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("dropMat", BinaryFieldType.DATA_MAT3DOLD)) throw new Exception("Failed to parse dropMat/MAT3DOLD");
                dropMat = tok.GetMatrix();
            }

            if (reader.N64)
            {
                tok = reader.ReadToken();
                UInt16 dropClassItemID = tok.GetUInt16();
                if (!ProcessedBZN.BZn64IdMap.ContainsKey(dropClassItemID)) throw new InvalidCastException(string.Format("Cannot convert n64 dropClass enumeration 0x{0:2X} to string dropClass", dropClassItemID));
                dropClass = ProcessedBZN.BZn64IdMap[dropClassItemID];
            }
            else if(reader.Version > 1030)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("dropClass", BinaryFieldType.DATA_ID)) throw new Exception("Failed to parse dropClass/ID");
                dropClass = tok.GetString();
            }

            base.LoadData(reader);
        }
    }

    public class BZNPowerUp : BZNGameObject
    {
        public BZNPowerUp() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            base.LoadData(reader);
        }
    }

    public class BZNWeaponPowerup : BZNPowerUp
    {
        public BZNWeaponPowerup() { }
        public override void LoadData(IRawBZN bzn, ref int idx, int version, bool bzn64 = false)
        {
            base.LoadData(bzn, ref idx, version, bzn64);
        }
        public override void LoadData(BZNReader reader)
        {
            base.LoadData(reader);
        }
    }
    
}
