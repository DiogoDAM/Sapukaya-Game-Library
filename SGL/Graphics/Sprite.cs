using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public sealed class Sprite
{
	public Texture2D Texture;

	public Rectangle SourceRect;

	public Sprite(Texture2D texture)
	{
		Texture = texture;
		SourceRect = new Rectangle(0, 0, Texture.Bounds.Width, Texture.Bounds.Height);
	}

	public Sprite(Texture2D texture, Rectangle sourceRectangle)
	{
		Texture = texture;
		SourceRect = sourceRectangle;
	}

	public Sprite()
	{
		Texture = null;
		SourceRect = Rectangle.Empty;
	}

	public override string ToString() => $"{Texture}, {SourceRect}";
}
