namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		//ContinueWith: Taskbäume anlegen mithilfe von Folgetasks
		//Auf einen Task können beliebig viele weitere Tasks angehängt werden, um Ketten zu erzeugen
		//Verwendung: Umgehung der Blockade des Main Threads, Fehlerbehandlung in separaten Zweigen, async/await, ...

		Task<int> t = new Task<int>(Berechne);
		t.ContinueWith(vorherigerTask => Console.WriteLine(t.Result)); //ContinueWith startet einen Task, der Zugriff auf den vorherigen Task hat
		//Konfiguration des Tasks zu Ende
		t.Start();

		//Effekt: Task läuft im Hintergrund, die Ergebnisauswertung läuft auch im Hintergrund

		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
            Console.WriteLine($"Main Thread: {i}");
        }

		//Ergebnis wird berechnet, Main Thread läuft ununterbrochen weiter


		//ContinuationOptions
		Task<int> t2 = new Task<int>(BerechneException);
		t2.ContinueWith(x => Console.WriteLine("Task fertig")); //Keine Options: Wird immer ausgeführt
		t2.ContinueWith(x => Console.WriteLine(t2.Result), TaskContinuationOptions.OnlyOnRanToCompletion); //Erfolgstask
		t2.ContinueWith(x => Console.WriteLine(t2.Exception.Message), TaskContinuationOptions.OnlyOnFaulted); //Exceptiontask
		t2.Start();

		Console.ReadKey();
	}

	static int Berechne()
	{
		Thread.Sleep(1500);
		return Random.Shared.Next();
	}

	static int BerechneException()
	{
		Thread.Sleep(1500);
		//throw new DivideByZeroException();
		return 0;
	}
}
