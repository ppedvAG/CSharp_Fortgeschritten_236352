using System.Windows;

namespace DelegatesEventsWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{ 
		//Console gibt es in der GUI nicht
		//Über Events definieren, wo der Output herauskommt
		//Wenn in der Komponente die Outputs gemacht werden, muss die Komponente angepasst werden

		InitializeComponent();

		Component comp = new();
		comp.ProcessStarted += Comp_ProcessStarted;
		comp.ProcessEnded += Comp_ProcessEnded;
		comp.Progress += Comp_Progress;
		comp.Progress += Comp_Progress1;
		comp.DoWork();
	}

	private void Comp_ProcessStarted()
	{
		TB.Text += "Prozess gestartet\n";
	}

	private void Comp_ProcessEnded()
	{
		TB.Text += "Prozess beendet\n";
	}

	private void Comp_Progress(int obj)
	{
		TB.Text += "Fortschritt: " + obj + "\n";
	}

	private void Comp_Progress1(int obj)
	{
		TB.Text += obj + "\n";
	}
}
