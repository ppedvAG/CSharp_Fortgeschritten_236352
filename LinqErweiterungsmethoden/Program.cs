using System.Text;
using System.Text.Json;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Listentheorie
		//IEnumerable
		//Interface, welches die Basis von allen Listentypen bildet (Array, List, Dictionary, ...)
		//Effekt: Macht eine Klasse iterierbar (foreach)

		//IEnumerable ist nur eine Anleitung zum Erstellen der fertigen Collection
		//IEnumerable hat selbst keine Werte
		IEnumerable<int> ints = Enumerable.Range(0, 20); //Nur eine Anleitung, keine konkreten Werte

		IEnumerable<int> vieleZahlen = Enumerable.Range(0, (int) 1E9); //1ms, weil nur eine Anleitung
		//Enumerable.Range(0, (int) 1E9).ToList(); //Hier wird tatsächlich iteriert, 3.5s

		//foreach (int x in Enumerable.Range(0, (int) 1E9)) { }

		//Enumerator
		//Grundkomponente von Listen, der ermöglicht eine Anleitung auszuiterieren
		//3 Teile:
		//bool MoveNext(): Bewegt den Zeiger auf das nächste Element
		//object Current: Das derzeitige Objekt, Zugriff auf Current erzeugt das Element
		//void Reset(): Bewegt den Zeiger auf die Anfangsposition zurück

		foreach (int i in Enumerable.Range(0, 20))
		{
            Console.WriteLine(i);
        }

		//foreach ohne foreach
		IEnumerable<int> enumerable = Enumerable.Range(0, 20);
		IEnumerator<int> enumerator = enumerable.GetEnumerator(); //Aus der Anleitung den Enumerator entnehmen

	start:
		bool more = enumerator.MoveNext();
		if (more)
		{
			Console.WriteLine(enumerator.Current);
			goto start;
		}
		//enumerator.Reset();

		//foreach (object o in 123) { } //foreach-Schleife benötigt GetEnumerator() -> IEnumerable

		vieleZahlen.Where(e => e % 2 == 0);
		//Auch nur eine Anleitung, da Linq Funktionen selbst immer IEnumerable zurückgeben

		//Where per Hand
		Func<int, bool> predicate = e => e % 2 == 0;
	where:
		bool next = enumerator.MoveNext();
		if (next)
		{
			if (predicate(enumerator.Current))
				Console.WriteLine(enumerator.Current);
			goto where;
		}

		//Predicate und Selector
		//Die 2 unterschiedlichen Linq Parametertypen
		//Predicate: bool als Ergebnis -> Where, Count, All, Any, ...
		//Selector: T als Ergebnis -> Select, OrderBy, MinBy, MaxBy, ...
		vieleZahlen.Select(e => e * 2.0);

		//Select per Hand
		Func<int, double> selector = e => e * 2.0;
	select:
		bool x = enumerator.MoveNext();
		if (x)
		{
			Console.WriteLine(selector(enumerator.Current));
			goto select;
		}


		//Average per Hand
		Func<Fahrzeug, double> avg = e => e.MaxV;
		IEnumerator<Fahrzeug> fzgEnumerator = fahrzeuge.GetEnumerator();
		double sum = 0;
	average:
		bool y = fzgEnumerator.MoveNext();
		if (y)
		{
			sum += avg(fzgEnumerator.Current); //Gib das derzeitige in die Average Lambda Expression hinein, und summiere das Ergebnis auf die sum Variable
			goto average;
		}
        Console.WriteLine(sum / fahrzeuge.Count);
        Console.WriteLine(fahrzeuge.Average(e => e.MaxV));
		#endregion

		#region Linq mit Objektliste
		//Select
		//2 Möglichkeiten
		//1. Fall: Einzelnes Feld entnehmen (80% aller Fälle)
		fahrzeuge.Select(e => e.Marke); //IEnumerable<FahrzeugMarke>

		//2. Fall: Liste transformieren (20% aller Fälle)

		//Aus einem Ordner alle Dateien entnehmen ohne Endung und Pfad
		string[] pfade = Directory.GetFiles("C:\\Windows");
		List<string> pfadeOhneEndung = new();
		foreach (string p in pfade)
			pfadeOhneEndung.Add(Path.GetFileNameWithoutExtension(p));

		//Mit Linq
		Directory.GetFiles("C:\\Windows")
			.Select(e => Path.GetFileNameWithoutExtension(e))
			.ToList();

		Directory.GetFiles("C:\\Windows")
			.Select(Path.GetFileNameWithoutExtension)
			.ToList();

		//Alle Dateilängen von .txt Dateien aufsummieren innerhalb eines Ordners
		Directory.GetFiles("C:\\Windows")
			.Where(e => Path.GetExtension(e) == ".txt")
			.Sum(e => File.ReadAllLines(e).Length);

		//Liste casten
		Enumerable.Range(0, 20).Select(e => (double) e);
		"Hello, World".Select(e => (int) e); //String ist auch iterierbar -> Linq verwendbar
											 //72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100

		//GroupBy
		//Erzeugt Gruppen anhand eines Kriteriums, wobei jeder Gruppe die entsprechenden Elemente enthält
		fahrzeuge.GroupBy(e => e.Marke);
		//IEnumerable<IGrouping<FahrzeugMarke, Fahrzeug>>
		//-> Liste von IGrouping
		//IGrouping: KeyValuePair<FahrzeugMarke, List<Fahrzeug>>
		fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList());
		fahrzeuge.ToLookup(e => e.Marke); //Read Only Dictionary, wobei jeder Key beliebig oft existieren

        //history.city.list.min.json mit Linq gruppieren
        //using StreamReader sr = new StreamReader(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_12_04\Serialisierung\bin\Debug\net7.0\history.city.list.min.json");
        //JsonDocument doc = JsonDocument.Parse(sr.BaseStream);
        //ILookup<string?, JsonElement> l = doc.RootElement.EnumerateArray()
        //	.ToLookup(e => e.GetProperty("city").GetProperty("country").GetString());

        //Aggregate
		//Aus einer Liste einen Output erzeugen
        Console.WriteLine
		(
			fahrzeuge
				.Where(e => e.Marke == FahrzeugMarke.VW)
				.Select(e => e.MaxV)
				.Distinct()
		);

		//Um das gesamte Linq Statement eine Funktion wrappen
		Console.WriteLine
		(
			string.Join(", ",
				fahrzeuge
					.Where(e => e.Marke == FahrzeugMarke.VW)
					.Select(e => e.MaxV)
					.Distinct()
			)
		);

		Console.WriteLine
		(
			fahrzeuge
				.Where(e => e.Marke == FahrzeugMarke.VW)
				.Select(e => e.MaxV)
				.Distinct()
				.Aggregate(new StringBuilder(), (agg, e) => agg.Append(e).Append(", "))
				.ToString()
		);
		#endregion

		#region Erweiterungsmethoden
		int zahl = 38275;
		zahl.Quersumme();

        Console.WriteLine(37584912.Quersumme());

		fahrzeuge.Shuffle();

        Console.WriteLine(fahrzeuge.AsString()); //Wenn ein Objekt mit CW ausgegeben wird, wird die ToString() Methode des Objekts aufgerufen
		Console.WriteLine(fahrzeuge.AsString(e => e.Marke));
		Console.WriteLine(fahrzeuge.AsString(e => (e.MaxV, e.Marke)));
		Console.WriteLine(fahrzeuge.AsString(e => $"Das Fahrzeug mit dem Typ {e.Marke} kann maximal {e.MaxV}km/h fahren."));
		#endregion
	}
}

//public record Fahrzeug(int MaxV, FahrzeugMarke Marke);

public class Fahrzeug
{
	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public int MaxV { get; set;}

	public FahrzeugMarke Marke { get; set; }
}

public enum FahrzeugMarke { Audi, BMW, VW }