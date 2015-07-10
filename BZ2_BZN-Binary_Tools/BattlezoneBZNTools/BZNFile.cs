using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools
{
    public class BZNFile
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

        public int Version { get; set; }
        public bool N64 { get; set; }
        public bool BinaryMode { get; set; }

        public string msn_filename;

        public int seq_count;
        public bool missionSave;
        public string TerrainName;

        public BZNGameObjectWrapper[] GameObjects;
        public string MissionClass;
        public UInt32 sObject;
        public BZNAOI[] AOIs;
        public BZNAiPath[] AiPaths;

        private BZNFile(Stream stream, bool inBinary = false, bool n64Data = false)
        {
            BZNReader reader = new BZNReader(stream, inBinary, n64Data);

            //Console.WriteLine("Version: {0}", reader.Version);
            //Console.WriteLine("Platform: {0}", reader.N64 ? "N64" : "PC");
            //Console.WriteLine("Mode: {0}", reader.BinaryMode ? "BIN" : "ASCII");
            //Console.WriteLine();

            IBZNToken tok;

            tok = reader.ReadToken();
            if (!tok.Validate("msn_filename", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse msn_filename");
            msn_filename = tok.GetString();

            //Console.WriteLine("msn_filename: \"{0}\"", msn_filename);

            if (!reader.N64)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("seq_count", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse seq_count/LONG");
                seq_count = tok.GetInt32();

                tok = reader.ReadToken();
                if (!tok.Validate("missionSave", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse missionSave/BOOL");
                missionSave = tok.GetBoolean();

                tok = reader.ReadToken();
                if (!tok.Validate("TerrainName", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse TerrainName/CHAR");
                TerrainName = tok.GetString();

                //Console.WriteLine("seq_count: {0}", seq_count);
                //Console.WriteLine("missionSave: {0}", missionSave);
                //Console.WriteLine("TerrainName: {0}", TerrainName);
                //Console.WriteLine();
            }

            tok = reader.ReadToken();
            if (!tok.Validate("size", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse count/LONG");
            Int32 CountItems = tok.GetInt32();

            //Console.WriteLine("Game Objects\tCount: {0}", CountItems);

            GameObjects = new BZNGameObjectWrapper[CountItems];

            for (int gameObjectCounter = 0; gameObjectCounter < CountItems; gameObjectCounter++)
            {
                GameObjects[gameObjectCounter] = new BZNGameObjectWrapper(reader);
            }

            //foreach(BZNGameObjectWrapper obj in GameObjects)
            //{
            //    string type = obj.gameObject.GetType().Name.Substring(3);
            //    if (type == "GameObject") type += "?";
            //
            //    Console.WriteLine(
            //        "{0}  {1}  {2}  [{3,6:F2}, {4,6:F2}, {5,9:F2}]",
            //        obj.seqno.ToString().PadLeft(4),
            //        obj.PrjID.PadRight(12),
            //        type.PadRight(16),
            //        obj.pos.x,
            //        obj.pos.y,
            //        obj.pos.z
            //    );
            //}

            //Console.WriteLine();

            tok = reader.ReadToken();
            if (!tok.Validate("name", BinaryFieldType.DATA_CHAR)) throw new Exception("Failed to parse name/CHAR");
            MissionClass = tok.GetString();

            //Console.WriteLine("MissionClass: \"{0}\"", MissionClass);

            tok = reader.ReadToken();
            if (!tok.Validate("sObject", BinaryFieldType.DATA_PTR)) throw new Exception("Failed to parse sObject/PTR");
            sObject = tok.GetUInt32H();

            //Console.WriteLine("sObject: {0:X8}", sObject);

            //Console.WriteLine();

            if (!reader.BinaryMode)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AiMission")) throw new Exception("Failed to parse [AiMission]");
            }
            /////////////////////////////////////
            if (!reader.BinaryMode)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AOIs")) throw new Exception("Failed to parse [AOIs]");
            }

            tok = reader.ReadToken();
            if (!tok.Validate("size", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse size/LONG");
            Int32 CountAOIs = tok.GetInt32();

            //Console.WriteLine("AOI\tCount: {0}", CountAOIs);

            AOIs = new BZNAOI[CountAOIs];

            for (int aoiCounter = 0; aoiCounter < CountAOIs; aoiCounter++)
            {
                AOIs[aoiCounter] = new BZNAOI(reader);

                //Console.WriteLine("{0:X8}\tTeam: {1}\tInteresting: {2}\tInside: {3}\tValue: {4}\tForce: {5}", undefptr, team.ToString().PadLeft(2), interesting, inside, value, force);
            }

            //Console.WriteLine();

            /////////////////////////////////////
            if (!reader.BinaryMode)
            {
                tok = reader.ReadToken();
                if (!tok.IsValidationOnly() || !tok.Validate("AiPaths", BinaryFieldType.DATA_UNKNOWN)) throw new Exception("Failed to parse [AiPaths]");
            }

            tok = reader.ReadToken();
            if (!tok.Validate("count", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse count/LONG");
            Int32 CountAiPaths = tok.GetInt32();

            //Console.WriteLine("AiPath\tCount: {0}", CountAiPaths);

            AiPaths = new BZNAiPath[CountAiPaths];

            for (int AiPathCounter = 0; AiPathCounter < CountAiPaths; AiPathCounter++)
            {
                AiPaths[AiPathCounter] = new BZNAiPath(reader);

                //Console.WriteLine("OldPtr: {0:X8}\tPathType: {1}\tLabel: {2}", old_ptr, pathType, label);
                //for (int pointCounter = 0; pointCounter < pointCount; pointCounter++)
                //{
                //    Console.WriteLine("\t[{0,2}] {1,8:F2}, {2,8:F2}", pointCounter, points[pointCounter].x, points[pointCounter].z);
                //}
            }

            //if ((tok = reader.ReadToken()) != null)
            //{
            //    Console.WriteLine("UNKNOWN DATA");
            //    do
            //    {
            //        if (tok.IsValidationOnly())
            //        {
            //            Console.WriteLine(tok.ToString());
            //        }
            //        else
            //        {
            //            Console.Write(tok.ToString());
            //            try
            //            {
            //                Console.Write("\t" + tok.GetString());
            //            }
            //            catch { }
            //            Console.WriteLine();
            //        }
            //        //Console.ReadKey(true);
            //    } while ((tok = reader.ReadToken()) != null);
            //}
        }

        public static BZNFile OpenBZ1(Stream stream)
        {
            return new BZNFile(stream);
        }

        public static BZNFile OpenBZn64(Stream stream)
        {
            return new BZNFile(stream, true, true);
        }
    }
}
