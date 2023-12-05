namespace Multitasking;

internal class _04_TaskExceptions
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new();
		CancellationToken ct = cts.Token;

		Task t = new Task(Run, ct);
		t.Start();

		Thread.Sleep(500);

		cts.Cancel();

		//AggregateException: Sammelexception, die beliebig viele Exception Objekte halten kann
		//Nicht logisch bei einem Task, sinnvoll bei Task.WaitAll (mehreren Tasks)
		//Existiert bei Wait, WaitAll, Result
		try
		{
			t.Wait();
		}
		catch (AggregateException e)
		{
			foreach (Exception ex in e.InnerExceptions) //in e.InnerExceptions sind die einzelnen Exception Objekte enthalten
			{
                Console.WriteLine(ex.Message);
            }
		}

		//WaitAll AggregateException
		List<Task> tasks = new List<Task>();
		for (int i = 0; i < 100; i++)
			tasks.Add(Task.Run(ThrowRandom));

		try
		{
			//Hier kommen alle Exceptions gleichzeitig
			//Vielleicht sollen die Fehler live auftreten
			Task.WaitAll(tasks.ToArray());
		}
		catch (AggregateException ex)
		{
            Console.WriteLine(ex.Message);
            //foreach (Exception e in ex.InnerExceptions)
            //             Console.WriteLine(e.Message);

        }

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Console.WriteLine($"Side Task: {i}");
				ct.ThrowIfCancellationRequested(); //Task wirft Exception, ist hier aber nicht sichtbar
			}
		}
	}

	static void ThrowRandom()
	{
		int x = Random.Shared.Next(100, 500);
		Thread.Sleep(x);
		throw new InvalidOperationException();
	}
}
