using SQLite;

namespace eramos_Semana5;
public class PersonRepository
{
    string _dbPath; //ruta
    private SQLiteConnection conectionSQL;
    //Mensaje a mostrar
    public string StatusMessage { get; set; }

    private void Init()
    {
        if (conectionSQL is not null)
            return;
        conectionSQL = new(_dbPath);
        conectionSQL.CreateTable<Person>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    /// <summary>
    /// Adds a new person to the database with the given name.
    /// </summary>
    /// <param name="name">The name of the person to add.</param>
    /// <exception cref="Exception">Thrown if the name is null or empty.</exception>
    public void AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            Init();
            //Valida que se ingrese el nombre
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name is required");
            Person person = new() { Name = name };
            result = conectionSQL.Insert(person);

            StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
        }
        catch (Exception e)
        {
            StatusMessage = string.Format("Error {0}", e.Message);
        }
    }

    /// <summary>
    /// Retrieves all people from the database.
    /// </summary>
    /// <returns>A list of Person objects representing all people in the database.</returns>
    /// <exception cref="Exception">Thrown if there is an error retrieving the data.</exception>
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

    /// <summary>
    /// Deletes a person record from the database based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the person to be deleted.</param>
    /// <exception cref="Exception">Thrown if there is an error during the deletion process.</exception>
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
}