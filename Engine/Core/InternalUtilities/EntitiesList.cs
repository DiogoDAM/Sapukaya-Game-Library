using System.Collections.Generic;
using System.Collections;
using System;

namespace Sgl.InternalUtilities;

public sealed class EntitiesList : IEnumerable<Entity>, IDisposable
{
	private List<Entity> m_entities;
	private HashSet<Entity> m_toAdd;
	private HashSet<Entity> m_toRemove;

	public Scene Scene;

	public int Count => m_entities.Count + m_toAdd.Count;

	public EntitiesList(Scene s)
	{
		m_entities = new();
		m_toAdd = new();
		m_toRemove = new();

		Scene = s;
	}

	public void Add(Entity c)
	{
		m_toAdd.Add(c);
	}

	public bool Remove(Entity c) 
	{
		if(m_toAdd.Contains(c)) return m_toAdd.Remove(c);

		if(m_entities.Contains(c)) 
		{
			m_toRemove.Add(c);
			return true;
		}

		return false;
	}

	public bool Contains(Entity c) 
	{
		return m_toAdd.Contains(c) || m_entities.Contains(c);
	}

	public Entity Find(Predicate<Entity> predicate)
	{
		return m_entities.Find(predicate);
	}

	public List<Entity> FindAll(Predicate<Entity> predicate)
	{
		return m_entities.FindAll(predicate);
	}

	public void Start()
	{
		foreach(var c in m_entities)
		{
			if(c.Awaked && c.IsActive) { c.Start(); c.Components.Start(); }
		}
	}


	public void Update(DeltaTime dt)
	{
		foreach(var c in m_entities)
		{
			if(c.Awaked && c.IsActive) { c.Update(dt); c.Components.Update(dt); }
		}
	}


	public void UpdateList()
	{
		if(m_toAdd.Count > 0)
		{
			foreach(var c in m_toAdd)
			{
				m_entities.Add(c);
				c.Added();
				c.Scene = Scene;

				c.Awake();
				c.Awaked = true;
			}
			m_toAdd.Clear();
		}


		if(m_toRemove.Count > 0)
		{
			foreach(var c in m_toRemove)
			{
				m_entities.Remove(c);
				c.Removed();
				c.Scene = null;
			}
			m_toRemove.Clear();
		}

	}

	public void Clear()
	{
		if(m_toAdd.Count > 0) m_toAdd.Clear();
		if(m_toRemove.Count > 0) m_toRemove.Clear();

		foreach(var c in m_entities)
		{
			c.Removed();
			c.Scene = null;
		}
		m_entities.Clear();
	}

    public IEnumerator<Entity> GetEnumerator()
    {
		return m_entities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
		return GetEnumerator();
    }

    public void Dispose()
    {
		foreach(var c in m_entities)
		{
			c.Dispose();
		}

		Clear();

		GC.SuppressFinalize(this);
    }
}
