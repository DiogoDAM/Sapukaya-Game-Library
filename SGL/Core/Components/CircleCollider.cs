using Microsoft.Xna.Framework;

namespace Sgl;

public sealed class CircleCollider : Collider
{
	public int Radius;
	public int Diameter => Radius*2;

	public Vector2 Center => new Vector2(Position.X + Radius, Position.Y + Radius);

	public CircleCollider() : base()
	{
	}

	public CircleCollider(int radius) : base()
	{
		Radius = radius;
	}

	protected override bool CollidesWith(BoxCollider box)
	{
		return CollisionHelper.BoxWithCircle(box, this);
	}

	protected override bool CollidesWith(Vector2 vec)
	{
		return CollisionHelper.CircleWithVector2(this, vec);
	}

	protected override bool CollidesWith(CircleCollider circle)
	{
		return CollisionHelper.CircleWithCircle(this, circle);
	}
}
