namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		//CancellationToken
		//Sender und Empfänger
		//Sender: CancellationTokenSource
		//Empfänger: CancellationToken von der entsprechenden Source

		try
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			CancellationToken token = cts.Token; //Token ist ein Struct, Source kann beliebig viele Tokens erzeugen

			Thread t = new Thread(Run);
			t.Start(token); //Hier Token übergeben

			Thread.Sleep(500);

			cts.Cancel(); //Auf der Source das Cancel Signal senden
		}
		catch (OperationCanceledException)
		{
			//Exception kann nicht hier oben gefangen
		}
	}

	static void Run(object o)
	{
		try
		{
			//Hier muss das Cancel Signal verarbeitet werden
			if (o is CancellationToken token)
			{
				for (int i = 0; i < 100; i++)
				{
					Thread.Sleep(25);
					Console.WriteLine($"Side Thread: {i}");

					if (token.IsCancellationRequested) //Prüfen, ob das Signal erhalten wurde
					{
						Console.WriteLine("Thread wird abgebrochen"); //Etwas vor dem Canceln machen
						token.ThrowIfCancellationRequested();
					}
				}
			}
		}
		catch (OperationCanceledException)
		{
            Console.WriteLine("Exception im Thread gefangen");
        }
	}
}
