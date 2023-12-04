namespace DelegatesEvents;

internal class Events
{
	static void Main(string[] args) => new Events().Start();

	//Event: Statischer Punkt (nicht static), an den Methoden angehängt werden können
	//Events bestehen immer aus einem Delegate und einem Namen
	//Events können nicht instanziert werden

	//Zweiseitige Programmierung: Entwicklerseite, Anwenderseite

	//Entwicklerseite
	//Definiert das Event
	//Gibt die Bedingungen vor, die für das Event gegegben sein müssen

	//Anwenderseite
	//Hängt an das Event den Code an, der ausgeführt werden soll

	//Beispiel: Click-Event
	//Entwicklerseite: event Delegate Click + Bedingungen (Maus im Button, Linksklick, keine UI-Elemente darüber, ...)
	//Anwenderseite: Click += Methode, void Methode(object sender, EventArgs args) { ... }

	/// <summary>
	/// Entwicklerseite
	/// EventHandler: Standard-Delegate, welches bei Events generell verwendet wird
	/// Besteht aus object sender und EventArgs e
	/// sender: Das Objekt, von dem das Event gekommen ist
	/// EventArgs: Die Daten des Events
	/// </summary>
	public event EventHandler TestEvent;

	public event EventHandler<TestEventArgs> ArgsEvent;

	public event EventHandler<int> IntEvent;

	public void Start()
	{
		TestEvent += Events_TestEvent; //Anwenderseite

		//if (...)
		TestEvent?.Invoke(this, EventArgs.Empty); //Entwicklerseite

		//Event mit Daten
		ArgsEvent += Events_ArgsEvent;
		ArgsEvent?.Invoke(this, new TestEventArgs() { Status = "Erfolg" });

		IntEvent += Events_IntEvent;
		IntEvent?.Invoke(this, 10);
	}

	private void Events_TestEvent(object sender, EventArgs e)
	{
        Console.WriteLine("TestEvent wurde ausgeführt");
	}

	private void Events_ArgsEvent(object sender, TestEventArgs e)
	{
        Console.WriteLine(e.Status);
	}

	private void Events_IntEvent(object sender, int e)
	{
		Console.WriteLine(e);
	}
}

public class TestEventArgs : EventArgs
{
	public string Status { get; set; }
}