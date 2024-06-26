using eramos_Semana5;

namespace MauiApp1;

public partial class vHome : ContentPage
{
	public vHome()
	{
		InitializeComponent();
	}
	/// <summary>
	/// Handles the Clicked event of the btnAdd control.
	/// Clears the status message, adds a new person to the repository using the text from txtName,
	/// and updates the status message with the result of the operation.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
	private void btnAdd_Clicked(object sender, EventArgs e)
	{
		statusMessage.Text = "";
		App._personRepository.AddNewPerson(txtName.Text);
		statusMessage.Text = App._personRepository.StatusMessage;
	}

	// Handles the button click event to retrieve all people from the database and display them in the list view.
	private void btnGetAll_Clicked(object sender, EventArgs e)
	{
		statusMessage.Text = "";
		var people = App._personRepository.GetAllPeople();
		listPerson.ItemsSource = people;
		statusMessage.Text = App._personRepository.StatusMessage;
	}
}