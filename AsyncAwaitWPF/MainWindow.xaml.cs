using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void Start(object sender, RoutedEventArgs e)
	{
		for (int i = 0; i < 50; i++)
		{
			Thread.Sleep(25); //Thread.Sleep blockiert, UI läuft am Main Thread
			TB.Text += i + "\n";
		}
	}

	private void StartTaskRun(object sender, RoutedEventArgs e)
	{
		Task.Run(() =>
		{
			for (int i = 0; i < 50; i++)
			{
				Thread.Sleep(25);
				Dispatcher.Invoke(() => TB.Text += i + "\n");
				//UI Updates dürfen nicht von Side Threads/Tasks ausgeführt werden
				//Dispatcher: Property, welches auf jedem UI Element enthalten ist, und ermöglicht auf dem Thread der Komponente beliebigen Code auszuführen
			}
		});
	}

	private async void StartAwait(object sender, RoutedEventArgs e)
	{
		//Mit Async/Await kann der Code darüber stark vereinfacht werden
		for (int i = 0; i < 50; i++)
		{
			await Task.Delay(25);
			TB.Text += i + "\n";
		}
	}

	private async void StartHttpClient(object sender, RoutedEventArgs e)
	{
		//Aufbau
		//Task starten: Task Variable anlegen und Async Methode starten
		//(optional) Zwischenschritte
		//Auf das Ergebnis warten mit await auf den vorher gestarteten Task

		using HttpClient client = new();
		Task<HttpResponseMessage> request = client.GetAsync("http://www.gutenberg.org/files/54700/54700-0.txt"); //Task starten
		TB.Text = "Request gestartet"; //Zwischenschritt
		Button1.IsEnabled = false; //Zwischenschritt
		HttpResponseMessage resp = await request; //Auf Ergebnis warten mit await
		if (resp.IsSuccessStatusCode)
		{
			Task<string> text = resp.Content.ReadAsStringAsync(); //Task starten
			TB.Text = "Request wird ausgelesen"; //Zwischenschritt
			await Task.Delay(500); //Künstliches Delay
			TB.Text = await text; //Auf Ergebnis warten mit await
		}
		Button1.IsEnabled = true;
	}

	private async void StartIAsyncEnumerable(object sender, RoutedEventArgs e)
	{
		//IAsyncEnumerable: IEnumerable aber Async -> Die Daten kommen vielleicht schnell oder auch nicht
		//z.B. Livestream, Bild und Ton kommt oder auch nicht

		AsyncDataSource dataSource = new();
		//foreach (...)
		//	await Next Number
		await foreach (int x in dataSource.GetNumbers())
		{
			TB.Text += x + "\n";
			Scroll.ScrollToBottom();
		}
	}
}
