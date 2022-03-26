﻿using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using SDVFactory.Factory;
using StardewValley;
using StardewValley.Objects;
using System;

namespace SDVFactory
{
    internal static class Harmony
    {
        internal static void Patch()
        {
            var harmony = new HarmonyLib.Harmony("bwdy.FactoryMod");
            harmony.Patch(
               original: AccessTools.Method(typeof(Furniture), nameof(Furniture.draw), new Type[] { typeof(SpriteBatch), typeof(int), typeof(int), typeof(float) }),
               prefix: new HarmonyMethod(typeof(Harmony), nameof(Furniture_draw_Pre))
            );
            harmony.Patch(
               original: AccessTools.Method(typeof(StardewValley.Object), nameof(StardewValley.Object.getCategoryColor)),
               postfix: new HarmonyMethod(typeof(Harmony), nameof(Object_getCategoryColor_Post))
            );
            harmony.Patch(
               original: AccessTools.Method(typeof(StardewValley.Object), nameof(StardewValley.Object.getCategoryName)),
               postfix: new HarmonyMethod(typeof(Harmony), nameof(Object_getCategoryName_Post))
            );
        }

        public static string Object_getCategoryName_Post(string __result, StardewValley.Object __instance)
        {
            if (__instance is Furniture)
            {
                var f = __instance as Furniture;
                if (f.modData.ContainsKey("FactoryMod"))
                {
                    return "Factory Component";
                }
            }
            return __result;
        }

        public static Color Object_getCategoryColor_Post(Color __result, StardewValley.Object __instance)
        {
            if (__instance is Furniture)
            {
                var f = __instance as Furniture;
                if (f.modData.ContainsKey("FactoryMod"))
                {
                    return Color.DarkOrange;
                }
            }
            return __result;
        }

        public static bool Furniture_draw_Pre(Furniture __instance, SpriteBatch spriteBatch, int x, int y, float alpha = 1f)
        {
            //todo, readd custom texture here
            //todo, make a machine class to simplify this stuff
            Furniture f = __instance as Furniture;
            if (!f.modData.ContainsKey("FactoryMod")) return true;

            if (f.isTemporarilyInvisible)
            {
                return false;
            }
            var sourceIndexOffset = FactoryGame.Helper.Reflection.GetField<NetInt>(f, "sourceIndexOffset").GetValue().Value;
            var drawPosition = FactoryGame.Helper.Reflection.GetField<NetVector2>(f, "drawPosition").GetValue().Value;
            Rectangle drawn_source_rect = f.sourceRect.Value;
            drawn_source_rect.X += drawn_source_rect.Width * sourceIndexOffset;
            if (Furniture.isDrawingLocationFurniture)
            {
                spriteBatch.Draw(Furniture.furnitureTexture, Game1.GlobalToLocal(Game1.viewport, drawPosition + ((f.shakeTimer > 0) ? new Vector2(Game1.random.Next(-1, 2), Game1.random.Next(-1, 2)) : Vector2.Zero)), drawn_source_rect, Color.White * alpha, 0f, Vector2.Zero, 4f, f.Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, (f.furniture_type.Value == 12) ? (2E-09f + f.TileLocation.Y / 100000f) : ((float)(f.boundingBox.Value.Bottom - ((f.furniture_type.Value == 6 || f.furniture_type.Value == 17 || f.furniture_type.Value == 13) ? 48 : 8)) / 10000f));
            }
            else
            {
                spriteBatch.Draw(Furniture.furnitureTexture, Game1.GlobalToLocal(Game1.viewport, new Vector2(x * 64 + ((f.shakeTimer > 0) ? Game1.random.Next(-1, 2) : 0), y * 64 - (f.sourceRect.Height * 4 - f.boundingBox.Height) + ((f.shakeTimer > 0) ? Game1.random.Next(-1, 2) : 0))), f.sourceRect, Color.White * alpha, 0f, Vector2.Zero, 4f, f.Flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None, (f.furniture_type.Value == 12) ? (2E-09f + f.TileLocation.Y / 100000f) : ((float)(f.boundingBox.Value.Bottom - (((int)f.furniture_type.Value == 6 || (int)f.furniture_type.Value == 17 || (int)f.furniture_type.Value == 13) ? 48 : 8)) / 10000f));
            }

            return false;
        }
    }
}