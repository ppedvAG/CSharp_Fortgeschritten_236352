using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Reflection;

internal class Program
{
	public static void Main(string[] args)
	{
		//Geht immer einem Type Objekt aus
		//2 Möglichkeiten:
		//typeof(...), GetType()

		Type pt = typeof(Program); //Typen anhand eines Namens

		Program p = new Program();
		Type type = p.GetType(); //Typen anhand eines Objects

		//Über das Type Objekt sehr viele Informationen herausfinden
		//z.B.: Welche Methoden, Properties, Felder, Interfaces, ... hat der Typ?
		//Ist der Typ Public, Sealed, ein Enum, ...?

		//Über die entsprechenden Get-Methoden können weiters auch Informationen über die entsprechenden Member erlangt werden

		Person person = new Person(0, "Max", "Mustermann", 30, true);
		PrintProperties(person);

		PrintMethods(p);

		pt.GetMethod("PrintMethods", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
			.Invoke(p, new object[] { p });

		//Activator
		//Gibt uns die Möglichkeit über einen Type ein Objekt zu erstellen
		object o = Activator.CreateInstance(pt);

		//Assembly
		//Die derzeitige Codebasis (DLL), das derzeitige Projekt
		Assembly.GetExecutingAssembly(); //Das derzeitige Projekt
		Assembly a = Assembly.LoadFrom(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_12_04\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll");

		//Von der Component mit den 3 Events ein Objekt erstellen, Events anhängen und starten
		Type compType = a.GetType("DelegatesEvents.Component");
		object comp = Activator.CreateInstance(compType);
		compType.GetEvent("ProcessStarted").AddEventHandler(comp, () => Console.WriteLine("Prozess gestartet"));
		compType.GetEvent("ProcessEnded").AddEventHandler(comp, () => Console.WriteLine("Prozess beendet"));
		compType.GetEvent("Progress").AddEventHandler(comp, (int x) => Console.WriteLine($"Fortschritt: {x}"));
		compType.GetMethod("DoWork").Invoke(comp, null);
    }

	//Eine Methode definieren, die alle Properties + die Werte an den Properties eines Objekts ausgibt
	public static void PrintProperties(object o)
	{
        Console.WriteLine(
		o.GetType()
			.GetProperties()
			.Aggregate(new StringBuilder(), (agg, prop) => agg.Append($"{prop.Name}: {prop.GetValue(o)}\n"))
			.ToString());
    }

	//Eine Methode definieren, die alle Methoden + Parameter eines Objekts ausgibt
	private static void PrintMethods(object o)
	{
        Console.WriteLine(
            o.GetType()
			.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
			.AsString(m => $"{m.Name}({m.GetParameters().AsString(e => $"{e.ParameterType} {e.Name}")})"));
	}
}

public record Person(int ID, string Vorname, string Nachname, int Alter, bool IstVerheiratet);