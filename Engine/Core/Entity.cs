using Microsoft.Xna.Framework;

using System;
using System.Linq;

using Sgl.InternalUtilities;
using System.Collections.Generic;

namespace Sgl;

public class Entity : IDisposable
{
	public bool Awaked;
	public bool IsActive;
	public bool IsDrawable;
	public bool Disposed { get; protected set; }

	public ComponentsList Components;

	public Transform2D Transform;

	public Scene Scene;

	public int Tag = 0;

	public Entity()
	{
		Components = new(this);
		Transform = new();
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
		IsDrawable = true;
	}

	public virtual void Removed()
	{
		IsActive = false;
		IsDrawable = false;
	}



	//Components Methods 
	public void AddComponent(Component c) 
	{
		Components.Add(c);
	}

	public bool RemoveComponent(Component c) 
	{
		return Components.Remove(c);
	}

	public bool ContainsComponent(Component c) 
	{
		return Components.Contains(c);
	}

	public Component FindComponent(Predicate<Component> predicate)
	{
		return Components.Find(predicate);
	}

	public T CreateComponent<T>() where T : Component, new()
	{
		T c = new();

		Components.Add(c);

		return c;
	}

	public T GetComponent<T>() where T : Component 
	{
		return (T)Components.Find(c => c is T);
	}

	public List<T> GetAllComponents<T>() where T : Component 
	{
		return Components.FindAll(c => c is T).OfType<T>().ToList<T>();
	}




	//Scene Methods
	public T Instantiate<T>() where T : Entity, new()
	{
		T e = new();

		Scene.AddEntity(e);

		return e;
	}

	public T Instantiate<T>(Vector2 pos) where T : Entity, new()
	{
		T e = new();

		Scene.AddEntity(e);
		e.Transform.Position = pos;

		return e;
	}

	public T Instantiate<T>(Transform2D transformParent) where T : Entity, new()
	{
		T e = new();

		Scene.AddEntity(e);
		e.Transform.Parent = transformParent;

		return e;
	}

	public void Destroy(Entity e)
	{
		Scene.RemoveEntity(e);
		Dispose();
	}

	public void AddToScene(Entity e)
	{
		Scene.AddEntity(e);
	}

	public bool RemoveFromScene(Entity e)
	{
		return Scene.RemoveEntity(e);
	}

	public T GetFromScene<T>() where T : Entity 
	{
		return Scene.GetEntity<T>();
	}

	public List<T> GetAllFromScene<T>() where T : Entity 
	{
		return Scene.GetAllEntities<T>();
	}



	public void Dispose()
	{
		GC.SuppressFinalize(this);
		Dispose(true);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Components.Dispose();
				Components.Entity = null;
				Disposed = true;
			}
		}
	}
}
