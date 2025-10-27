using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public sealed class SpriteAtlas
{
	public Texture2D Texture;

	public List<Sprite> Sprites;

	public Sprite this[int index]
	{
		get
		{
			if(index < 0 || index >= Sprites.Count) throw new IndexOutOfRangeException("Index out of range in SpriteAtlas");
			return Sprites[index];
		}
	}

	public SpriteAtlas(Texture2D texture)
	{
		Texture = texture;
		Sprites = new();
	}

	public SpriteAtlas(Texture2D texture, int sw, int sh)
	{
		Texture = texture;
		Sprites = new();

		int width = Texture.Bounds.Width / sw;
		int height = Texture.Bounds.Height / sh;

		for(int y=0; y<height; y++)
		{
			for(int x=0; x<width; x++)
			{
				Sprites[x + y * width] = new Sprite(Texture, new Rectangle(x * sw, y * sh, sw, sh));
			}
		}
	}

	public List<Sprite> GetFrames(int start, int end)
	{
		List<Sprite> sprites = new();
		for(int i=start; i<=end; i++)
		{
			sprites.Add(Sprites[i]);
		}

		return sprites;
	}

	public override string ToString() => $"{Texture}, {Sprites.Count}";
}
