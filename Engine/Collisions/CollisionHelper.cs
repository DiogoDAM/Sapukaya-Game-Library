using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sgl;

public static class CollisionHelper
{
	public static bool BoxWithBox(BoxCollider b1, BoxCollider b2)
	{
		return b1.Left <= b2.Right &&
			b1.Right >= b2.Left &&
			b1.Top <= b2.Bottom &&
			b1.Bottom >= b1.Top;
	}

	public static bool BoxWithVector2(BoxCollider b, Vector2 v)
	{
		return v.X <= b.Right &&
			v.X >= b.Left &&
			v.Y <= b.Bottom &&
			v.Y >= b.Top;
	}

	public static bool BoxWithCircle(BoxCollider b, CircleCollider c) 
	{
		float closestX = MathHelper.Clamp(c.Position.X, b.Left, b.Right);
		float closestY = MathHelper.Clamp(c.Position.Y, b.Top, b.Bottom);

		float dx = c.Position.X - closestX;
		float dy = c.Position.Y - closestY;

		return dx * dx + dy * dy <= c.Radius * c.Radius;
	}

	public static bool CircleWithCircle(CircleCollider c1, CircleCollider c2)
	{
		Vector2 d = c2.Position - c1.Position;
		return d.Length() <= c1.Radius + c2.Radius;
	}

	public static bool CircleWithVector2(CircleCollider c, Vector2 v)
	{
		Vector2 d = v - c.Position;
		return d.Length() <= c.Radius;
	}

	public static void ProcessCollisions(IEnumerable<Collider> colliders1, IEnumerable<Collider> colliders2, Action<Collider, Collider> resolveCollision)
	{
		foreach(var col1 in colliders1)
		{
			foreach(var col2 in colliders2)
			{
				if(col1.CollidesWith(col2)) resolveCollision(col1, col2);
			}
		}
	}
}
