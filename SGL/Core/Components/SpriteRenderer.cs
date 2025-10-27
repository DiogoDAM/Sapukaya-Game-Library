using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Sgl;

public class SpriteRenderer : Component
{
	public Sprite Sprite;
	public Color Color = Color.White;
	public Transform2D Transform;
	public float Depth;
	public Vector2 Origin;
	public SpriteEffects Flip;

	public SpriteRenderer() : base()
	{
		Sprite = new();
		Transform = new();
	}

	public SpriteRenderer(Sprite sprite) : base()
	{
		Sprite = sprite;
		Transform = new();
	}

	public SpriteRenderer(Sprite sprite, Transform2D transformParent) : base()
	{
		Sprite = sprite;
		Transform = new();
		Transform.Parent = transformParent;
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
		Core.SpriteBatch.Draw(Sprite.Texture, Transform.Position, Sprite.SourceRect, Color, Transform.Rotation, Origin, Transform.Scale, Flip, Depth);
	}
}
