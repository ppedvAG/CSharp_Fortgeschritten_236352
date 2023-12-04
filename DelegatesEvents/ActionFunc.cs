using System;

namespace DelegatesEvents;

internal class ActionFunc
{
	static void Main(string[] args)
	{
		//Action, Func: Vorgegebene Delegates die an vielen in C# vorkommen
		//-> Methodenzeiger, z.B. Linq, Reflection, Multitasking, ...
		//Essentiell für die fortgeschrittene Programmierung in C#
		//Können alles was in dem vorherigen Teil besprochen wurde


		//Action: Delegate mit void als Rückgabetyp und bis zu 16 Parametern
		Action<int, int> a = Addiere; //Über die Generics werden die Parameter festgelegt
		a(4, 5);
		a?.Invoke(3, 9);

		//Verwendung: Parameter bei Methoden
		//Über den Parameter die Methode anpassen
		DoAction(4, 8, Addiere);
		DoAction(6, 2, Subtrahiere);
		DoAction(3, 1, a);

		List<int> intList = Enumerable.Range(0, 10).ToList();

		intList.ForEach(Quadriere);
		void Quadriere(int x) => Console.WriteLine(x * x); //Rückgabetyp void, Parameter int

		intList.ForEach(Console.WriteLine); //WriteLine als Methodenzeiger übergeben


		//Func: Methode mit einem beliebigen Rückgabetyp und bis zu 16 Parametern
		//WICHTIG: Das letzte Generic ist der Rückgabetyp
		Func<int, int, double> f = Multipliziere;
		double d = f(3, 5); //Bei einer Func kommt immer ein Ergebnis heraus -> kann in eine Variable gespeichert werden
		//double d2 = f?.Invoke(4, 5); //?.Invoke könnte null sein, wenn f null ist
		double? d2 = f?.Invoke(4, 5); //Lösung 1: Nullable double
		double d3 = f?.Invoke(4, 5) ?? double.NaN; //Lösung 2: double.NaN falls das Ergebnis null ist

		DoFunc(4, 6, Multipliziere);
		DoFunc(4, 6, Dividiere);
		DoFunc(4, 6, f);

		//Praktische Beispiele
		intList.Where(TeilbarDurch2);
		bool TeilbarDurch2(int x) => x % 2 == 0;

		//Anonyme Methoden: Methoden, für die keine separate Methode angelegt wird
		f += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		f += (int x, int y) => { return x + y; }; //Kürzere Form

		f += (x, y) => { return x - y; };

		f += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Anonyme Methoden als Parameter
		intList.Where(e => e % 2 == 0);
		DoAction(4, 6, (x, y) => Console.WriteLine(x + y));
		DoFunc(6, 2, (x, y) => x % y);

		//Wie funktioniert Where?
		List<int> durch3 = Where(intList, e => e % 3 == 0).ToList();
	}

	#region Action
	static void Addiere(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");

	static void Subtrahiere(int x, int y) => Console.WriteLine($"{x} - {y} = {x - y}");

	static void DoAction(int x, int y, Action<int, int> action) => action?.Invoke(x, y);
	#endregion

	#region Func
	public static double Multipliziere(int x, int y) => x * y;

	public static double Dividiere(int x, int y) => x / y;

	public static double DoFunc(int x, int y, Func<int, int, double> func) => func?.Invoke(x, y) ?? double.NaN;
	#endregion

	public static IEnumerable<T> Where<T>(IEnumerable<T> list, Func<T, bool> predicate)
	{
		foreach (T item in list) //Gehe alle Elemente durch
			if (predicate(item)) //Gib alle Elemente in das Predicate hinein, true oder false Ergebnis
				yield return item; //Wenn true -> zurückgeben
	}
}
