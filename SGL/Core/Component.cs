using System;

namespace Sgl;

public abstract class Component : IDisposable
{
	public Entity Entity;

	public bool Awaked;
	public bool IsActive;
	public bool IsDrawable;
	public bool Disposed { get; protected set; }

	public Component()
	{
	}

	public virtual void Awake()
	{
	}

	public virtual void Start()
	{
	}

	public virtual void Update(DeltaTime dt)
	{
	}

	public virtual void Draw()
	{
	}

	public virtual void Added()
	{
		IsActive = true;
	}

	public virtual void Removed()
	{
		IsActive = false;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposable) { }
}
