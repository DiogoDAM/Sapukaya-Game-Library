using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public class Camera2D
{
	public Transform2D Transform;

	public Viewport Viewport;

	public Matrix Matrix => Matrix.CreateTranslation(-Transform.Position.X, -Transform.Position.Y, 0f) *
		Matrix.CreateRotationZ(Transform.Rotation) *
		Matrix.CreateScale(Transform.Scale.X, Transform.Scale.Y, 1f);

	public Vector2 ViewScale => new Vector2((float)Core.WindowWidth / Viewport.Width,
			(float)Core.WindowHeight / Viewport.Height);

	public Matrix MatrixView => Matrix.CreateTranslation(-Transform.Position.X, -Transform.Position.Y, 0f) *
		Matrix.CreateRotationZ(Transform.Rotation) *
		Matrix.CreateScale(Transform.Scale.X * ViewScale.X, Transform.Scale.Y * ViewScale.Y, 1f);

	public Vector2 CursorPosition => Vector2.Transform(Core.Input.Mouse.CursorPosition, Matrix.Invert(Matrix));
	public Vector2 ViewCursorPosition => Vector2.Transform(Core.Input.Mouse.CursorPosition, Matrix.Invert(MatrixView));

	public Camera2D()
	{
		Transform = new();
		Viewport = new();
	}

	public Camera2D(int width, int height)
	{
		Transform = new();
		Viewport = new();
		Viewport.Width = width;
		Viewport.Height = height;
	}

	public Camera2D(Transform2D parent, int width, int height)
	{
		Transform = new();
		Transform.Parent = parent;
		Viewport = new();
		Viewport.Width = width;
		Viewport.Height = height;
	}

	public virtual void Update(DeltaTime dt) { }
}
