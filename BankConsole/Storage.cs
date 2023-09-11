using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankConsole;

public static class Storage{
    static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    public static void AddUser(object user){

        string usersInFile = "";

        if (File.Exists(filePath)){
            usersInFile = File.ReadAllText(filePath);
        }

        var listOfUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile) ?? new List<object>();

        listOfUsers.Add(user);

        JsonSerializerSettings settings = new();
        settings.Formatting = Formatting.Indented;

        string json = JsonConvert.SerializeObject(listOfUsers, settings);
        
        File.WriteAllText(filePath, json);

    }

    public static void AddUsers(object[] users){
        string usersInFile = "";

        if (File.Exists(filePath)){
            usersInFile = File.ReadAllText(filePath);
        }

        var listOfUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile) ?? new List<object>();

        foreach (var user in users){
            listOfUsers.Add(user);
        }

        JsonSerializerSettings settings = new();
        settings.Formatting = Formatting.Indented;

        string json = JsonConvert.SerializeObject(listOfUsers, settings);

        File.WriteAllText(filePath, json);
    }

    public static List<User> GetListOfNewUsers(){

        List<User> listUsers = GetAllUsers() ?? new List<User>();

        var newUsersList = listUsers.Where(user => user.GetRegisterDate().Date.Equals(DateTime.Today)).ToList();

        return newUsersList;

    }

    public static List<User>? GetAllUsers(){

        string usersInFile;

        try{
            usersInFile = File.ReadAllText(filePath);
        } catch {

            Console.WriteLine("¡El archivo 'users.json' no se encuentra en el directorio del programa!");
            Console.WriteLine("Creando un archivo nuevo 'users.json'...");
            File.WriteAllText(filePath, "");
            Console.WriteLine("¡Archivo Creado!");
            return null;
            
        }
        List<User> users = new List<User>();

        var objsInFile = JsonConvert.DeserializeObject<List<object>>(usersInFile) 
            ?? new List<object>();

        foreach (var obj in objsInFile){

            User newUser;
            JObject user = (JObject)obj;
            if (user.ContainsKey("TaxRegime")){
                newUser = user.ToObject<Client>() ?? new Client();
            } else {
                newUser = user.ToObject<Employee>() ?? new Employee();
            }

            users.Add(newUser);

        }
        
        return users;

    }

    public static void DeleteUser(int ID){

        List<User> users = GetAllUsers() ?? new List<User>();
        List<User> keptUsers = users.Where(user => !user.GetID().Equals(ID)).ToList();
        
        JsonSerializerSettings settings = new();
        settings.Formatting = Formatting.Indented;

        string json = JsonConvert.SerializeObject(keptUsers, settings);
        File.WriteAllText(filePath, json);
        
    }

}