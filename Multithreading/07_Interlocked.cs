namespace Multithreading;

internal class _07_Interlocked
{
	static int Zaehler;

	static void Main(string[] args)
	{
		for (int i = 0; i < 100; i++)
			new Thread(Run).Start();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Interlocked.Increment(ref Zaehler); //Interlocked lockt hier selbst
		}
	}
}
