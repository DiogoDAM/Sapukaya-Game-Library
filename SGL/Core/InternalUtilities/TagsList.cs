using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Sgl.InternalUtilities;

public sealed class TagsList : IDisposable
{
	private Dictionary<int, List<Entity>> m_tags;

	public TagsList()
	{
		m_tags = new();
	}

	public void AddTag(int tag)
	{
		if(!m_tags.ContainsKey(tag)) m_tags.Add(tag, new());
	}

	public void RemoveTag(int tag)
	{
		if(m_tags.ContainsKey(tag)) m_tags.Remove(tag);
	}

	public bool ContainsTag(int tag)
	{
		return m_tags.ContainsKey(tag);
	}

	public void AddEntity(Entity e)
	{
		if(!m_tags.ContainsKey(e.Tag)) m_tags.Add(e.Tag, new());

		m_tags[e.Tag].Add(e);
	}

	public bool RemoveEntity(Entity e)
	{
		if(!m_tags.ContainsKey(e.Tag)) return true;

		return m_tags[e.Tag].Remove(e);
	}

	public bool ContainsEntity(Entity e)
	{
		if(!m_tags.ContainsKey(e.Tag)) return false;

		return m_tags[e.Tag].Contains(e);
	}

	public List<Entity> GetTag(int tag)
	{
		if(!m_tags.ContainsKey(tag)) return null;

		return m_tags[tag];
	}

	public Entity GetEntity(Entity e)
	{
		if(!m_tags.ContainsKey(e.Tag)) return null;

		return m_tags[e.Tag].Find(ce => ce.Equals(e));
	}

	public T GetEntityType<T>(int tag) where T : Entity
	{
		if(!m_tags.ContainsKey(tag)) return null;

		return (T)m_tags[tag].Find(ce => ce is T);
	}

	public List<T> GetAllEntitiesType<T>(int tag) where T : Entity
	{
		if(!m_tags.ContainsKey(tag)) return null;

		return m_tags[tag].FindAll(ce => ce is T).OfType<T>().ToList<T>();
	}

	public void Dispose()
	{
		foreach(var pair in m_tags)
		{
			pair.Value.Clear();
		}
		m_tags.Clear();

		GC.SuppressFinalize(this);
	}
}
