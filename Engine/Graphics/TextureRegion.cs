using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public sealed class TextureRegion
{
	public Texture2D Texture;
	public Rectangle Bounds;

	public TextureRegion(Texture2D texture, int x, int y, int w, int h)
	{
		Texture = texture;
		Bounds = new Rectangle(x, y, w, h);
	}

	public TextureRegion(Texture2D texture, Rectangle bounds)
	{
		Texture = texture;
		Bounds = bounds;
	}

	public void Draw(Vector2 pos, Color color)
	{
		Core.SpriteBatch.Draw(Texture, pos, Bounds, color);
	}

	public void Draw(Vector2 pos, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float depth)
	{
		Core.SpriteBatch.Draw(Texture, pos, Bounds, color, rotation, origin, scale, spriteEffects, depth);
	}
}
