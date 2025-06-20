using BZNParser.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone, "hover")]
    public class ClassHoverCraftFactory : IClassFactory
    {
        public bool Create(BZNStreamReader reader, string PrjID, bool isUser, string classLabel, out ClassGameObject? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassHoverCraft(PrjID, isUser, classLabel);
            ClassHoverCraft.Build(reader, obj as ClassHoverCraft);
            return true;
        }
    }
    public class ClassHoverCraft : ClassCraft
    {
        public ClassHoverCraft(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public static void Build(BZNStreamReader reader, ClassHoverCraft? obj)
        {
            if (reader.Format == BZNFormat.Battlezone && reader.Version > 1001 && reader.Version < 1026)
            {
                IBZNToken tok = reader.ReadToken();
                tok = reader.ReadToken(); // accelDragStop
                tok = reader.ReadToken(); // accelDragFull
                tok = reader.ReadToken(); // alphaTrack
                tok = reader.ReadToken(); // alphaDamp
                tok = reader.ReadToken(); // pitchPitch
                tok = reader.ReadToken(); // pitchThrust
                tok = reader.ReadToken(); // rollStrafe
                tok = reader.ReadToken(); // rollSteer
                tok = reader.ReadToken(); // velocForward
                tok = reader.ReadToken(); // velocReverse
                tok = reader.ReadToken(); // velocStrafe
                tok = reader.ReadToken(); // accelThrust
                tok = reader.ReadToken(); // accelBrake
                tok = reader.ReadToken(); // omegaSpin
                tok = reader.ReadToken(); // omegaTurn
                tok = reader.ReadToken(); // alphaSteer
                tok = reader.ReadToken(); // accelJump
                tok = reader.ReadToken(); // thrustRatio
                tok = reader.ReadToken(); // throttle
                tok = reader.ReadToken(); // airBorne
            }

            ClassCraft.Build(reader, obj as ClassCraft);
        }
    }
}
