using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public static class Drawer
{
	//Create Textures
	public static Texture2D RectangleTexture2D(int w, int h)
	{
		Texture2D texture = new(Core.GraphicsDevice, w, h);

		return texture;
	}

	public static Texture2D LineTexture2D(Vector2 start, Vector2 end, int thickness)
	{
		Vector2 deltaD = end - start;

		Texture2D texture = new(Core.GraphicsDevice, (int)deltaD.Length(), thickness);

		return texture;
	}



	//Draw
	public static void DrawRectangle(int w, int h, Vector2 pos, Color color, float depth=1f)
	{
		Texture2D texture = new(Core.GraphicsDevice, w, h);

		Core.SpriteBatch.Draw(texture, pos, null, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
	}

	public static void DrawLine(Vector2 start, Vector2 end, int thickness, Color color, float depth=1f)
	{
		Vector2 deltaD = end - start;
		float angle = (float)Math.Atan2(deltaD.Y, deltaD.X);

		Texture2D texture = new(Core.GraphicsDevice, (int)deltaD.Length(), thickness);

		Core.SpriteBatch.Draw(texture, start, null, color, angle, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
	}
}
