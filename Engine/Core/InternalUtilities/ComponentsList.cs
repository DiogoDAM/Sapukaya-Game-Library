using System.Collections.Generic;
using System.Collections;
using System;

namespace Sgl.InternalUtilities;

public sealed class ComponentsList : IEnumerable<Component>, IDisposable
{
	private List<Component> m_components;
	private HashSet<Component> m_toAdd;
	private HashSet<Component> m_toRemove;

	public Entity Entity;

	public int Count => m_components.Count + m_toAdd.Count;

	public ComponentsList(Entity e)
	{
		m_components = new();
		m_toAdd = new();
		m_toRemove = new();

		Entity = e;
	}

	public void Add(Component c)
	{
		m_toAdd.Add(c);
	}

	public bool Remove(Component c) 
	{
		if(m_toAdd.Contains(c)) return m_toAdd.Remove(c);

		if(m_components.Contains(c)) 
		{
			m_toRemove.Add(c);
			return true;
		}

		return false;
	}

	public bool Contains(Component c) 
	{
		return m_toAdd.Contains(c) || m_components.Contains(c);
	}

	public Component Find(Predicate<Component> predicate)
	{
		return m_components.Find(predicate);
	}

	public List<Component> FindAll(Predicate<Component> predicate)
	{
		return m_components.FindAll(predicate);
	}

	public void Start()
	{
		UpdateList();

		foreach(var c in m_components)
		{
			if(c.Awaked && c.IsActive) c.Start();
		}
	}


	public void Update(DeltaTime dt)
	{
		foreach(var c in m_components)
		{
			if(c.Awaked && c.IsActive) c.Update(dt);
		}

		UpdateList();
	}

	public void Draw()
	{
		foreach(var c in m_components)
		{
			if(c.Awaked && c.IsDrawable) c.Draw();
		}
	}

	private void UpdateList()
	{
		if(m_toAdd.Count > 0)
		{
			foreach(var c in m_toAdd)
			{
				m_components.Add(c);
				c.Added();
				c.Entity = Entity;

				c.Awake();
				c.Awaked = true;
			}
			m_toAdd.Clear();
		}

		if(m_toRemove.Count > 0)
		{
			foreach(var c in m_toRemove)
			{
				m_components.Remove(c);
				c.Removed();
				c.Entity = null;

			}
			m_toRemove.Clear();
		}
	}

	public void Clear()
	{
		if(m_toAdd.Count > 0) m_toAdd.Clear();
		if(m_toRemove.Count > 0) m_toRemove.Clear();

		foreach(var c in m_components)
		{
			c.Removed();
			c.Entity = null;
		}
		m_components.Clear();
	}

    public IEnumerator<Component> GetEnumerator()
    {
		return m_components.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
		return GetEnumerator();
    }

    public void Dispose()
    {
		foreach(var c in m_components)
		{
			c.Dispose();
		}

		Clear();

		GC.SuppressFinalize(this);
    }
}
