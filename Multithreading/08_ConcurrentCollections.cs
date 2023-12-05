using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ServiceModel;

namespace Multithreading;

internal class _08_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> ints = new ConcurrentBag<int>();
		ints.Add(3);
		ints.Add(6);
		ints.Add(10);
		//ints[0] //Index nicht möglich
		//Daten nur mit Linq angreifbar

		SynchronizedCollection<int> list = new();
		list.Add(3);
		list.Add(6);
		list.Add(10);
		Console.WriteLine(list[0]);

		ConcurrentDictionary<int, string> dict = new();
		dict.TryAdd(2, ""); //Füge hinzu, falls es funktioniert
		bool b = dict.TryGetValue(4, out string value); //Versuche, einen Wert herauszuholen, falls ein anderer Thread diesen nicht schon entfernt hat

		dict.AddOrUpdate(0, "", (key, value) => value);
		dict.GetOrAdd(0, (key) => "");
	}
}
