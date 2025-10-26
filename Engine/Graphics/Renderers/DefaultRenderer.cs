namespace Sgl;

public sealed class DefaultRenderer : Renderer
{
	public DefaultRenderer() : base()
	{
	}

	public override void PreDraw(Scene scene)
	{ }

	public override void Draw(Scene scene)
	{
		Core.SpriteBatch.Begin(samplerState: SamplerState, transformMatrix: scene.Camera.Matrix);

			foreach(var obj in scene.Entities)
			{
				if(obj.IsDrawable) { obj.Draw(); obj.Components.Draw(); }
			}

		Core.SpriteBatch.End();
	}

	public override void PosDraw(Scene scene)
	{ }
}
