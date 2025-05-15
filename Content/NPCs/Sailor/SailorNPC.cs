using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SubworldLibrary;


namespace Primordium.Content.NPCs.Sailor
{
    public class SailorNPC : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 25; // The amount of frames the NPC has

            NPCID.Sets.ExtraFramesCount[Type] = 9; 
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700; 
            NPCID.Sets.PrettySafe[Type] = 300;
            NPCID.Sets.AttackType[Type] = 1; 
            NPCID.Sets.AttackTime[Type] = 60; 
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4; 
            NPCID.Sets.ShimmerTownTransform[NPC.type] = true; 
            NPCID.Sets.ActsLikeTownNPC[Type] = true;
            NPCID.Sets.NoTownNPCHappiness[Type] = true;
            NPCID.Sets.SpawnsWithCustomName[Type] = true;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f, 
                Direction = 1 
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 0;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.friendly = true;
            NPC.townNPC = true;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            foreach (Player player in Main.player)
            {
                if (player.active && player.ZoneBeach && NPC.downedBoss2)
                    return true;
            }
            return false;
        }
        public override string GetChat()
        {
            return "The seas beyond here be treacherous, but I can take ye there if yer brave enough. What'a ya say, matey?";
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Yes";
            button2 = "What lies ahead?";
        }
        public override void OnChatButtonClicked(bool firstButton, ref string question)
        {
            if (firstButton)
            {
                SubworldSystem.Enter<VastOcean>();
            }
            else
            {
                Main.npcChatText = "Yar! The seas hold many forgotten treasures, and of course there's plenty o' beasts for ya to slay!";
            }
        }
    }
}
