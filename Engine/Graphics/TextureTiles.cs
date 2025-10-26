using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

namespace Sgl;

public sealed class TextureTiles
{
	public int TileWidth;
	public int TileHeight;
	public int Width;
	public int Height;
	public int Columns;
	public int Rows;

	public Texture2D Texture;

	public Rectangle[] Tiles { get; private set; }

	public TextureTiles(Texture2D texture, int tw, int th, int w, int h)
	{
		Texture = texture;
		TileWidth = tw;
		TileHeight = th;
		Width = w;
		Height = h;
		Columns = tw / w;
		Rows = th / h;
		Tiles = new Rectangle[Columns * Rows];

		for(int y=0; y<Rows; y++)
		{
			for(int x=0; x<Columns; x++)
			{
				Tiles[x + y * Columns] = new Rectangle((x * TileWidth), (y * TileHeight), TileWidth, TileHeight);
			}
		}
	}

	public TextureTiles(string texturePath, int tw, int th, int w, int h)
	{
		Texture = Core.Content.Load<Texture2D>(texturePath);
		TileWidth = tw;
		TileHeight = th;
		Width = w;
		Height = h;
		Columns = tw / w;
		Rows = th / h;
		Tiles = new Rectangle[Columns * Rows];

		for(int y=0; y<Rows; y++)
		{
			for(int x=0; x<Columns; x++)
			{
				Tiles[x + y * Columns] = new Rectangle((x * TileWidth), (y * TileHeight), TileWidth, TileHeight);
			}
		}
	}
}
