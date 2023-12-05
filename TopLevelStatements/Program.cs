using System.Collections.Concurrent;

Console.WriteLine(args);

public class ConcurrentHashSet<T>
{
	private readonly ConcurrentDictionary<T, object> dict = new();

	public void Add(T obj)
	{
		dict.TryAdd(obj, null);
	}

	public bool Contains(T obj)
	{
		return dict.ContainsKey(obj);
	}
}