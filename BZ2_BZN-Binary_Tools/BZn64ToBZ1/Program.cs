using BattlezoneBZNTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BZn64ToBZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns3.bzn Soviet\A510D4.bin";

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

                Console.WriteLine("version [1] =");
                Console.WriteLine("1045");
                Console.WriteLine("binarySave [1] =");
                Console.WriteLine("false");
                Console.WriteLine("msn_filename = {0}", bzn.msn_filename);
                Console.WriteLine("seq_count [1] =");
                Console.WriteLine(bzn.seq_count);
                Console.WriteLine("missionSave [1] =");
                Console.WriteLine(bzn.missionSave);
                Console.WriteLine("TerrainName = {0}", bzn.TerrainName);
                Console.WriteLine("size [1] =");
                Console.WriteLine(bzn.GameObjects.Length);
                foreach (BZNGameObjectWrapper obj in bzn.GameObjects)
                {
                    Console.Write(obj.GetBZ1ASCII());
                }
                Console.WriteLine("name = {0}", bzn.MissionClass);
                Console.WriteLine("sObject = {0:X8}", bzn.sObject);
                Console.WriteLine("[AiMission]");
                Console.WriteLine("[AOIs]");
                Console.WriteLine("size [1] =");
                Console.WriteLine(bzn.AOIs.Length);
                foreach (BZNAOI aoi in bzn.AOIs)
                {
                    Console.WriteLine("[AOI]");
                }
                Console.WriteLine("[AiPaths]");
                Console.WriteLine("count [1] =");
                Console.WriteLine(bzn.AiPaths.Length);
                foreach (BZNAiPath AiPath in bzn.AiPaths)
                {
                    Console.WriteLine("[AiPath]");
                    Console.WriteLine("old_ptr = {0:x8}", AiPath.old_ptr);
                    Console.WriteLine("size [1] =");
                    Console.WriteLine(AiPath.label.Length);
                    Console.WriteLine("label = {0}", AiPath.label);
                    Console.WriteLine("pointCount [1] =");
                    Console.WriteLine(AiPath.points.Length);
                    Console.WriteLine("points [{0}] =", AiPath.points.Length);
                    for (int pointCounter = 0; pointCounter < AiPath.points.Length; pointCounter++)
                    {
                        Console.WriteLine("  x [1] =");
                        Console.WriteLine(AiPath.points[pointCounter].x);
                        Console.WriteLine("  z [1] =");
                        Console.WriteLine(AiPath.points[pointCounter].z);
                    }
                    Console.WriteLine("pathType = {0:X8}", AiPath.pathType);
                }
            }
        }
    }
}
