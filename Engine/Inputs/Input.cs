namespace Sgl;

public class Input
{
	public KeyboardManager Keyboard;
	public MouseManager Mouse;

	public Input()
	{
		Keyboard = new();
		Mouse = new();
	}

	public void Update(DeltaTime dt)
	{
		Keyboard.Update();
		Mouse.Update();
	}
}
