namespace Multitasking;

internal class _03_CancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new();
		CancellationToken ct = cts.Token;

		Task t = new Task(Run, ct);
		t.Start();

		Thread.Sleep(500);

		cts.Cancel();

		Thread.Sleep(100);

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
}
