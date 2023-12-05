namespace Multithreading;

internal class _05_Lock
{
	static int Zaehler;

	static object LockObject = new();

	static void Main(string[] args)
	{
		for (int i = 0; i < 100; i++)
			new Thread(Run).Start();
	}

	static void Run()
	{
		//Problem: Threads wollen gleichzeitig auf die Variable greifen, dürfen aber nicht
		//-> Outputs sind gemischt, Zaehler könnte das Maximum nicht erreichen
		for (int i = 0; i < 100; i++)
		{
			//Lock: Definiert, das dieser Codeblock nur immer gleichzeitig von einem einzigen Thread ausgeführt werden kann
			//Alle Threads warten hier, bis der Thread der den Block gesperrt hat fertig ist und das Lock wieder freigibt
			lock (LockObject)
			{
				Zaehler++;
				Console.WriteLine(Zaehler);
			}

			//Monitor hat 1:1 denselben Code wie Lock
			Monitor.Enter(LockObject);
			Zaehler++;
			Console.WriteLine(Zaehler);
			Monitor.Exit(LockObject);
		}
	}
}
