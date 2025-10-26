using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public abstract class Renderer
{
	public SamplerState SamplerState;

	public bool IsDrawable;

	public Renderer()
	{
		SamplerState = SamplerState.PointWrap;

		IsDrawable = true;
	}

	public abstract void PreDraw(Scene scene);
	public abstract void Draw(Scene scene);
	public abstract void PosDraw(Scene scene);
}
