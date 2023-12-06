using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncAwaitWPF;

internal class AsyncDataSource
{
	public async IAsyncEnumerable<int> GetNumbers()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next(100, 1000));
			yield return Random.Shared.Next();

			//yield return: Gib einen Wert zurück, aber beende nicht die Funktion
			//Benötigt immer einen IEnumerable Äquivalenten Typen als Rückgabetyp

			//Ohne yield return:
			//List<int> ints = new();
			//ints.Add(<Zahl>);
			//return ints;
		}
	}
}
