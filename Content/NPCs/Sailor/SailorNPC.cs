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

            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700; 
            NPCID.Sets.PrettySafe[Type] = 300;
            NPCID.Sets.AttackType[Type] = 1; 
            NPCID.Sets.AttackTime[Type] = 60; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
            NPCID.Sets.ShimmerTownTransform[NPC.type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

            NPCID.Sets.ActsLikeTownNPC[Type] = true;

            // This prevents the happiness button
            NPCID.Sets.NoTownNPCHappiness[Type] = true;

            NPCID.Sets.SpawnsWithCustomName[Type] = true;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7; // Town NPC AI
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
            return "The seas beyond here are treacherous, but I can take you there if you’re brave enough. What do you say, matey?";
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Yes"; // First button
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                Main.NewText("Prepare yourself, we set sail now!", 0, 100, 255);
                SubworldSystem.Enter<VastOcean>();
            }
            //No need for else statement
        }
    }
}
