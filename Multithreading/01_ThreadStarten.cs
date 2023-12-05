namespace Multithreading;

internal class _01_ThreadStarten
{
	static void Main(string[] args)
	{
		Thread t1 = new Thread(Run);
		t1.Start();

		Thread t2 = new Thread(Run);
		t2.Start();

		Thread t3 = new Thread(Run);
		t3.Start();

		//Ab hier parallel

		//Auf Threads warten
		t1.Join(); //Hier auf t1 warten, t2 und t3 laufen weiter
		t2.Join(); //Hier auf t2 warten, t3 läuft weiter
		t3.Join(); //Hier auf t3 warten

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Side Thread: {i}");
    }
}