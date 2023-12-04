namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellungen(string name); //Definition von Delegate mit dem delegate Keyword und einem Methodenkopf

	static void Main(string[] args)
	{
		Vorstellungen v = new(VorstellungDE); //Erstellung des Delegates mit Initialmethode
		v("Max"); //Methode ausführen, die an dem Delegate hängt

		v += VorstellungDE; //Methode an das Delegate anhängen
		v("Tim"); //2 Outputs

		v += VorstellungDE;
		v += VorstellungDE;
		v += VorstellungDE;

		v -= VorstellungDE; //Methode vom Delegate herunternehmen
		
		v += VorstellungEN;
		v("Leo");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN; //Wenn eine Methode heruntergenommen wird, die nicht an dem Delegate angehängt ist, passiert nichts

		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE;
		v -= VorstellungDE;
		//v("Max"); //Wenn die letzte Methode abgenommen wird, ist das Delegate Objekt null

		if (v is not null)
			v("Max");

		//Null propagation: Führe die Methode aus, wenn das Objekt nicht null ist
		v?.Invoke("Max"); //Generell sollten Delegates immer mit einem Null-Check ausgeführt werden

		v += VorstellungDE;
		foreach (Delegate dg in v.GetInvocationList()) //Alle Methoden an dem Delegate anschauen
		{
            Console.WriteLine(dg.Method.Name);
        }
	}

	static void VorstellungDE(string name)
	{
        Console.WriteLine($"Hallo mein Name ist {name}");
    }

	static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}