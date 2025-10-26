using Microsoft.Xna.Framework.Input;

namespace Sgl;

public class KeyboardManager
{
	private KeyboardState _curr;
	private KeyboardState _prev;

	public KeyboardManager()
	{
		_curr = Keyboard.GetState();
		_prev = Keyboard.GetState();
	}

	public void Update()
	{
		_prev = _curr;
		_curr = Keyboard.GetState();
	}

	public bool IsKeyDown(Keys key)
	{
		return _curr.IsKeyDown(key);
	}

	public bool IsKeyUp(Keys key)
	{
		return _curr.IsKeyUp(key);
	}

	public bool WasKeyPressed(Keys key)
	{
		return _curr.IsKeyDown(key) && _prev.IsKeyUp(key);
	}

	public bool WasKeyReleased(Keys key)
	{
		return _curr.IsKeyUp(key) && _prev.IsKeyDown(key);
	}
}
