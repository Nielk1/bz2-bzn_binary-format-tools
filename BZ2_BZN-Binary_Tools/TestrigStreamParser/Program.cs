using BattlezoneBZNTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestrigStreamParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filename = @"D:\My Files\Battlezone\~Scratch\unzfs\stock\files\bzone\misn08.bzn";
            string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns3.bzn Soviet\A510D4.bin";
            //string filename = @"F:\Battlezone\Projects\Programs\BinaryBZNReader\bz2-bzn_binary-format-tools\BZ2_BZN-Binary_Tools\BZNs\bin\multmp01.bzn";
            //string filename = @"F:\Battlezone\Projects\Programs\BinaryBZNReader\bz2-bzn_binary-format-tools\BZ2_BZN-Binary_Tools\BZNs\paths&aois\misn05ascii.bzn";
            //string filename = @"F:\Battlezone\Projects\Programs\BinaryBZNReader\bz2-bzn_binary-format-tools\BZ2_BZN-Binary_Tools\BZNs\paths&aois\misn05.bzn";
            using (FileStream file = File.OpenRead(filename))
            {
                BZNFile bzn;
                if (Path.GetExtension(filename) == ".bin")
                {
                    bzn = BZNFile.OpenBZn64(file);
                }
                else
                {
                    bzn = BZNFile.OpenBZ1(file);
                }
                Console.WriteLine("Version: {0}", bzn.Version);
                Console.WriteLine("Platform: {0}", bzn.N64 ? "N64" : "PC");
                Console.WriteLine("Mode: {0}", bzn.BinaryMode ? "BIN" : "ASCII");
                Console.WriteLine();
                Console.WriteLine("msn_filename: \"{0}\"", bzn.msn_filename);
                if (!bzn.N64)
                {
                    Console.WriteLine("seq_count: {0}", bzn.seq_count);
                    Console.WriteLine("missionSave: {0}", bzn.missionSave);
                    Console.WriteLine("TerrainName: {0}", bzn.TerrainName);
                    Console.WriteLine();
                }
                Console.WriteLine("Game Objects\tCount: {0}", bzn.GameObjects.Length);
                foreach(BZNGameObjectWrapper obj in bzn.GameObjects)
                {
                    string type = obj.gameObject.GetType().Name.Substring(3);
                    if (type == "GameObject") type += "?";

                    Console.WriteLine(
                        "{0}  {1}  {2}  [{3,6:F2}, {4,6:F2}, {5,9:F2}]",
                        obj.seqno.ToString().PadLeft(4),
                        obj.PrjID.PadRight(12),
                        type.PadRight(16),
                        obj.pos.x,
                        obj.pos.y,
                        obj.pos.z
                    );
                }
                Console.WriteLine();
                Console.WriteLine("MissionClass: \"{0}\"", bzn.MissionClass);
                Console.WriteLine("sObject: {0:X8}", bzn.sObject);
                Console.WriteLine();
                Console.WriteLine("AOI\tCount: {0}", bzn.AOIs.Length);
                foreach (BZNAOI aoi in bzn.AOIs)
                {
                    Console.WriteLine(
                        "{0:X8}\tTeam: {1}\tInteresting: {2}\tInside: {3}\tValue: {4}\tForce: {5}",
                        aoi.undefptr,
                        aoi.team.ToString().PadLeft(2),
                        aoi.interesting,
                        aoi.inside,
                        aoi.value,
                        aoi.force
                    );
                }
                Console.WriteLine();
                Console.WriteLine("AiPath\tCount: {0}", bzn.AiPaths.Length);
                foreach (BZNAiPath AiPath in bzn.AiPaths)
                {
                    Console.WriteLine("OldPtr: {0:X8}\tPathType: {1}\tLabel: {2}", AiPath.old_ptr, AiPath.pathType, AiPath.label);
                    for (int pointCounter = 0; pointCounter < AiPath.points.Length; pointCounter++)
                    {
                        Console.WriteLine("\t[{0,2}] {1,8:F2}, {2,8:F2}", pointCounter, AiPath.points[pointCounter].x, AiPath.points[pointCounter].z);
                    }
                }
            }
        }
    }
}
