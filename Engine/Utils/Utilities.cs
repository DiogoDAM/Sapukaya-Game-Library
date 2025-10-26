using System;
using Microsoft.Xna.Framework;

namespace  Sgl
{
	public static class Utilities
	{
		public static Random Random = new Random();

		public static float RandomFloat(float min, float max)
		{
			return (float)(Random.NextDouble() * (max - min) + min);
		}

		//Cartesian methods Screen and map

		public static Vector2 MapToScreen(int mapx, int mapy, int tileWidth, int tileHeight)
		{
			return new Vector2(mapx * tileWidth, mapy * tileHeight);
		}

		public static Vector2 ScreenToMap(Vector2 ScreenPos, int tileWidth, int tileHeight)
		{
			return new Vector2((float)Math.Floor(ScreenPos.X / tileWidth), (float)Math.Floor(ScreenPos.Y / tileHeight));
		}

	}
}
