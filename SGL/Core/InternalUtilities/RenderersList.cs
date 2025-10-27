using System.Collections.Generic;
using System.Collections;
using System;

namespace Sgl.InternalUtilities;

public sealed class RenderersList : IEnumerable<Renderer>, IDisposable
{
	private List<Renderer> m_renderers;
	private HashSet<Renderer> m_toAdd;
	private HashSet<Renderer> m_toRemove;

	public Scene Scene;

	public int Count => m_renderers.Count + m_toAdd.Count;

	public RenderersList(Scene s)
	{
		m_renderers = new();
		m_toAdd = new();
		m_toRemove = new();

		Scene = s;
	}

	public void Add(Renderer c)
	{
		m_toAdd.Add(c);
	}

	public bool Remove(Renderer c) 
	{
		if(m_toAdd.Contains(c)) return m_toAdd.Remove(c);

		if(m_renderers.Contains(c)) 
		{
			m_toRemove.Add(c);
			return true;
		}

		return false;
	}

	public bool Contains(Renderer c) 
	{
		return m_toAdd.Contains(c) || m_renderers.Contains(c);
	}

	public Renderer Find(Predicate<Renderer> predicate)
	{
		return m_renderers.Find(predicate);
	}

	public List<Renderer> FindAll(Predicate<Renderer> predicate)
	{
		return m_renderers.FindAll(predicate);
	}

	public void PreDraw()
	{
		foreach(var renderer in m_renderers)
		{
			if(renderer.IsDrawable) renderer.PreDraw(Scene);
		}
	}

	public void Draw()
	{
		foreach(var renderer in m_renderers)
		{
			if(renderer.IsDrawable) renderer.Draw(Scene);
		}
	}

	public void PosDraw()
	{
		foreach(var renderer in m_renderers)
		{
			if(renderer.IsDrawable) renderer.PosDraw(Scene);
		}
	}

	public void UpdateList()
	{
		if(m_toAdd.Count > 0)
		{
			foreach(var c in m_toAdd)
			{
				m_renderers.Add(c);
			}
			m_toAdd.Clear();
		}

		if(m_toRemove.Count > 0)
		{
			foreach(var c in m_toRemove)
			{
				m_renderers.Remove(c);
			}
			m_toRemove.Clear();
		}
	}

	public void Clear()
	{
		if(m_toAdd.Count > 0) m_toAdd.Clear();
		if(m_toRemove.Count > 0) m_toRemove.Clear();

		m_renderers.Clear();
	}

    public IEnumerator<Renderer> GetEnumerator()
    {
		return m_renderers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
		return GetEnumerator();
    }

    public void Dispose()
    {
		Clear();

		GC.SuppressFinalize(this);
    }
}
