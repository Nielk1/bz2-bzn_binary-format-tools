using BZNParser.Reader;

namespace BZNParser.Battlezone.GameObject
{
    [ObjectClass(BZNFormat.Battlezone2, "turrettank")]
    public class ClassTurretTank2 : ClassDeployable
    {
        public ClassTurretTank2(string PrjID, bool isUser, string classLabel) : base(PrjID, isUser, classLabel) { }
        public override void LoadData(BZNStreamReader reader)
        {
            IBZNToken tok;

            if (reader.Version < 1110 && ClassLabel == "artillery")
            {
                base.LoadData(reader);
                return;
            }

            bool m_Use13Aim = false; // we're assuming Use13Aim is impossible before 1109
            //if (reader.Version < 1109 || !m_Use13Aim)
            {
                long pos = reader.BaseStream.Position;
                // Use13Aim might be true if we're >= 1109, so be prepared to walk back and try again
                // if it is Use13Aim, we expect a bool first, if it's not we expect a float

                tok = reader.ReadToken();
                if (tok.Validate("turretAligned", BinaryFieldType.DATA_BOOL))
                {
                    if (reader.Version < 1109)
                    {
                        // we read a turretAligned but we're too old a version for that to be a thing
                        throw new Exception("Use13Aim turret save data found in BZN Version < 1109, impossible, parse error expected");
                    }

                    //turretAligned = tok.GetBoolean();
                    m_Use13Aim = true;
                }
                else
                {
                    // walk back and try again
                    reader.BaseStream.Position = pos;

                    tok = reader.ReadToken();
                    if (!tok.Validate("omegaTurret", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse omegaTurret/FLOAT");

                    tok = reader.ReadToken();
                    if (!tok.Validate("alphaTurret", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse alphaTurret/FLOAT");

                    tok = reader.ReadToken();
                    if (!tok.Validate("timeDeploy", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse timeDeploy/FLOAT");

                    tok = reader.ReadToken();
                    if (!tok.Validate("timeUndeploy", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse timeUndeploy/FLOAT");

                    tok = reader.ReadToken();
                    if (!tok.Validate("state", BinaryFieldType.DATA_VOID))
                        throw new Exception("Failed to parse state/VOID");

                    tok = reader.ReadToken();
                    if (!tok.Validate("delayTimer", BinaryFieldType.DATA_FLOAT))
                        throw new Exception("Failed to parse delayTimer/FLOAT");

                    if (reader.Version == 1100)
                    {
                        // obsolete
                        tok = reader.ReadToken();
                        if (!tok.Validate("wantTurret", BinaryFieldType.DATA_BOOL)) throw new Exception("Failed to parse wantTurret/BOOL");
                    }
                    else
                    {
                        tok = reader.ReadToken();
                        if (!tok.Validate("turretAligned", BinaryFieldType.DATA_BOOL))
                            throw new Exception("Failed to parse turretAligned/BOOL");
                    }

                    // reader.SaveType != 0 && reader.Version >= 1140

                    //if (reader.Version < 1109)
                    //{
                    //    // hovercraft load instead of normal base
                    //}
                }
            }

            // reader.SaveType != 0

            base.LoadData(reader);
        }
    }
}
