using MauiApp1;

namespace eramos_Semana5;

public partial class App : Application
{
	public static PersonRepository _personRepository {get; set; } = null!;
	public App(PersonRepository personRepository)
	{
		InitializeComponent();

		MainPage = new vHome();
		_personRepository = personRepository;
	}
}
