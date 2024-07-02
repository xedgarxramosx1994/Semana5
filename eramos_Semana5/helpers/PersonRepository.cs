using SQLite;

namespace eramos_Semana5;
public class PersonRepository
{
    string _dbPath;
    private SQLiteConnection conectionSQL;
    public string StatusMessage { get; set; }

    /// <summary>
    /// Initializes the connection to the database and creates the table for the Person model.
    /// </summary>
    /// <remarks>
    /// This method checks if the connection to the database is already established.
    /// If the connection is not null, the method returns immediately.
    /// Otherwise, it creates a new connection using the provided database path and
    /// creates a table for the Person model.
    /// </remarks>
    private void Init()
    {
        if (conectionSQL is not null)
            return;
        conectionSQL = new(_dbPath);
        conectionSQL.CreateTable<Person>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonRepository"/> class with the specified database path.
    /// </summary>
    /// <param name="dbPath">The path to the database file.</param>
    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    // Adds a new person to the database with the provided name. 
    // If the name is empty or null, it throws an exception. 
    // It then creates a new Person object, inserts it into the database, 
    // and updates the StatusMessage with the number of records added and the person's name.
    // If an exception occurs during the process, it updates the StatusMessage with the error message.
    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            Init();
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is required");
            Person person = new() { Name = name };
            result = conectionSQL.Insert(person);
            StatusMessage = string.Format("{0} record(s) added [Name: {1}]", result, name);
        }
        catch (Exception e)
        {
            StatusMessage = string.Format("Error {0}", e.Message);
        }
    }

    /// <summary>
    /// Retrieves a list of all people from the database.
    /// </summary>
    /// <returns>A list of Person objects representing all people in the database.</returns>
    public List<Person> GetAllPeople()
    {
        try
        {
            Init();
            return conectionSQL.Table<Person>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            return new List<Person>();
        }
    }

    // Deletes a person record from the database based on the provided ID.
    public void DeletePerson(int id)
    {
        try
        {
            Init();
            conectionSQL.Delete<Person>(id);
            StatusMessage = string.Format("Record {0} deleted", id);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }
    }

    /// <summary>
    /// Updates a person record in the database with the provided Person object.
    /// </summary>
    /// <param name="person">The Person object to update.</param>
    /// <remarks>
    /// This method will update the person record in the database with the provided Person object.
    /// If the update is successful, the StatusMessage will be set to a string indicating the
    /// record was updated with the person's ID. If an exception occurs during the update,
    /// the StatusMessage will be set to a string indicating the update failed with the
    /// exception message.
    /// </remarks>
    public void UpdatePerson(Person person)
    {
        try
        {
            Init();
            conectionSQL.Update(person);
            StatusMessage = string.Format("Record {0} updated", person.Id);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to update data. {0}", ex.Message);
        }
    }
}