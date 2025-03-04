
using Terraria;
using Terraria.ModLoader;

namespace WarriorsPath.Content.NPCs.Bosses.GiantLeech
{
    public class GiantLeech_Body : GiantLeech
    {
        public override int BodyType => ModContent.NPCType<GiantLeech_Body>();
        public override int TailType => ModContent.NPCType<GiantLeech_Tail>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.lifeMax = 3000; // Body segments have less health
            NPC.damage = 40;    // Body segments do less damage
            NPC.width = 90;
            NPC.height = 60;
        }
    }
}