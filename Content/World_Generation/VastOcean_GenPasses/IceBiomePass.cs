using Terraria;
using Terraria.ID;
using Terraria.WorldBuilding;
using Terraria.IO;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

namespace Primordium.Content.World_Generation.VastOcean_GenPasses
{
    public class IceBiomePass : GenPass
    {
        public IceBiomePass() : base("Ice Biome", 1f) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Sculpting Glacial Networks";

            const int iceLevel = 501;
            int iceWidth = Main.maxTilesX / 8;
            var caveSystems = new List<Rectangle>();

            // PHASE 1: Core Ice Mass
            GenerateJaggedIceCore(progress, iceWidth, iceLevel);

            // PHASE 2: Cave Systems
            for (int i = 0; i < 3; i++)
            {
                var mainCave = CarveCaveSystem(
                    WorldGen.genRand.Next(50, iceWidth - 50),
                    iceLevel + WorldGen.genRand.Next(100, 300),
                    5,
                    iceWidth,
                    iceLevel
                );
                caveSystems.Add(mainCave);
            }

            // PHASE 3: Ice Pillars
            GenerateIcePillars(iceWidth, iceLevel, caveSystems);

            // PHASE 4: Glacial Trenches
            GenerateTrenches(iceWidth, iceLevel);

            // PHASE 5: Decorative Features
            PlaceFrozenRuins(iceWidth, iceLevel);
            GenerateIceSpikes(iceWidth, iceLevel);
        }

        private void GenerateJaggedIceCore(GenerationProgress progress, int iceWidth, int iceLevel)
        {
            for (int x = 0; x < iceWidth; x++)
            {
                if (x % 50 == 0) progress.Set((float)x / iceWidth);

                int baseY = iceLevel + (int)(Math.Sin(x * 0.05f) * 15 + WorldGen.genRand.Next(-10, 10));
                float density = 1f - (x / (float)iceWidth * 0.3f);

                for (int y = baseY; y < Main.maxTilesY; y++)
                {
                    if (WorldGen.genRand.NextFloat() < density)
                        WorldGen.PlaceTile(x, y, TileID.IceBlock, forced: true);
                }
            }
        }

        private Rectangle CarveCaveSystem(int startX, int startY, int maxBranches, int iceWidth, int iceLevel)
        {
            var systemBounds = new Rectangle(startX, startY, 0, 0);
            int branches = 0;

            void CarveBranch(int x, int y, int length, bool isMain = true)
            {
                int dirX = WorldGen.genRand.Next(-1, 2);
                int dirY = WorldGen.genRand.NextBool(3) ? 0 : 1;

                for (int i = 0; i < length; i++)
                {
                    systemBounds = Rectangle.Union(systemBounds, new Rectangle(x, y, 1, 1));
                    int radius = WorldGen.genRand.Next(isMain ? 5 : 3, isMain ? 12 : 8);
                    WorldGen.TileRunner(x, y, radius, radius, TileID.SnowBlock);

                    if (branches < maxBranches && WorldGen.genRand.NextBool(20))
                    {
                        branches++;
                        CarveBranch(x, y, WorldGen.genRand.Next(30, 80), false);
                    }

                    x += dirX;
                    y += dirY;

                    if (x < 10 || x >= iceWidth - 10) dirX *= -1;
                    if (y < iceLevel + 20 || y > Main.maxTilesY - 50) dirY *= -1;
                }
            }

            CarveBranch(startX, startY, WorldGen.genRand.Next(100, 200));
            return systemBounds;
        }

        private void GenerateIcePillars(int iceWidth, int iceLevel, List<Rectangle> caveSystems)
        {
            for (int x = 20; x < iceWidth - 20; x += WorldGen.genRand.Next(30, 60))
            {
                bool inCave = caveSystems.Exists(c => c.Contains(x, iceLevel + 50));
                if (inCave) continue;

                int height = WorldGen.genRand.Next(20, 50);
                int startY = iceLevel + WorldGen.genRand.Next(10, 30);

                for (int y = startY; y < startY + height; y++)
                {
                    int width = (int)(height * 0.3f * (1 - (y - startY) / (float)height) + 2);
                    for (int w = -width; w <= width; w++)
                    {
                        if (x + w > 0 && x + w < iceWidth)
                            WorldGen.PlaceTile(x + w, y, TileID.IceBlock, forced: true);
                    }
                }
            }
        }

        private void GenerateTrenches(int iceWidth, int iceLevel)
        {
            for (int i = 0; i < 4; i++)
            {
                int trenchX = WorldGen.genRand.Next(iceWidth / 4, iceWidth);
                int width = WorldGen.genRand.Next(30, 80);
                int depth = WorldGen.genRand.Next(20, 40);

                for (int x = trenchX; x < trenchX + width && x < iceWidth; x++)
                {
                    int baseY = iceLevel + (int)(Math.Sin((x - trenchX) * 0.1f) * 10) + WorldGen.genRand.Next(-5, 5);

                    for (int y = baseY; y < baseY + depth && y < Main.maxTilesY; y++)
                    {
                        WorldGen.KillTile(x, y);
                        if (WorldGen.genRand.NextBool(3))
                            WorldGen.PlaceTile(x, y, TileID.BreakableIce, forced: true);
                    }
                }
            }
        }

        private void GenerateIceSpikes(int iceWidth, int iceLevel)
        {
            for (int x = 10; x < iceWidth - 10; x++)
            {
                if (!WorldGen.genRand.NextBool(40)) continue;

                int spikeHeight = WorldGen.genRand.Next(5, 15);
                int baseY = FindTopIce(x, iceLevel);

                for (int y = baseY; y > baseY - spikeHeight; y--)
                {
                    if (y < 10) break;
                    WorldGen.PlaceTile(x, y, TileID.IceBlock, forced: true);
                }
            }
        }

        private int FindTopIce(int x, int iceLevel)
        {
            for (int y = iceLevel; y < Main.maxTilesY; y++)
            {
                if (Main.tile[x, y].HasTile && Main.tile[x, y].TileType == TileID.IceBlock)
                    return y;
            }
            return iceLevel;
        }

        private void PlaceFrozenRuins(int iceWidth, int iceLevel)
        {
            for (int k = 0; k < WorldGen.genRand.Next(1, 4); k++)
            {
                int ruinX = WorldGen.genRand.Next(50, iceWidth - 50);
                int ruinY = iceLevel + WorldGen.genRand.Next(50, 150);
                int ruinWidth = WorldGen.genRand.Next(20, 30);
                int ruinHeight = WorldGen.genRand.Next(12, 20);

                for (int x = ruinX; x < ruinX + ruinWidth; x++)
                {
                    for (int y = ruinY; y < ruinY + ruinHeight; y++)
                    {
                        bool isWall = x == ruinX || x == ruinX + ruinWidth - 1 ||
                                    y == ruinY || y == ruinY + ruinHeight - 1;

                        if (isWall)
                            WorldGen.PlaceTile(x, y, TileID.IceBrick, forced: true);
                        else if (WorldGen.genRand.NextBool(4))
                            WorldGen.PlaceTile(x, y, TileID.BreakableIce, forced: true);
                    }
                }
            }
        }
    }
}