using Microsoft.Xna.Framework;

namespace Sgl;

public sealed class BoxCollider : Collider
{
	public int Width;
	public int Height;

	public float Left => Position.X;
	public float Right => Position.X + Width;
	public float Top => Position.Y;
	public float Bottom => Position.Y + Height;

	public BoxCollider() : base()
	{
	}

	protected override bool CollidesWith(BoxCollider box)
	{
		return CollisionHelper.BoxWithBox(this, box);
	}

	protected override bool CollidesWith(Vector2 vec)
	{
		return CollisionHelper.BoxWithVector2(this, vec);
	}

	protected override bool CollidesWith(CircleCollider circle)
	{
		return CollisionHelper.BoxWithCircle(this, circle);
	}
}
