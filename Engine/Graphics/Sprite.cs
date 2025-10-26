using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public class Sprite : Component
{
	public TextureRegion Region;

	public Transform2D Transform;
	public Color Color = Color.White;
	public Vector2 Origin;
	public SpriteEffects Flip;
	public float Depth;

	public int Width => Region.Bounds.Width;
	public int Height => Region.Bounds.Height;

	public Sprite()
	{
		Region = null;
		Transform = new();
	}

	public override void Added()
	{
		IsDrawable = true;
	}

	public override void Removed()
	{
		IsDrawable = false;
	}

	public override void Draw()
	{
		Region.Draw(Transform.Position, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
	}

	public void Centralize()
	{
		Origin.X = Width * .5f;
		Origin.Y = Height * .5f;
	}
}
