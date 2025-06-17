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
            //string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (Soviet)\misns3.bzn Soviet\A510D4.bin";
            //string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn02b.bzn US\A304C0.bin";
            //string filename = @"F:\Battlezone\Projects\BZN64 Rebuild\Data Extraction\BZNs\Missions (US)\misn08.bzn US\A3C8B8.bin";

            if (args.Length == 0) return;
            string filename = args[0];

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

                string outputName = Path.GetFileName(Path.GetDirectoryName(filename));

                string outputFilename = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar + outputName + @"_" + Path.GetFileNameWithoutExtension(filename) + @".txt";
                File.WriteAllText(outputFilename, bzn.GetBZ1ASCII());

                //Console.WriteLine(bzn.GetBZ1ASCII());
            }
        }
    }
}
