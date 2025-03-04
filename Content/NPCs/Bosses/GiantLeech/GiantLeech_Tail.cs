using Terraria.ModLoader;

namespace WarriorsPath.Content.NPCs.Bosses.GiantLeech
{
    public class GiantLeech_Tail : GiantLeech
    {
        public override int BodyType => ModContent.NPCType<GiantLeech_Body>();
        public override int TailType => ModContent.NPCType<GiantLeech_Tail>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 2000; // Tail segments have the least health
            NPC.damage = 30;    // Tail segments deal the least damage
            NPC.width = 82;
            NPC.height = 76;
        }
    }
}