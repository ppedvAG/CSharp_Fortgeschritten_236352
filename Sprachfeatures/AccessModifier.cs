namespace Sprachfeatures;

internal class AccessModifier
{
	public int Alter { get; set; } //Kann überall gesehen werden (auch außerhalb vom Projekt)

	private string Name { get; set; } //Kann nur innerhalb der Klasse gesehen werden

	internal string Adresse { get; set; } //Kann überall gesehen werden (nur innerhalb des Projekts)


	protected int ID { get; set; } //Kann nur innerhalb der Klasse gesehen werden, und auch in Unterklassen (auch außerhalb vom Projekt)

	protected private string Description { get; set; } //Kann nur innerhalb der Klasse gesehen werden und auch in Unterklassen (nur innerhalb des Projekts)

	protected internal string Haarfarbe { get; set; } //Kann überall gesehen werden (nur innerhalb des Projekts), aber auch in Unterklassen außerhalb des Projekts
}
