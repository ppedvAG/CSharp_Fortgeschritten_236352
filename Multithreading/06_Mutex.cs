namespace Multithreading;

internal class _06_Mutex
{
	static void Main(string[] args)
	{
		Mutex m = null;
		Mutex.TryOpenExisting("Multithreading", out m);
		if (m != null)
		{
            Console.WriteLine("Applikation ist bereits gestartet");
			//Environment.Exit(0);
        }
		else
		{
            Console.WriteLine("Mutex geöffnet");
            m = new Mutex(true, "Multithreading");
		}
		Console.ReadLine();
		//m.ReleaseMutex(); //Mutex beim Beenden der Applikation wieder freigeben
	}
}
