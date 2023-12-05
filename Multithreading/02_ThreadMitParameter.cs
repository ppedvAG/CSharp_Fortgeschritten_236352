namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		Thread t = new Thread(Run);
		t.Start(200);

		object returnValue;
		Thread t2 = new Thread(RunWithCallback);
		t2.Start((object ret) => returnValue = ret); //Func mit Parameter (Action<object>)

		//Callback
		//Funktionsaufruf der am Ende einer Aufgabe gemacht wird, aber der User kann selbst entscheiden (über Delegate) was am Ende der Aufgabe passieren soll
		//Bei Threads kann als Parameter eine Action übergeben werden, um am Ende der Aufgabe eine Antwort zurückzusenden

		Thread t3 = new Thread(RunWithCallbackAndParameter);
		t3.Start((200, new Action<object>((object ret) => returnValue = ret)));
		t3.Start(new ThreadData(200, ret => returnValue = ret));

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	static void Run(object o)
	{
		if (o is int x)
		{
			for (int i = 0; i < x; i++)
				Console.WriteLine($"Side Thread: {i}");
		}
	}

	static void RunWithCallback(object o)
	{
		if (o is Delegate callback) //Prüft, ob o eine Action ist
		{
			Thread.Sleep(200);
			callback.DynamicInvoke(Random.Shared.Next()); //Führt die gegebene Action aus mit dem Ergebnis als Parameter
		}
	}

	static void RunWithCallbackAndParameter(object o)
	{
		if (o is Tuple<object, Delegate> data) //Prüft, ob o eine Action ist
		{
			Thread.Sleep((int) data.Item1);
			data.Item2.DynamicInvoke(Random.Shared.Next()); //Führt die gegebene Action aus mit dem Ergebnis als Parameter
		}
	}

	private record ThreadData(int x, Action<object> callback);
}