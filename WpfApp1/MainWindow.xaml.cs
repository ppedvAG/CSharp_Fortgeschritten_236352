using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	static int Berechne()
	{
		Thread.Sleep(5000);
		return Random.Shared.Next();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		//Task<int> t = new Task<int>(Berechne);
		//t.Start();

		//TB.Text = t.Result.ToString();

		Task<int> t = new Task<int>(Berechne);
		t.ContinueWith(x => Dispatcher.Invoke(() => TB.Text = x.Result.ToString()));
		t.Start();
	}
}
