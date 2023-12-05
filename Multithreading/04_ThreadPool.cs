namespace Multithreading;

internal class _04_ThreadPool
{
	static void Main(string[] args)
	{
		ThreadPool.QueueUserWorkItem(Run);
		ThreadPool.QueueUserWorkItem(Run);
		ThreadPool.QueueUserWorkItem(Run);

		//Vordergrund- vs. Hintergrundthreads
		Thread t = new Thread(Run);
		t.Start(); //Vordergrundthread

		//Alle Items im ThreadPool werden abgebrochen, wenn alle Vordergrundthreads fertig sind

		Thread.Sleep(500); //Blockiert den Main Thread für 500ms
	}

	static void Run(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Side Thread: {i}");
		}
    }
}
