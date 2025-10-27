using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System;
using System.Linq;
using Sgl.InternalUtilities;

namespace Sgl;

public class Scene : IDisposable
{
	public Color BgColor = Color.CornflowerBlue;

	public Camera2D Camera;

	public EntitiesList Entities;
	public RenderersList Renderers;
	public TagsList Tags;

	public bool IsActive;
	public bool IsDrawable;

	public uint Id;

	public int EntitiesCount => Entities.Count;

	public Scene()
	{
		Entities = new(this);
		Renderers = new(this);
		Tags = new();

		Camera = new(Core.ViewWidth, Core.ViewHeight);
	}



	//Entities
	public void AddEntity(Entity e)
	{
		Entities.Add(e);

		if(e.Tag != 0) Tags.AddEntity(e);
	}

	public bool RemoveEntity(Entity e)
	{
		if(e.Tag != 0) Tags.RemoveEntity(e);

		return Entities.Remove(e);
	}

	public bool ContainsEntity(Entity e)
	{
		return Entities.Contains(e);
	}

	public Entity FindEntity(Predicate<Entity> predicate)
	{
		return Entities.Find(predicate);
	}

	public List<Entity> FindAllEntities(Predicate<Entity> predicate)
	{
		return Entities.FindAll(predicate);
	}

	public T GetEntity<T>() where T : Entity
	{
		return (T)Entities.Find(e => e is T);
	}

	public List<T> GetAllEntities<T>() where T : Entity
	{
		return Entities.FindAll(e => e is T).OfType<T>().ToList<T>();
	}


	//Renderers
	public void AddRenderer(Renderer renderer)
	{
		Renderers.Add(renderer);
	}

	public bool RemoveRenderer(Renderer renderer)
	{
		return Renderers.Remove(renderer);
	}

	public bool ContainsRenderer(Renderer renderer)
	{
		return Renderers.Contains(renderer);
	}


	//Tags 
	public bool ContainsEntityInTag(Entity e)
	{
		return Tags.ContainsEntity(e);
	}

	public T GetEntityByTag<T>(int tag) where T : Entity
	{
		return Tags.GetEntityType<T>(tag);
	}

	public List<T> GetAllEntitiesByTag<T>(int tag) where T : Entity
	{
		return Tags.GetAllEntitiesType<T>(tag);
	}


	//Behaviour Methods
	public virtual void Awake()
	{
	}

	public virtual void Start()
	{
		Entities.UpdateList();
		Renderers.UpdateList();

		Entities.Start();
	}

	public virtual void PreUpdate(DeltaTime dt)
	{
		if(!IsActive) return;
	}

	public virtual void Update(DeltaTime dt)
	{
		if(!IsActive) return;
		Entities.Update(dt);
	}

	public virtual void PosUpdate(DeltaTime dt)
	{
		if(!IsActive) return;
		Entities.UpdateList();
		Renderers.UpdateList();
	}

	public virtual void PreDraw()
	{
		Core.GraphicsDevice.Clear(BgColor);

		if(!IsDrawable) return;

		Renderers.PreDraw();
	}

	public virtual void Draw()
	{
		if(!IsDrawable) return;
		Renderers.Draw();
	}

	public virtual void PosDraw()
	{
		if(!IsDrawable) return;
		Renderers.PosDraw();
	}


	public virtual void Active()
	{
		IsActive = true;
		IsDrawable = true;
	}

	public virtual void Desactive()
	{
		IsActive = false;
		IsDrawable = false;
	}


	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable)
		{
			Entities.Dispose();
			Renderers.Dispose();
			Tags.Dispose();

			Camera = null;
		}
	}
}
