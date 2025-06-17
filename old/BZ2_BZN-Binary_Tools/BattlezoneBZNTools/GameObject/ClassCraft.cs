using BattlezoneBZNTools.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattlezoneBZNTools.GameObject
{
    public class ClassCraft : ClassGameObject
    {
        public UInt32 abandoned { get; set; }

        public ClassCraft(string PrjID, bool isUser) : base(PrjID, isUser) { }
        public override void LoadData(BZNReader reader)
        {
            IBZNToken tok;

            if(reader.N64 || reader.Version > 1022)
            {
                tok = reader.ReadToken();
                if (!tok.Validate("abandoned", BinaryFieldType.DATA_LONG)) throw new Exception("Failed to parse abandoned/LONG");
                abandoned = tok.GetUInt32();
            }
            else
            {
                tok = reader.ReadToken();//setAltitude [1] =
                //1
                tok = reader.ReadToken();//accelDragStop [1] =
                //3.5
                tok = reader.ReadToken();//accelDragFull [1] =
                //1
                tok = reader.ReadToken();//alphaTrack [1] =
                //20
                tok = reader.ReadToken();//alphaDamp [1] =
                //5
                tok = reader.ReadToken();//pitchPitch [1] =
                //0.25
                tok = reader.ReadToken();//pitchThrust [1] =
                //0.1
                tok = reader.ReadToken();//rollStrafe [1] =
                //0.1
                tok = reader.ReadToken();//rollSteer [1] =
                //0.1
                tok = reader.ReadToken();//velocForward [1] =
                //20
                tok = reader.ReadToken();//velocReverse [1] =
                //15
                tok = reader.ReadToken();//velocStrafe [1] =
                //20
                tok = reader.ReadToken();//accelThrust [1] =
                //20
                tok = reader.ReadToken();//accelBrake [1] =
                //75
                tok = reader.ReadToken();//omegaSpin [1] =
                //4
                tok = reader.ReadToken();//omegaTurn [1] =
                //1.5
                tok = reader.ReadToken();//alphaSteer [1] =
                //5
                tok = reader.ReadToken();//accelJump [1] =
                //20
                tok = reader.ReadToken();//thrustRatio [1] =
                //1
                tok = reader.ReadToken();//throttle [1] =
                //0
                tok = reader.ReadToken();//airBorne [1] =
                //5.96046e-008
            }

            base.LoadData(reader);
        }

        public override string GetBZ1ASCII()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("abandoned [1] =");
            sb.AppendLine(abandoned.ToString());

            sb.Append(base.GetBZ1ASCII());

            return sb.ToString();
        }
    }
}
