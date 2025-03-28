﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Terraria.IO;
using Terraria.Localization;
using Terraria.WorldBuilding;
using Terraria.Chat;

namespace Primordium.Content.Tiles
{
    internal class EnigmaticOre : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.Ore[Type] = true;

            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 900;
            Main.tileShine2[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 350;

            AddMapEntry(new Color(146, 161, 185), CreateMapEntryName());

            DustType = DustID.AncientLight;

            HitSound = SoundID.LiquidsHoneyLava;

            MineResist = 1.5f;
            MinPick = 60;
        }
    }
   
    public class EnigmaticOreSystem : ModSystem
    {
        public static LocalizedText EnigmaticOrePassMessage { get; private set; }
        public static LocalizedText BlessedWithEnigmaticOreMessage { get; private set; }

        public override void SetStaticDefaults()
        {
            EnigmaticOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(EnigmaticOrePassMessage)}");
            BlessedWithEnigmaticOreMessage = Mod.GetLocalization($"WorldGen.{nameof(BlessedWithEnigmaticOreMessage)}");
        }

        
        public void BlessWorldWithEnigmaticOre()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return; // This should not happen, but just in case.
            }

            
            ThreadPool.QueueUserWorkItem(_ => {
                // Broadcast a message to notify the user.
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(BlessedWithEnigmaticOreMessage.Value, 50, 255, 130);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(BlessedWithEnigmaticOreMessage.ToNetworkText(), new Color(50, 255, 130));
                }

                 
                int splotches = (int)(100 * (Main.maxTilesX / 4200f));
                int highestY = (int)Utils.Lerp(Main.rockLayer, Main.UnderworldLayer, 0.5);
                for (int iteration = 0; iteration < splotches; iteration++)
                {

                    int i = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                    int j = WorldGen.genRand.Next(highestY, Main.UnderworldLayer);

                   
                    WorldGen.OreRunner(i, j, WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(5, 9), (ushort)ModContent.TileType<EnigmaticOre>());
                }
            });
        }

        
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {

            int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));

            if (ShiniesIndex != -1)
            {
              
                tasks.Insert(ShiniesIndex + 1, new EnigmaticOrePass("Primordium Ores", 237.4298f));
            }
        }
    }

    public class EnigmaticOrePass : GenPass
    {
        public EnigmaticOrePass(string name, float loadWeight) : base(name, loadWeight)
        {
        }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            // progress.Message is the message shown to the user while the following code is running.
            // Try to make your message clear. You can be a little bit clever, but make sure it is descriptive enough for troubleshooting purposes.
            progress.Message = EnigmaticOreSystem.EnigmaticOrePassMessage.Value;

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);

                
                int y = WorldGen.genRand.Next((int)GenVars.worldSurfaceLow, Main.maxTilesY);

                
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), ModContent.TileType<EnigmaticOre>());
            }
        }
    }
}
