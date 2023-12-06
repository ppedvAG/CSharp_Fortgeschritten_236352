using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Tasse();
		//Kaffee();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//sw.Restart();
		//Task t1 = Task.Run(Toast);
		//Task t2 = Task.Run(Tasse).ContinueWith(_ => Kaffee());
		//Task.WaitAll(t1, t2); //Task.WaitAll blockiert -> schlecht
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		//Async und await
		//await blockiert nicht den Main Thread, weil im Hintergrund ein Task.Run, ContinueWith und Callback Konstrukt aufgebaut wird

		//Wenn eine Async Methode gestartet wird, die einen Task zurückgibt, wird diese asynchron gestartet

		//sw.Restart();
		//ToastAsync();
		//TasseAsync();
		//KaffeeAsync();
		//Console.WriteLine(sw.ElapsedMilliseconds); //19ms

		//await: Warte hier, das die gegebene Aufgabe fertig wird
		//sw.Restart();
		//await ToastAsync(); //Starte und warte auf den Toast
		//await TasseAsync(); //Starte und warte auf die Tasse
		//await KaffeeAsync(); //Starte und warte auf den Kaffee
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		////////////////////////////////////////////////////////////////////////////////////////////////////////

		//sw.Restart();
		//Task toast = ToastAsync(); //Starte den Toast
		//Task tasse = TasseAsync(); //Starte die Tasse
		//await tasse; //Warte darauf, das die Tasse fertig wird
		//Task kaffee = KaffeeAsync(); //Starte den Kaffee
		//await kaffee; //Warte darauf, das der Kaffee fertig wird
		//await toast; //Warte darauf, das der Toast fertig wird
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s
		////Starte Toast, Starte Tasse, Warte auf Tasse, Starte Kaffee, Warte auf Kaffee und Toast

		//Vereinfachen

		//sw.Restart();
		//Task toast = ToastAsync(); //Starte den Toast
		//await TasseAsync(); //Starte und Warte auf die Tasse
		//await KaffeeAsync(); //Starte und Warte auf den Kaffee
		//await toast; //Warte darauf, das der Toast fertig wird
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s
		//Starte Toast, Starte Tasse, Warte auf Tasse, Starte Kaffee, Warte auf Kaffee und Toast

		////////////////////////////////////////////////////////////////////////////////////////////////////////

		//Diese drei Methoden sollen jetzt Objekte zurückgeben
		//sw.Restart();
		//Task<Toast> toastTask = ToastObjectAsync();
		//Task<Tasse> tasseTask = TasseObjectAsync();
		//Tasse tasse = await tasseTask; //Warte darauf, das dieser Task fertig wird und gib das Objekt zurück
		//Task<Kaffee> kaffeeTask = KaffeeObjectAsync(tasse);
		//Kaffee kaffee = await kaffeeTask; //Hier kommt jetzt das Objekt hinter dem Task heraus
		//Toast toast = await toastTask;
		//Fruehstueck f = new Fruehstueck(toast, kaffee);
		//Console.WriteLine(sw.ElapsedMilliseconds); //4s

		//Vereinfachen
		sw.Restart();
		Fruehstueck f = new Fruehstueck(await ToastObjectAsync(), await KaffeeObjectAsync(await TasseObjectAsync()));
		Console.WriteLine(sw.ElapsedMilliseconds); //4s

		////////////////////////////////////////////////////////////////////////////////////////////////////////

		//Unterschiede zwischen Rückgabewerten in Async Methoden
		//async void: Innerhalb der Methode kann await verwendet werden, aber die Methode selbst kann nicht awaited werden
		//async Task: Innerhalb der Methode kann await verwendet werden und die Methode kann awaited werden
		//async Task<T>: Innerhalb der Methode kann await verwendet werden und die Methode kann awaited werden. Diese Methode gibt ein Objekt als Ergebnis zurück

		//Task.Run
		//Task.Run ist awaitable, damit können alle Codeteile asynchron gemacht werden
		for (int i = 0; i < (int) 1E9; i++) //Dauert
			Console.WriteLine(i);

		Task t = Task.Run(() => 
		{
			for (int i = 0; i < (int) 1E9; i++)
				Console.WriteLine(i);
		});
		//Zwischenschritte
		await t;

		//Task.WhenAll & Task.WhenAny
		//Mehrere Tasks awaiten kann nicht mit einem einzelnen await durchgeführt werden
		Task t1 = null, t2 = null, t3 = null;
		//await t1, t2, t3; //Geht nicht

		await Task.WhenAll(t1, t2, t3);

		//Unbestimmte Anzahl von Tasks awaiten
		Task[] tasks = new Task[10];
		await Task.WhenAll(tasks);

		//Unbestimmte Anzahl von Tasks mit Ergbenis awaiten
		Task<int>[] intTasks = new Task<int>[10];
		int[] x = await Task.WhenAll(intTasks); //Ergebnis ist in dem Array enthalten

		//Aufbau
		//Task starten: Task Variable anlegen und Async Methode starten
		//(optional) Zwischenschritte
		//Auf das Ergebnis warten mit await auf den vorher gestarteten Task
	}

	#region Synchron
	static void Toast()
	{
		Thread.Sleep(4000);
        Console.WriteLine("Toast fertig");
    }

	static void Tasse()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Tasse fertig");
	}

	static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}
	#endregion

	#region Asynchron
	static async Task ToastAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		//Hier wird kein return benötigt
	}

	static async Task TasseAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		//Hier wird kein return benötigt
	}

	static async Task KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		//Hier wird kein return benötigt
	}
	#endregion

	#region Asynchron mit Ergebnis
	static async Task<Toast> ToastObjectAsync() //Hier wird der Rückgabewert über das Generic beim Task angegeben
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast(); //Hier wird ein Rückgabewert vom Typen des Generics benötigt, kein Task
	}

	static async Task<Tasse> TasseObjectAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	static async Task<Kaffee> KaffeeObjectAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee(t);
	}
	#endregion
}

public class Toast { }

public class Tasse { }

public record Kaffee(Tasse t);

public record Fruehstueck(Toast t, Kaffee k);