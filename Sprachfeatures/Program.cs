using System.Text.Json.Serialization;

namespace Sprachfeatures;

public class Program
{
	static unsafe void Main(string[] args)
	{
		//Unterschied zwischen o.GetType() == typeof(...) und o is <Type>

		//GetType == typeof: Genauer Typvergleich
		//Nur true, wenn die Typen genau übereinstimmen
		List<int> i = new List<int>();
		if (i.GetType() == typeof(IEnumerable<int>))
		{
			//False, weil die Typen nicht genau übereinstimmen
		}

		//is: Vererbungshierarchietypvergleich
		//True, wenn die Typen kompatibel sind
		if (i is IEnumerable<int>)
		{
			//True, weil die Typen kompatibel sind
		}

		//Nicht möglich:
		//Type p = typeof(int);
		//switch (p)
		//{
		//	case typeof(int):
		//		break;
		//}

		//Typswitch (durch Pattern Matching):
		int o = 5;
		switch (o)
		{
			case IComparable:
				break;
		}

		//Unterschied zwischen class und struct

		//Referenztyp
		//class
		//Wenn ein Objekt einer Klasse in eine Variable geschrieben wird, wird eine Referenz gespeichert (Zeiger auf das Objekt im RAM)
		//Wenn zwei Objekte einer Klasse verglichen werden, werden die Speicheradressen verglichen (Hash Codes)
		Person p = new Person();
		Person p2 = p; //Referenz auf das Objekt hinter p
		p.Vorname = "Max"; //Inhalt wird bei beiden Variablen angepasst, da darunter das selbe Objekt liegt

        Console.WriteLine(p.GetHashCode());
        Console.WriteLine(p2.GetHashCode());
        Console.WriteLine(p == p2); //p.GetHashCode() == p2.GetHashCode()

		//Wertetyp
		//struct
		//Wenn ein Objekt eines Structs in eine Variable geschrieben wird, wird eine Kopie von dem unterliegenden Wert erzeugt
		//Wenn zwei Objekte eines Structs verglichen werden, werden die Inhalte verglichen
		int original = 5;
		int x = original; //Hier wird der Inhalt von original in x kopiert
		original = 10; //Hier wird nur original verändert (nicht x)

		int originalRef = 5;
		ref int xRef = ref originalRef;
		originalRef = 10; //Hier wird jetzt durch ref einer Referenz hergestellt (beide Werte werden verändert)

		Test(ref x); //x als Referenz übergeben

		//Doppel Fragezeichen Operator (??): Null coalescing Operator
		//Gibt den linken Wert zurück, wenn er nicht null ist, sonst den rechten Wert

		string name = "Max";

		string name2;
		if (name != null)
			name2 = name;
		else
			name2 = "Kein Name";

		name2 = name != null ? name : "Kein Name"; //Vereinfachung von darüber

		name2 = name ?? "Kein Name"; //Vereinfachung von darüber

		//Methode konfigurierbar machen
		Test(alter: 30);
		Test(vorname: "Max", alter: 30);
		Test(alter: 30, nachname: "Max");

		if (p == null)
		{
            Console.WriteLine();
        }

		if (p == default)
		{
            Console.WriteLine();
        }

		int* a = stackalloc int[5] { 1, 2, 3, 4, 5 };
		a[0] = 10;

		nint n = 10;
		n = 20;

		using HttpClient client = new();

		for (int j = 0; j < 10; j++)
		{
			using (HttpClient c = new())
			{

			}
		}

		void Test2() => Console.WriteLine(p);
		//static void Test() => Console.WriteLine(p); //p nicht mehr verfügbar

		Mensch m = new();
		m.Clone();
		m.Test();

		Person pm = m;
		pm.Clone();
		pm.Test();

		if (m != null)
            Console.WriteLine();

        if (m is not null)
            Console.WriteLine();

		//Wochentage: Print Wochentag für Mo-Fr, Print Wochenende für Sa, So

		switch (DateTime.Now.DayOfWeek)
		{
			case DayOfWeek.Monday:
			case DayOfWeek.Tuesday:
			case DayOfWeek.Wednesday:
			case DayOfWeek.Thursday:
			case DayOfWeek.Friday:
                Console.WriteLine("Wochentag");
				break;
			case DayOfWeek.Saturday:
			case DayOfWeek.Sunday:
                Console.WriteLine("Wochenende");
				break;
        }

		switch (DateTime.Now.DayOfWeek)
		{
			case >= DayOfWeek.Monday and <= DayOfWeek.Friday:
				Console.WriteLine("Wochentag");
				break;
			case DayOfWeek.Saturday or DayOfWeek.Sunday:
				Console.WriteLine("Wochenende");
				break;
		}

		Mitarbeiter mitarbeiter = new(1, "Max");
		(int id, string vn) = mitarbeiter;

		Half half = Half.MaxValue;

		string str = $$""" " {{{5}}} """;
		string str2 = $" \" {{{5}}}";
	}

	public static void Test(ref int x) => x = 100; //x in der Main Methode wird angepasst

	public static void Test(string vorname = "", string nachname = "", int alter = 0)
	{

	}
}

public class Person : ICloneable
{
	public string Vorname { get; set; }

	public object Clone() => null;

	public void Test()
	{
        Console.WriteLine();
    }

	public static bool operator ==(Person p, Person p2)
	{
		return true;
		//return p.GetType().GetProperties().All(info => info.GetValue(p) == info.GetValue(p2));
	}

	public static bool operator !=(Person p, Person p2)
	{
		return !(p == p2);
	}
}

public class Mensch : Person
{
	public string Vorname { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public object Clone()
	{
		//throw new NotImplementedException();
		return null;
	}
}

public record Mitarbeiter([field: JsonIgnore] int ID, string Vorname)
{
	public void Test()
	{
		Console.WriteLine();
	}
}