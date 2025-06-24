﻿using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "service")]
    public class ClassServiceTruckFactory : IClassFactory
    {
        public bool Create(BZNFileBattlezone parent, BZNStreamReader reader, EntityDescriptor preamble, string classLabel, out Entity? obj, bool create = true)
        {
            obj = null;
            if (create)
                obj = new ClassServiceTruck(preamble, classLabel);
            ClassServiceTruck.Hydrate(parent, reader, obj as ClassServiceTruck);
            return true;
        }
    }
    public class ClassServiceTruck : ClassTrackedVehicle
    {
        public ClassServiceTruck(EntityDescriptor preamble, string classLabel) : base(preamble, classLabel) { }
        public static void Hydrate(BZNFileBattlezone parent, BZNStreamReader reader, ClassServiceTruck? obj)
        {
            ClassTrackedVehicle.Hydrate(parent, reader, obj as ClassTrackedVehicle);
        }
    }
}
