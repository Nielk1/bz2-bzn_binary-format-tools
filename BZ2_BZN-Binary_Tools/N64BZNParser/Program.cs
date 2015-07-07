using BinaryBZNFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace N64BZNParser
{
    class Program
    {
        static void Main(string[] args)
        {
            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\play01.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Training\play01.bzn Training\A5B3F0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn01.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Training\play01.bzn Training\A5B3F0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\tran02.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Training\tran02.bzn Training\A5A44C.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\tran03.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Training\tran03.bzn Training\A5AA38.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\tran04.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Training\tran04.bzn Training\A5B034.bin";

            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Training\tran06.bzn Training (NEW)\A5D2E8.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn02b.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn02b.bzn US\A304C0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn03.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn03.bzn US\A30FB0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn04.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn04.bzn US\A339C0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn05.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn05.bzn US\A355E8.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn06.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn06.bzn US\A36280.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn07.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn07.bzn US\A3820C.bin";

            //string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn08.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn08.bzn US\A3C8B8.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn09.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn09.bzn US\A3F000.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn10.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn10.bzn US\A405BC.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn11.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn11.bzn US\A42600.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn12.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn12.bzn US\A437C0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn13.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn13.bzn US\A46418.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn14.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn14.bzn US\A4A5F8.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn15b.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn15b.bzn US\A4B794.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn16b.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn16b.bzn US\A4C14C.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn17.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn17.bzn US\A4CE10.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn18.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn18.bzn US\A4D9A0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns1.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns1.bzn Soviet\A4E480.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns2.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns2.bzn Soviet\A4FAD4.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns3.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns3.bzn Soviet\A510D4.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns4.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns4.bzn Soviet\A52BF8.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns5.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns5.bzn Soviet\A53778.bin";

            //string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns6.bzn";
            //string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns6.bzn Soviet\A540B0.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns7.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns7.bzn Soviet\A54D8C.bin";

            ////string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misns8.bzn";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns8.bzn Soviet\A56DD4.bin";

            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn03.bzn Black Dog (NSDF3)\A6DD20.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn04.bzn Black Dog (NSDF4)\A60254.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn20.bzn Black Dog (NEW)\A5D4A8.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn21.bzn Black Dog (NEW)\A5F198.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn23.bzn Black Dog (NEW)\A61D84.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn24.bzn Black Dog (NSDF7)\A64B48.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn25.bzn Black Dog (NSDF7)\A65C70.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn26.bzn Black Dog (NSDF11)\A66960.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn27.bzn Black Dog (NEW)\A68CC0.bin";
            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Black Dogs)\misn28.bzn Black Dog (NSDF13)\A69BC0.bin";

            ////string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Unused\misn00.bzn UNUSED\AA7518.bin";

            ////string[] FileList = Directory.GetFiles(@"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Arcade (Black Dog)", "*.bin", SearchOption.AllDirectories);
            ////string[] FileList = Directory.GetFiles(@"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Arcade (Soviet)", "*.bin", SearchOption.AllDirectories);
            ////string[] FileList = Directory.GetFiles(@"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Arcade (US)", "*.bin", SearchOption.AllDirectories);
            string[] FileList = Directory.GetFiles(@"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs", "*.bin", SearchOption.AllDirectories);
            foreach (string filename in FileList)
            {
                IRawBZN bzn = null;
                using (FileStream file = File.OpenRead(filename))
                {
                    if (Path.GetExtension(filename) == ".bin")
                    {
                        bzn = new N64BZN(file);
                    }
                    else
                    {
                        bzn = new RawBZN(file, true);
                    }
                }
                if (bzn != null)
                {
                    Console.WriteLine("Folder:\t{0}", Path.GetDirectoryName(filename).Split('\\').Last());
                    Console.WriteLine("File:\t{0}", Path.GetFileName(filename));

                    Console.WriteLine("object\tclass\tx\ty\tz");

                    ProcessedBZN proc = new ProcessedBZN(bzn);

                    proc.GameObjects.ForEach(dr =>
                    {
                        //Console.WriteLine("            \"{0}\",", dr.PrjID);
                        Console.WriteLine(
                            "{0}\t{1}\t{2}\t{3}\t{4}",
                            dr.PrjID,
                            dr.gameObject.GetType().ToString().Split('.').Last(),
                            dr.pos.x.ToString("0.0"),
                            dr.pos.y,
                            dr.pos.z
                        );
                    });

                    Console.WriteLine();
                }

                //Console.ReadKey(true);
            }
        }

        private static void ReadBZ1File(string filename)
        {
            Dictionary<string, string> ClassLabelMap = new Dictionary<string, string>();
            ClassLabelMap["abbarr"] = "barracks";
            ClassLabelMap["abcafe"] = "i76building";
            ClassLabelMap["abcomm"] = "commtower";
            ClassLabelMap["abhang"] = "repairdepot";
            ClassLabelMap["abhqcp"] = "i76building";
            ClassLabelMap["ablpad"] = "i76building";
            ClassLabelMap["ablpow"] = "powerplant";
            ClassLabelMap["abmbld"] = "supplydepot";
            ClassLabelMap["abshld"] = "i76building";
            ClassLabelMap["absilo"] = "scrapsilo";
            ClassLabelMap["abspow"] = "powerplant";
            ClassLabelMap["abstor"] = "i76building";
            ClassLabelMap["absupp"] = "supplydepot";
            ClassLabelMap["abtowe"] = "turret";
            ClassLabelMap["abwpow"] = "powerplant";
            ClassLabelMap["apammo"] = "ammopack";
            ClassLabelMap["apcamr"] = "camerapod";
            ClassLabelMap["aprepa"] = "repairkit";
            ClassLabelMap["apstab"] = "wpnpower";
            ClassLabelMap["apwrck"] = "daywrecker";
            ClassLabelMap["aspilo"] = "person";
            ClassLabelMap["avapc" ] = "apc";
            ClassLabelMap["avartl"] = "howitzer";
            ClassLabelMap["avcnst"] = "constructionrig";
            ClassLabelMap["avhaul"] = "tug";
            ClassLabelMap["avmine"] = "minelayer";
            ClassLabelMap["avmuf" ] = "factory";
            ClassLabelMap["avrecy"] = "recycler";
            ClassLabelMap["avsav" ] = "sav";
            ClassLabelMap["avscav"] = "scavenger";
            ClassLabelMap["avslf" ] = "armory";
            ClassLabelMap["avtank"] = "wingman";
            ClassLabelMap["avtest"] = "turrettank";
            ClassLabelMap["avwalk"] = "walker";

            ClassLabelMap["sbbarr"] = "barracks";
            ClassLabelMap["sbcafe"] = "i76building";
            ClassLabelMap["sbcomm"] = "commtower";
            ClassLabelMap["sbhang"] = "repairdepot";
            ClassLabelMap["sbhqcp"] = "i76building";
            ClassLabelMap["sblpad"] = "i76building";
            ClassLabelMap["sblpow"] = "powerplant";
            ClassLabelMap["sbshld"] = "i76building";
            ClassLabelMap["sbsilo"] = "scrapsilo";
            ClassLabelMap["sbspow"] = "powerplant";
            ClassLabelMap["sbsupp"] = "supplydepot";
            ClassLabelMap["sbtowe"] = "turret";
            ClassLabelMap["sbwpow"] = "powerplant";
            ClassLabelMap["spammo"] = "ammopack";
            ClassLabelMap["spcamr"] = "camerapod";
            ClassLabelMap["sprepa"] = "repairkit";
            ClassLabelMap["spstab"] = "wpnpower";
            ClassLabelMap["spwrck"] = "daywrecker";
            ClassLabelMap["sspilo"] = "person";
            ClassLabelMap["svapc"] = "apc";
            ClassLabelMap["svartl"] = "howitzer";
            ClassLabelMap["svcnst"] = "constructionrig";
            ClassLabelMap["svhaul"] = "tug";
            ClassLabelMap["svmine"] = "minelayer";
            ClassLabelMap["svmuf"] = "factory";
            ClassLabelMap["svrecy"] = "recycler";
            ClassLabelMap["svsav"] = "sav";
            ClassLabelMap["svscav"] = "scavenger";
            ClassLabelMap["svslf"] = "armory";
            ClassLabelMap["svtank"] = "wingman";
            ClassLabelMap["svwalk"] = "walker";

            ClassLabelMap["cbport"] = "portal";

            ClassLabelMap["boltmine"] = "weaponmine";
            ClassLabelMap["eggeizr1"] = "geyser";
            ClassLabelMap["flare"] = "flare";
            ClassLabelMap["magpull"] = "magnet";
            ClassLabelMap["nparr"] = "i76sign";

            ClassLabelMap["npscr1"] = "scrap";
            ClassLabelMap["npscr2"] = "scrap";
            ClassLabelMap["npscr3"] = "scrap";
            ClassLabelMap["sscr_1"] = "scrap";

            ClassLabelMap["obdata"] = "artifact";
            ClassLabelMap["player"] = "wingman";
            ClassLabelMap["proxmine"] = "proximity";
            ClassLabelMap["pspwn_1"] = "spawnpnt";
            ClassLabelMap["sfield"] = "scrapfield";
            ClassLabelMap["splintbm"] = "spraybomb";
            ClassLabelMap["waspmsl"] = "torpedo";

            //Version 1045 - latest

            RawBZN bzn = null;
            using (FileStream file = File.OpenRead(filename))
            {
                bzn = new RawBZN(file, true);
            }
            if (bzn != null)
            {
                int version = int.Parse(bzn.asciiFields.Where(dr => dr.name == "version").First().value[0]);

                bzn.asciiFields.ForEach(dr =>
                {
                    if (dr.isArray)
                    {
                        Console.WriteLine("{0} [{1}] =", dr.name, dr.value.Length);
                        for(int itemNum = 0; itemNum < dr.value.Length; itemNum++)
                        {
                            Console.WriteLine(dr.value[itemNum]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0} = {1}", dr.name, dr.value[0]);
                    }
                });

                int idx = 0;
                //for (int idx = 0; idx < bzn.fields.Count; idx++)
                {
                    //Field tmp = bzn.fields[idx];
                    string msn_filename = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                    Int32 seq_count = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                    bool missionSave = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                    string TerrainName = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());

                    Console.WriteLine("0\tmsn_filename: {0}", msn_filename);
                    Console.WriteLine("1\tseq_count: {0}", seq_count);
                    Console.WriteLine("2\tmissionSave: {0}", missionSave);
                    Console.WriteLine("3\tTerrainName: {0}", TerrainName);

                    Int32 CountItems = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                    Console.WriteLine("4\tsize: {0}", CountItems);
                    Console.WriteLine("------------------------------");
                    for (int i = 0; i < CountItems; i++)
                    {
                        Console.WriteLine("[GameObject]");

                        string PrjID = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                        Console.WriteLine("{0}\t  PrjID: {1}", idx - 1, PrjID);

                        UInt16 seqno = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  seqno: {1}", idx - 1, seqno);

                        float posx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                        float posy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                        float posz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  pos: {1}f, {2}f, {3}f", idx - 1, posx.ToString("0.0"), posy.ToString("0.0"), posz.ToString("0.0"));

                        UInt32 team = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  team: {1}", idx - 1, team);

                        string label = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                        Console.WriteLine("{0}\t  label: {1}", idx - 1, label);

                        UInt32 isUser = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  isUser: {1}", idx - 1, isUser);

                        UInt32 obj_addr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  obj_addr: 0x{1,8:X8}", idx - 1, obj_addr);

                        byte[] transform = bzn.fields[idx++].GetRawRef().ToArray();
                        Console.Write("{0}\t  transform (raw for now): ", idx - 1);
                        for (int j = 0; j < transform.Length; j++)
                        {
                            Console.Write("{0,2:X2}", transform[j]);
                        }
                        Console.WriteLine();

                        if (ClassLabelMap[PrjID.TrimEnd('\0')] == "scrapsilo")
                        {
                            UInt32 undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                            Console.WriteLine("{0}\t  undefptr: 0x{1,8:X8}", idx - 1, undefptr);
                        }

                        if ( ClassLabelMap[PrjID.TrimEnd('\0')] == "wingman"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "recycler"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "factory"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "armory"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "constructionrig"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "turret"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "person"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "wpnpower"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "apc"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "howitzer"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "minelayer"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "sav"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "scavenger"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "tug"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "turrettank"
                          || ClassLabelMap[PrjID.TrimEnd('\0')] == "walker")
                        {
                            if (ClassLabelMap[PrjID.TrimEnd('\0')] == "tug")
                            {
                                UInt32 undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  undefptr: 0x{1,8:X8}", idx - 1, undefptr);
                            }

                            if (ClassLabelMap[PrjID.TrimEnd('\0')] == "scavenger")
                            {
                                if (version > 1035)
                                {
                                    UInt32 scrapHeld = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                    Console.WriteLine("{0}\t  scrapHeld: {1}", idx - 1, scrapHeld);
                                }
                            }

                            if ( ClassLabelMap[PrjID.TrimEnd('\0')] == "turrettank"
                              || ClassLabelMap[PrjID.TrimEnd('\0')] == "howitzer")
                            {
                                float undeffloat1 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                                Console.WriteLine("{0}\t  undeffloat: {1}f", idx - 1, undeffloat1.ToString("0.0"));

                                float undeffloat2 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                                Console.WriteLine("{0}\t  undeffloat: {1}f", idx - 1, undeffloat2.ToString("0.0"));

                                float undeffloat3 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                                Console.WriteLine("{0}\t  undeffloat: {1}f", idx - 1, undeffloat3.ToString("0.0"));

                                float undeffloat4 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                                Console.WriteLine("{0}\t  undeffloat: {1}f", idx - 1, undeffloat4.ToString("0.0"));

                                UInt32 undefraw = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  undefraw: 0x{1,8:X8}", idx - 1, undefraw);

                                float undeffloat5 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                                Console.WriteLine("{0}\t  undeffloat: {1}f", idx - 1, undeffloat5.ToString("0.0"));

                                bool undefbool = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  undefbool: {1}", idx - 1, undefbool);
                            }

                            if ( ClassLabelMap[PrjID.TrimEnd('\0')] == "factory"
                              || ClassLabelMap[PrjID.TrimEnd('\0')] == "armory"
                              || ClassLabelMap[PrjID.TrimEnd('\0')] == "constructionrig"
                              || ClassLabelMap[PrjID.TrimEnd('\0')] == "recycler")
                            {
                                if (ClassLabelMap[PrjID.TrimEnd('\0')] == "recycler")
                                {
                                    UInt32 undefptr = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                    Console.WriteLine("{0}\t  undefptr: 0x{1,8:X8}", idx - 1, undefptr);
                                }

                                if (ClassLabelMap[PrjID.TrimEnd('\0')] == "constructionrig")
                                {
                                    byte[] dropMat = bzn.fields[idx++].GetRawRef().ToArray();
                                    Console.Write("{0}\t  dropMat (raw for now): ", idx - 1);
                                    for (int j = 0; j < dropMat.Length; j++)
                                    {
                                        Console.Write("{0,2:X2}", dropMat[j]);
                                    }
                                    Console.WriteLine();

                                    string dropClass = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                                    Console.WriteLine("{0}\t  dropClass: {1}", idx - 1, dropClass);
                                }

                                UInt32 timeDeploy = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  timeDeploy: {1}", idx - 1, timeDeploy);

                                UInt32 timeUndeploy = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  timeUndeploy: {1}", idx - 1, timeUndeploy);

                                UInt32 undefptr2 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  undefptr: 0x{1,8:X8}", idx - 1, undefptr2);

                                UInt32 state = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  state: 0x{1,8:X8}", idx - 1, state);

                                UInt32 delayTimer = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  delayTimer: {1}", idx - 1, delayTimer);

                                float nextRepair = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                                Console.WriteLine("{0}\t  nextRepair: {1}f", idx - 1, nextRepair.ToString("0.0"));

                                string buildClass = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                                Console.WriteLine("{0}\t  buildClass: {1}", idx - 1, buildClass);

                                UInt32 buildDoneTime = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  buildDoneTime: {1}", idx - 1, buildDoneTime);
                            }

                            if (ClassLabelMap[PrjID.TrimEnd('\0')] == "person")
                            {
                                UInt32 nextScream = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  nextScream: {1}", idx - 1, nextScream);
                            }

                            if (ClassLabelMap[PrjID.TrimEnd('\0')] == "apc")
                            {
                                UInt32 soldierCount = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  soldierCount: {1}", idx - 1, soldierCount);

                                UInt32 state = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                                Console.WriteLine("{0}\t  state: 0x{1,8:X8}", idx - 1, state);
                            }

                            UInt32 abandoned = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                            Console.WriteLine("{0}\t  abandoned: {1}", idx - 1, abandoned);
                        }

                        float illumination = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  illumination: {1}f", idx - 1, illumination.ToString("0.0"));

                        float pos2x = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                        float pos2y = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                        float pox2z = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  pos: {1}f, {2}f, {3}f", idx - 1, pos2x.ToString("0.0"), pos2y.ToString("0.0"), pox2z.ToString("0.0"));

                        float euler_mass = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  euler_mass: {1}f", idx - 1, euler_mass.ToString("0.0"));

                        float euler_mass_inv = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  euler_mass_inv: {1}f", idx - 1, euler_mass_inv.ToString("0.0"));

                        float euler_v_mag = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  euler_v_mag: {1}f", idx - 1, euler_v_mag.ToString("0.0"));

                        float euler_v_mag_inv = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  euler_v_mag_inv: {1}f", idx - 1, euler_v_mag_inv.ToString("0.0"));

                        float euler_I = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  euler_I: {1}f", idx - 1, euler_I.ToString("0.0"));

                        float euler_k_i = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  euler_k_i: {1}f", idx - 1, euler_k_i.ToString("0.0"));

                        float euler_vx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                        float euler_vy = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                        float euler_vz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  euler_v: {1}f, {2}f, {3}f", idx - 1, euler_vx.ToString("0.0"), euler_vy.ToString("0.0"), euler_vz.ToString("0.0"));

                        float euler_omegax = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                        float euler_omegay = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                        float euler_omegaz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  euler_omega: {1}f, {2}f, {3}f", idx - 1, euler_omegax.ToString("0.0"), euler_omegay.ToString("0.0"), euler_omegaz.ToString("0.0"));

                        float euler_Accelx = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).ToArray(), 0);
                        float euler_Accely = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).ToArray(), 0);
                        float euler_Accelz = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  euler_Accel: {1}f, {2}f, {3}f", idx - 1, euler_Accelx.ToString("0.0"), euler_Accely.ToString("0.0"), euler_Accelz.ToString("0.0"));

                        UInt32 seqNo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  seqNo: 0x{1,8:X8}", idx - 1, seqNo);

                        //byte[] UnknownA18 = bzn.fields[idx++].GetRawRef().ToArray();
                        //Console.Write("{0}\t  UnknownA18: ", idx - 1);
                        //for (int j = 0; j < UnknownA18.Length; j++)
                        //{
                        //    Console.Write("{0,2:X2}", UnknownA18[j]);
                        //}
                        //Console.WriteLine();
                        string name = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                        Console.WriteLine("{0}\t  name: {1}", idx - 1, name);

                        bool isObjective = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  isObjective: {1}", idx - 1, isObjective);

                        bool isSelected = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  isSelected: {1}", idx - 1, isSelected);

                        UInt32 isVisible = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  isVisible: 0x{1,8:X8}", idx - 1, isVisible);

                        UInt32 seen = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  seen: {1}", idx - 1, seen);

                        float healthRatio = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  healthRatio: {1}f", idx - 1, healthRatio.ToString("0.0"));

                        UInt32 curHealth = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  curHealth: {1}", idx - 1, curHealth);

                        UInt32 maxHealth = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  maxHealth: {1}", idx - 1, maxHealth);

                        float ammoRatio = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).ToArray(), 0);
                        Console.WriteLine("{0}\t  ammoRatio: {1}f", idx - 1, ammoRatio.ToString("0.0"));

                        UInt32 curAmmo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  curAmmo: {1}", idx - 1, curAmmo);

                        UInt32 maxAmmo = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  maxAmmo: {1}", idx - 1, maxAmmo);

                        UInt32 priority = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  priority: {1}", idx - 1, priority);

                        //byte[] UnknownA30 = bzn.fields[idx++].GetRawRef().ToArray();
                        //Console.Write("{0}\t  UnknownA30: ", idx - 1);
                        //for (int j = 0; j < UnknownA30.Length; j++)
                        //{
                        //    Console.Write("{0,2:X2}", UnknownA30[j]);
                        //}
                        //Console.WriteLine();
                        UInt32 what = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  what: 0x{1,8:X8}", idx - 1, what);

                        UInt32 who = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  who: {1}", idx - 1, who);

                        UInt32 where = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  where: 0x{1,8:X8}", idx - 1, where);

                        UInt32 param = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  param: {1}", idx - 1, param);

                        bool aiProcess = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  aiProcess: {1}", idx - 1, aiProcess);

                        bool isCargo = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  isCargo: {1}", idx - 1, isCargo);

                        UInt32 independence = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  independence: {1}", idx - 1, independence);

                        string curPilot = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                        Console.WriteLine("{0}\t  curPilot: {1}", idx - 1, curPilot);

                        UInt32 perceivedTeam = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  perceivedTeam: {1}", idx - 1, perceivedTeam);

                        Console.WriteLine("------------------------------");
                    }

                    {
                        string name = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());
                        Console.WriteLine("{0}\tname: {1}", idx - 1, name);

                        UInt32 sObject = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\tsObject: 0x{1,8:X8}", idx - 1, sObject);

                        Console.WriteLine("[AiMission]");

                        Console.WriteLine("[AOIs]");
                        UInt32 size = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  size: {1}", idx - 1, size);

                        Console.WriteLine("[AiPaths]");
                        UInt32 count = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().ToArray(), 0);
                        Console.WriteLine("{0}\t  count: {1}", idx - 1, count);
                    }
                }
            }
        }

        private static void ReadBZn64File(string filename)
        {
            N64BZN bzn = null;
            using (FileStream file = File.OpenRead(filename))
            {
                bzn = new N64BZN(file);
            }
            if (bzn != null)
            {
                int idx = 0;
                //for (int idx = 0; idx < bzn.fields.Count; idx++)
                {
                    //Field tmp = bzn.fields[idx];
                    Int32 Unknown1 = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                    bool Unknown2 = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                    string bznFilename = Encoding.ASCII.GetString(bzn.fields[idx++].GetRawRef());

                    Console.WriteLine("0\tUnknown1: {0}", Unknown1);
                    Console.WriteLine("1\tUnknown2: {0}", Unknown2);
                    Console.WriteLine("2\tbznFilename: {0}", bznFilename);

                    Int32 CountItems = BitConverter.ToInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                    Console.WriteLine("3\tCountItems: {0}", CountItems);
                    Console.WriteLine("------------------------------");
                    for (int i = 0; i < CountItems; i++)
                    {
                        UInt16 ItemID = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0); // 8 byte ASCII ID in BZ1
                        Console.WriteLine("{0}\t  ItemID: 0x{1,4:X4}", idx - 1, ItemID);

                        UInt16 UnknownA1 = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA1: 0x{1,8:X8}", idx - 1, UnknownA1);

                        float UnknownA2X = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        float UnknownA2Y = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                        float UnknownA2Z = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  UnknownA2: {1}f, {2}f, {3}f", idx - 1, UnknownA2X.ToString("0.0"), UnknownA2Y.ToString("0.0"), UnknownA2Z.ToString("0.0"));

                        UInt32 UnknownA3 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA3: 0x{1,8:X8}", idx - 1, UnknownA3);

                        UInt16 ItemLabel = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0); // 8 byte ASCII ID in BZ1
                        Console.WriteLine("{0}\t  ItemLabel: 0x{1,4:X4}", idx - 1, ItemLabel);

                        UInt32 UnknownA4 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA4: 0x{1,8:X8}", idx - 1, UnknownA4);

                        UInt32 ItemPointer = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  ItemPointer: 0x{1,8:X8}", idx - 1, ItemPointer);

                        byte[] OddMatrixData = bzn.fields[idx++].GetRawRef().ToArray();
                        Console.Write("{0}\t  OddMatrixData: ", idx - 1);
                        for (int j = 0; j < OddMatrixData.Length; j++)
                        {
                            Console.Write("{0,2:X2}", OddMatrixData[j]);
                        }
                        Console.WriteLine();

                        if (ItemLabel == 0x0001)
                        {
                            UInt32 UnknownA5 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                            Console.WriteLine("{0}\t  UnknownA5: 0x{1,8:X8}", idx - 1, UnknownA5);
                        }

                        float UnknownA6 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA6: {1}f", idx - 1, UnknownA6.ToString("0.0"));

                        float UnknownA7X = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        float UnknownA7Y = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                        float UnknownA7Z = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  UnknownA7: {1}f, {2}f, {3}f", idx - 1, UnknownA7X.ToString("0.0"), UnknownA7Y.ToString("0.0"), UnknownA7Z.ToString("0.0"));

                        float UnknownA8 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA8: {1}f", idx - 1, UnknownA8.ToString("0.0"));

                        float UnknownA9 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA9: {1}f", idx - 1, UnknownA9.ToString("0.0"));

                        float UnknownA10 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA10: {1}f", idx - 1, UnknownA10.ToString("0.0"));

                        float UnknownA11 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA11: {1}f", idx - 1, UnknownA11.ToString("0.0"));

                        float UnknownA12 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA12: {1}f", idx - 1, UnknownA12.ToString("0.0"));

                        float UnknownA13 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA13: {1}f", idx - 1, UnknownA13.ToString("0.0"));

                        float UnknownA14X = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        float UnknownA14Y = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                        float UnknownA14Z = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  UnknownA14: {1}f, {2}f, {3}f", idx - 1, UnknownA14X.ToString("0.0"), UnknownA14Y.ToString("0.0"), UnknownA14Z.ToString("0.0"));

                        float UnknownA15X = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        float UnknownA15Y = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                        float UnknownA15Z = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  UnknownA15: {1}f, {2}f, {3}f", idx - 1, UnknownA15X.ToString("0.0"), UnknownA15Y.ToString("0.0"), UnknownA15Z.ToString("0.0"));

                        float UnknownA16X = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        float UnknownA16Y = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(4).Take(4).Reverse().ToArray(), 0);
                        float UnknownA16Z = BitConverter.ToSingle(bzn.fields[idx].GetRawRef().Skip(8).Take(4).Reverse().ToArray(), 0);
                        idx++;
                        Console.WriteLine("{0}\t  UnknownA16: {1}f, {2}f, {3}f", idx - 1, UnknownA16X.ToString("0.0"), UnknownA16Y.ToString("0.0"), UnknownA16Z.ToString("0.0"));

                        UInt32 UnknownA17 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA17: 0x{1,8:X8}", idx - 1, UnknownA17);

                        byte[] UnknownA18 = bzn.fields[idx++].GetRawRef().ToArray();
                        Console.Write("{0}\t  UnknownA18: ", idx - 1);
                        for (int j = 0; j < UnknownA18.Length; j++)
                        {
                            Console.Write("{0,2:X2}", UnknownA18[j]);
                        }
                        Console.WriteLine();

                        bool UnknownA19 = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA19: {1}", idx - 1, UnknownA19);

                        bool UnknownA20 = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA20: {1}", idx - 1, UnknownA20);

                        UInt32 UnknownA21 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA21: 0x{1,8:X8}", idx - 1, UnknownA21);

                        UInt32 UnknownA22 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA22: 0x{1,8:X8}", idx - 1, UnknownA22);

                        float UnknownA23 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA23: {1}f", idx - 1, UnknownA23.ToString("0.0"));

                        UInt32 UnknownA24 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA24: 0x{1,8:X8}", idx - 1, UnknownA24);

                        UInt32 UnknownA25 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA25: 0x{1,8:X8}", idx - 1, UnknownA25);

                        float UnknownA26 = BitConverter.ToSingle(bzn.fields[idx++].GetRawRef().Take(4).Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA26: {1}f", idx - 1, UnknownA26.ToString("0.0"));

                        UInt32 UnknownA27 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA27: 0x{1,8:X8}", idx - 1, UnknownA27);

                        UInt32 UnknownA28 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA28: 0x{1,8:X8}", idx - 1, UnknownA28);

                        UInt32 UnknownA29 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA29: 0x{1,8:X8}", idx - 1, UnknownA29);

                        byte[] UnknownA30 = bzn.fields[idx++].GetRawRef().ToArray();
                        Console.Write("{0}\t  UnknownA30: ", idx - 1);
                        for (int j = 0; j < UnknownA30.Length; j++)
                        {
                            Console.Write("{0,2:X2}", UnknownA30[j]);
                        }
                        Console.WriteLine();

                        UInt32 UnknownA31 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA31: 0x{1,8:X8}", idx - 1, UnknownA31);

                        UInt32 UnknownA32 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA32Pointer: 0x{1,8:X8}", idx - 1, UnknownA32);

                        UInt32 UnknownA33 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA33: 0x{1,8:X8}", idx - 1, UnknownA33);

                        bool UnknownA34 = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA34: {1}", idx - 1, UnknownA34);

                        bool UnknownA35 = BitConverter.ToBoolean(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA35: {1}", idx - 1, UnknownA35);

                        UInt32 UnknownA36 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA36: 0x{1,8:X8}", idx - 1, UnknownA36);

                        UInt16 PilotItemID = BitConverter.ToUInt16(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0); // 8 byte ASCII ID in BZ1
                        Console.WriteLine("{0}\t  PilotItemID: 0x{1,4:X4}", idx - 1, PilotItemID);

                        UInt32 UnknownA37 = BitConverter.ToUInt32(bzn.fields[idx++].GetRawRef().Reverse().ToArray(), 0);
                        Console.WriteLine("{0}\t  UnknownA37: 0x{1,8:X8}", idx - 1, UnknownA37);

                        Console.WriteLine("------------------------------");
                    }
                }
            }
        }
    }
}
