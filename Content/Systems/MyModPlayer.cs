using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

public class MyModPlayer : ModPlayer
{
    public float ScreenShakeIntensity;

    public override void ModifyScreenPosition()
    {
        if (ScreenShakeIntensity > 0)
        {
            Main.screenPosition += new Vector2(
                Main.rand.NextFloat(-ScreenShakeIntensity, ScreenShakeIntensity),
                Main.rand.NextFloat(-ScreenShakeIntensity, ScreenShakeIntensity));
            ScreenShakeIntensity *= 0.9f;

            if (ScreenShakeIntensity < 0.1f)
                ScreenShakeIntensity = 0;
        }
    }
}
