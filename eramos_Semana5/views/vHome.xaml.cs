using eramos_Semana5;

namespace MauiApp1;

public partial class vHome : ContentPage
{
	private int personToEditId = -1;
	private Person personToEdit = null!;
	/// <summary>
	/// Initializes a new instance of the <see cref="vHome"/> class.
	/// </summary>
	public vHome()
	{
		InitializeComponent();
	}

	// Handles the event when the "Add" button is clicked. Adds a new person to the repository with the name entered in the text box, updates the status message, and refreshes the person list.
	private void btnAdd_Clicked(object sender, EventArgs e)
	{
		statusMessage.Text = "";
		App._personRepository.AddNewPerson(txtName.Text);
		statusMessage.Text = App._personRepository.StatusMessage;
		RefreshPersonList();
	}

	// Handles the button click event when the "Get All" button is clicked. Retrieves all people from the person repository and updates the list of people displayed.
	private void btnGetAll_Clicked(object sender, EventArgs e)
	{
		statusMessage.Text = "";
		var people = App._personRepository.GetAllPeople();
		listPerson.ItemsSource = people;
		statusMessage.Text = App._personRepository.StatusMessage;
	}

	/// <summary>
	/// Handles the event when the "Edit" button is clicked. 
	/// Retrieves the person associated with the clicked button and updates the text box with the person's name. 
	/// Sets the personToEdit variable to the retrieved person.
	/// </summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The event data.</param>
	private void btnEdit_Clicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var person = button?.BindingContext as Person;
		if (person != null)
		{
			txtName.Text = person.Name;
			personToEdit = person;
		}
	}

	/// <summary>
	/// Saves the edited person's name and updates the person in the repository.
	/// Also updates the status message and resets the person to edit and the text box.
	/// </summary>
	/// <param name="sender">The object that raised the event.</param>
	/// <param name="e">The event arguments.</param>
	private void btnSaveEdit_Clicked(object sender, EventArgs e)
	{
		if (personToEdit != null)
		{
			personToEdit.Name = txtName.Text;
			App._personRepository.UpdatePerson(personToEdit);
			statusMessage.Text = App._personRepository.StatusMessage;
			personToEdit = null!;
			txtName.Text = "";
			RefreshPersonList();
		}
	}

	// Handles the button click event for deleting a person. Retrieves the person associated with the button,
	// prompts the user for confirmation, deletes the person if confirmed, updates the status message, and refreshes the person list.
	private async void btnDelete_Clicked(object sender, EventArgs e)
	{
		var button = sender as Button;
		var person = button?.BindingContext as Person;
		if (person != null)
		{
			bool confirm = await DisplayAlert("Confirmar eliminación", $"¿Estás seguro de que deseas eliminar a {person.Name}?", "Sí", "No");
			if (confirm)
			{
				App._personRepository.DeletePerson(person.Id);
				statusMessage.Text = App._personRepository.StatusMessage;
				RefreshPersonList();
			}
		}
	}

	// Refreshes the person list by retrieving all people from the person repository and updating the list of people displayed.
	private void RefreshPersonList()
	{
		var people = App._personRepository.GetAllPeople();
		listPerson.ItemsSource = people;
	}
}