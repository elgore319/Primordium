using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Primordium.Content.Systems
{
	public class DownedBossSystem : ModSystem
	{
		public static bool downedDefenseMech = false;
		public override void ClearWorld() {
			downedDefenseMech = false;
		}

		public override void SaveWorldData(TagCompound tag) {
			if (downedDefenseMech) {
				tag["downedDefenseMech"] = true;
			}
		}

		public override void LoadWorldData(TagCompound tag) {
			downedDefenseMech = tag.ContainsKey("downedDefenseMech");
		}

		public override void NetSend(BinaryWriter writer) {
			var flags = new BitsByte();
			flags[0] = downedDefenseMech;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader) {
			BitsByte flags = reader.ReadByte();
			downedDefenseMech = flags[0];
		}
	}
}
