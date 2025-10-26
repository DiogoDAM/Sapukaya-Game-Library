using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sgl;

public sealed class TextureAtlas
{
	public Texture2D Texture { get; private set; }

	private Dictionary<string, TextureRegion> m_regions;
	private Dictionary<string, TextureRegion[]> m_frames;


	public TextureAtlas(string textureAtlasPath)
	{
		Texture = Core.Content.Load<Texture2D>(textureAtlasPath);

		m_regions = new();
		m_frames = new();
	}

	public TextureRegion GetRegion(string regionName)
	{
		if(!m_regions.ContainsKey(regionName)) throw new KeyNotFoundException($"The TextureAtlas don't contains the regions: {regionName}");
		return m_regions[regionName];
	}

	public TextureRegion[] GetFrames(string framesName)
	{
		if(!m_frames.ContainsKey(framesName)) throw new KeyNotFoundException($"The TextureAtlas don't contains the frames: {framesName}");
		return m_frames[framesName];
	}

	public void AddRegion(string frameName, int x, int y, int w, int h)
	{
		m_regions.Add(frameName, new(Texture, new Rectangle(x, y, w, h)));
	}

	public void AddFrames(string framesName, int x, int y, int w, int h, int countx, int county)
	{
		TextureRegion[] frames = new TextureRegion[countx * county];
		for(int row=0; row<county; row++)
		{
			for(int col=0; col<countx; col++)
			{
				frames[col + row * countx] = new(Texture, new Rectangle(x + (w * col), y + (h * row), w, h));
			}
		}

		m_frames.Add(framesName, frames);
	}

	public void AddRegion(string frameName, Rectangle frameBounds)
	{
		m_regions.Add(frameName, new(Texture, frameBounds));
	}

	public Sprite CreateSprite(string regionName)
	{
		if(!m_regions.ContainsKey(regionName)) throw new KeyNotFoundException($"The TextureAtlas don't contains the regions: {regionName}");

		Sprite sprite = new();
		sprite.Region = m_regions[regionName];

		return sprite;
	}
}
