using Microsoft.Xna.Framework;

namespace Sgl;

public sealed class CircleCollider : Collider
{
	public int Radius;

	public CircleCollider() : base()
	{
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
