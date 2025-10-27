using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Sgl;

public class Core : Game
{
	private static Core s_instance;
	public static Core Instance => s_instance;

	public static GraphicsDeviceManager Graphics { get; private set; }
	public static SpriteBatch SpriteBatch { get; private set; }
	public static new ContentManager Content { get; private set; }
	public static new GraphicsDevice GraphicsDevice { get; private set; }

	public static DeltaTime DeltaTime { get; protected set; }

	public static Scene CurrentScene;

	public static Input Input { get; private set; }

	public static int WindowWidth => Graphics.PreferredBackBufferWidth;
	public static int WindowHeight => Graphics.PreferredBackBufferHeight;

	public static int ViewWidth { get; private set; }
	public static int ViewHeight { get; private set; }

	public Core(int ww, int wh, string wt, bool isFullScreen) : base()
	{
		s_instance = this;

		Graphics = new(this);

		Content = base.Content;
		Content.RootDirectory = "Content";

		SetViewSize(ww, wh);
		SetWindowSize(ww, wh);

		Window.Title = wt;

		SetWindowFullScreen(isFullScreen);

		DeltaTime = new();

		Input = new();

		IsMouseVisible = true;
	}

	protected override void Initialize()
	{
		GraphicsDevice = base.GraphicsDevice;
		SpriteBatch = new(GraphicsDevice);

		base.Initialize();

		CurrentScene.Awake();

		CurrentScene.Start();
	}

	protected override void Update(GameTime gameTime)
	{
		base.Update(gameTime);

		DeltaTime.Time = (float)gameTime.ElapsedGameTime.TotalSeconds;

		Input.Update(DeltaTime);

		CurrentScene.PreUpdate(DeltaTime);
		CurrentScene.Update(DeltaTime);
		CurrentScene.PosUpdate(DeltaTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		base.Draw(gameTime);

		CurrentScene.PreDraw();
		CurrentScene.Draw();
		CurrentScene.PosDraw();
	}

	public static void SetViewSize(int w, int h)
	{
		ViewWidth = w;
		ViewHeight = h;
	}

	public static void SetWindowSize(int ww, int wh)
	{
		Graphics.PreferredBackBufferWidth = ww;
		Graphics.PreferredBackBufferHeight = wh;
		Graphics.ApplyChanges();
	}

	public static void SetWindowFullScreen(bool value) 
	{
		Graphics.IsFullScreen = value;
		Graphics.ApplyChanges();
	}

	public static void ToggleWindowFullScreen()
	{
		Graphics.ToggleFullScreen();
	}



	public static Scene CreateSceneWithDefaultRenderer(Color bgColor)
	{
		Scene scene = new();
		scene.BgColor = bgColor;
		scene.AddRenderer(new DefaultRenderer());

		return scene;
	}

	public static Scene CreateScene(Color bgColor, Renderer renderer)
	{
		Scene scene = new();
		scene.BgColor = bgColor;
		scene.AddRenderer(renderer);

		return scene;
	}
}
