using Microsoft.Xna.Framework;

namespace Sgl;

public abstract class Collider : Component
{
	public Vector2 Position { get { return Entity.Transform.Position + m_offset; } set { m_offset = value; } }
	private Vector2 m_offset;

	public Collider()
	{
	}

	public bool CollidesWith(Collider other)
	{
		switch(other)
		{
			case BoxCollider box: return CollidesWith(box);
			case CircleCollider circle: return CollidesWith(circle);
			default: return false;
		}
	}

	protected abstract bool CollidesWith(BoxCollider box);
	protected abstract bool CollidesWith(CircleCollider circle);
	protected abstract bool CollidesWith(Vector2 vec);
}
