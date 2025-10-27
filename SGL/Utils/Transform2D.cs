using Microsoft.Xna.Framework;

using System;

namespace Sgl;

public sealed class Transform2D
{
	public Vector2 LocalPosition;
	public Vector2 LocalScale;
	public float LocalRotation;

	public Transform2D Parent { get; set; }

	public Vector2 Position 
	{
		get => Parent == null ? LocalPosition : LocalPosition + Parent.Position;
		set => LocalPosition = value;
	}

	public Vector2 Scale 
	{
		get => Parent == null ? LocalScale : LocalScale * Parent.Scale;
		set => LocalScale = value;
	}

	public float Rotation 
	{
		get => Parent == null ? LocalRotation : LocalRotation * Parent.Rotation;
		set => LocalRotation = value;
	}

	public Transform2D()
	{
		LocalPosition = Vector2.Zero;
		LocalScale = Vector2.One;
		LocalRotation = 0f;
	}

	public Transform2D(Vector2 pos)
	{
		LocalPosition = pos;
		LocalScale = Vector2.One;
		LocalRotation = 0f;
	}

	public Transform2D(Vector2 pos, Vector2 scale, float rot)
	{
		LocalPosition = pos;
		LocalScale = scale;
		LocalRotation = rot;
	}

	public Transform2D(Transform2D transform)
	{
		LocalPosition = transform.LocalPosition;
		LocalScale = transform.LocalScale;
		LocalRotation = transform.LocalRotation;
	}

	public void Translate(Vector2 move)
	{
		LocalPosition += move;
	}

	public void ScaleBy(float value)
	{
		LocalScale *= value;
	}

	public static Vector2 MoveTowards(Vector2 start, Vector2 target, float vel)
	{
		Vector2 dir = target - start;

		if(dir.Length() <= vel) return new Vector2(start.Y + vel, start.Y + vel);

		dir.Normalize();

		return start + (dir * vel);
	}

	public void MoveTowards(Vector2 target, float vel)
	{
		LocalPosition = MoveTowards(LocalPosition, target, vel);
	}

	public void LookAt(Vector2 target)
	{
		Vector2 dir = target - Position;

		LocalRotation = (float)Math.Atan2(dir.Y, dir.X);
	}

	public override string ToString() => $"{Position}, {Rotation}, {Scale}";
}
