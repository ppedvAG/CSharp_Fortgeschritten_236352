namespace DelegatesEvents;

internal class User
{
	static void Main(string[] args)
	{
		Component comp = new();
		comp.ProcessStarted += Comp_ProcessStarted;
		comp.ProcessEnded += Comp_ProcessEnded;
		comp.Progress += Comp_Progress;
		comp.Progress += Comp_Progress1; //Reihenfolge ist wichtig
		comp.DoWork();
	}

	private static void Comp_ProcessStarted()
	{
        Console.WriteLine("Prozess gestartet");
	}

	private static void Comp_ProcessEnded()
	{
		Console.WriteLine("Prozess beendet");
	}

	private static void Comp_Progress(int obj)
	{
		Console.WriteLine("Fortschritt: " + obj);
	}

	private static void Comp_Progress1(int obj)
	{
		Console.WriteLine(obj);
	}
}
