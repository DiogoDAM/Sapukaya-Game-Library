using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl.Tiled;

public class SimpleTileDraw
{
	public int[] Data;
	public TextureTiles Texture;
	public int TileWidth;
	public int TileHeight;
	public int Width;
	public int Height;
	public Vector2 Position = Vector2.Zero;

	public Scene Scene;
	public SamplerState SamplerState = SamplerState.PointWrap;

	public SimpleTileDraw(int[] data, TextureTiles texture, int tw, int th, int w, int h)
	{
		Data = data;
		Texture = texture;
		TileWidth = tw;
		TileHeight = th;
		Width = w;
		Height = h;
	}

	public void Draw()
	{
		Core.SpriteBatch.Begin(samplerState: SamplerState, transformMatrix: Scene.Camera.Matrix);

		for(int y = 0; y < Height; y++)
		{
			for(int x = 0; x < Width; x++)
			{
				int value = Data[x + y * Width];
				if(value != 0)
				{
					Core.SpriteBatch.Draw(Texture.Texture, new Vector2(Position.X + (x * TileWidth), Position.Y + (y * TileHeight)), Texture.Tiles[value], Color.White);
				}
			}
		}

		Core.SpriteBatch.End();
	}
}
