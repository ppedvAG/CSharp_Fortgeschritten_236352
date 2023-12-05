using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection;

namespace Benchmarks;

internal class Program
{
	static void Main(string[] args) => BenchmarkRunner.Run<Benchmarks>();
}

[MemoryDiagnoser(false)]
public class Benchmarks
{
	private List<PersonReflection> personReflection = new();

	private List<PersonRecord> personRecord = new();

	[Params(1000, 5000, 10000, 50000)]
	public int Amount;

	[GlobalSetup]
	public void Setup()
	{
		for (int i = 0; i < Amount; i++)
		{
			int id = i;
			string vorname = (Random.Shared.Next(0, 20) * 3287592).ToString();
			string nachname = (Random.Shared.Next(0, 20) * 13257912).ToString();
			DateTime gebDat = new DateTime(Random.Shared.Next((int) 1E9, (int) 2E9));
			bool verheiratet = Random.Shared.Next() % 2 == 0;

			personReflection.Add(new PersonReflection(id, vorname, nachname, gebDat, verheiratet));
			personRecord.Add(new PersonRecord(id, vorname, nachname, gebDat, verheiratet));
		}
	}

	[Benchmark]
	[IterationCount(50)]
	public void TestReflection()
	{
		int x = 0;
		foreach (PersonReflection p1 in personReflection)
			foreach (PersonReflection p2 in personReflection)
				if (p1 == p2)
					x++;
	}

	[Benchmark]
	[IterationCount(50)]
	public void TestRecord()
	{
		int x = 0;
		foreach (PersonRecord p1 in personRecord)
			foreach (PersonRecord p2 in personRecord)
				if (p1 == p2)
					x++;
	}
}

public class PersonReflection
{
	public int ID { get; set; }

	public string Vorname { get; set; }

	public string Nachname { get; set; }

	public DateTime GebDat { get; set; }

	public bool IstVerheiratet { get; set; }

	public PersonReflection(int iD, string vorname, string nachname, DateTime gebDat, bool istVerheiratet)
	{
		ID = iD;
		Vorname = vorname;
		Nachname = nachname;
		GebDat = gebDat;
		IstVerheiratet = istVerheiratet;
	}

	private static PropertyInfo[] Properties = typeof(PersonReflection).GetProperties();

	public static bool operator ==(PersonReflection p1, PersonReflection p2)
	{
		return Properties.All(e => e.GetValue(p1) == e.GetValue(p2));
	}

	public static bool operator !=(PersonReflection p1, PersonReflection p2)
	{
		return !(p1 == p2);
	}
}

public record PersonRecord(int ID, string Vorname, string Nachname, DateTime GebDat, bool IstVerheiratet);