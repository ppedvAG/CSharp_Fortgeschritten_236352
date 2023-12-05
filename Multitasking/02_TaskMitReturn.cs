namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = new Task<int>(Berechne);
		t.Start();

		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Main Thread: {i}");

        Console.WriteLine(t.Result);
		//t.Result wartet auf das Beenden des Tasks
		//Problem: Blockieren des Main Threads
		//2 Lösungen: ContinueWith, async/await

		//Auf Tasks warten
		//Problem: Blockieren des Main Threads
		t.Wait(); //Auf den gegebenen Task warten
		Task.WaitAll(t, t, t); //NEU: Auf mehrere Tasks warten
		Task.WaitAny(t, t, t); //NEU: Auf einen der gegebenen Tasks warten

        Console.ReadKey();
	}

	static int Berechne()
	{
		Thread.Sleep(500);
		return Random.Shared.Next();
	}
}
