using BankConsole;

ushort option = 0;
bool validInput = false;

if (args.Length == 0) {

    Console.WriteLine("Enviar Correo ...");
    EmailService.SendMail();
    Console.WriteLine("Presione cualquier tecla para continua ...");
    Console.ReadKey();

} else {

    while (option != 3) {

        ShowMenu();
        if (validInput) DetermineAction();
        Console.WriteLine("Presione cualquier tecla para continuar ...");
        Console.ReadKey();

    }

}

void ShowMenu() {
    Console.Clear();

    Console.WriteLine(
@"Selecciona una opción:
1.- Crear un nuevo usuario.
2.- Eliminar un Usuario existente.
3.- Salir.
"
    );

    try {

        option = ushort.Parse(Console.ReadLine()!);
        validInput = true;

    } catch(Exception) {

        Console.WriteLine("¡La opción que intentó realizar no es válida!");
        option = 0;
        validInput = false;

    }

}

void DetermineAction() {

    switch (option) {

        case 1:
            CreateNewUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Console.WriteLine("Saliendo del Programa ...");
            break;
        default:
            Console.WriteLine($"¡La opción {option} no está contemplada en el programa!");
            break;

    }

}

void CreateNewUser(){
    List<User> users = Storage.GetAllUsers() ?? new();

    bool invalidInput = false;
    bool idTaken = false;
    bool invalidName = false;
    bool invalidEmail = false;
    bool invalidTypeOfUser = false;
    bool invalidBalance = false;
    bool invalidPerson = false;
    bool invalidDepartment = false;

    uint id = 0;
    string name;
    string email;
    char typeOfUser;
    decimal balance;
    char typeOfPerson;
    string department;

    // Pidiendo El ID al usuario.
    do {

        if (invalidInput) Console.WriteLine("¡El último intento ingresó un valor inválido!");
        if (idTaken) Console.WriteLine("¡El ID que ha quedido establecer ya ha sido tomado!");
        Console.WriteLine("Ingrese el ID del nuevo usuario: ");

        if (!uint.TryParse(Console.ReadLine()!, out id)) {

            invalidInput = true;
            idTaken = false;

        } else {

            invalidInput = false;
            idTaken = CheckIfIDTaken(users, id);

        }

    } while (invalidInput || idTaken);

    // Pidiendo el nombre al usuario.
    do {

        if (invalidName) Console.WriteLine("¡No ha ingresado el nombre correctamente!");
        Console.WriteLine("Ingrese el nombre del nuevo usuario: ");
        name = Console.ReadLine()!;

        if (string.IsNullOrEmpty(name)) invalidName = true;
        else invalidName = false;

    } while (invalidName);

    // Pidiendo el Correo Al usuario.   
    do {

        if (invalidEmail) Console.WriteLine("¡El Correo Electrónico proporcionado es inválido!");
        Console.WriteLine("Ingrese el correo electrónico del usuario nuevo: ");
        email = Console.ReadLine()!;

        invalidEmail = !isThisEmailValid(email);

    } while (invalidEmail);

    // Pidiendo el tipo de usuario al usuario
    do {

        if (invalidTypeOfUser) Console.WriteLine("¡La respuesta acerca del tipo de usuario que ha dado es inválida!");
        Console.WriteLine("Ingrese el tipo de usuario que es el nuevo usuario (inserte 'c' para cliente o 'e' para empleado): ");

        if (!char.TryParse(Console.ReadLine()!, out typeOfUser)){

            invalidTypeOfUser = true;

        } else {

            if (typeOfUser.Equals('c') || typeOfUser.Equals('e')) invalidTypeOfUser = false;
            else invalidTypeOfUser = true;

        }

    } while (invalidTypeOfUser);

    // Pidiendo el balance al usuario
    do {

        if (invalidBalance) Console.WriteLine("¡El saldo que se ha quedido ingresar al usuario es inválido (debe ser un número positivo)!");
        Console.WriteLine("Ingrese el saldo del usuario nuevo: ");

        if (!decimal.TryParse(Console.ReadLine()!, out balance)){
            invalidBalance = true;
        } else {

            if (balance >= 0) invalidBalance = false;
            else invalidBalance = true;

        }

    } while (invalidBalance);

    // Determinando el tipo de usuario
    if (typeOfUser.Equals('c')){
        
        // Pidiendo el tipo de persona.
        do {

            if (invalidPerson) Console.WriteLine("¡El tipo de persona es inválido!");
            Console.WriteLine("Ingrese el tipo de persona para el usuario nuevo (ingrese F para persona física o M para persona moral): ");

            if (!char.TryParse(Console.ReadLine()!, out typeOfPerson)){
                invalidPerson = true;
            } else {

                if (typeOfPerson.Equals('F') || typeOfPerson.Equals('M')){

                    invalidPerson = false;

                } else {

                    invalidPerson = true;

                }

            }

        } while (invalidPerson);

        // Creando Cliente
        var newClient = new Client((int)id, name, email, balance, typeOfPerson);
        Storage.AddUser(newClient);
        Console.WriteLine("¡Usuario de tipo cliente agregado!");

    } else {

        // Pidiendo el departamento del emplado.
        do {

            if (invalidDepartment) Console.WriteLine("¡El departamente específicado es inválido!");
            Console.WriteLine("Ingrese el departamento del nuevo usuario: ");

            department = Console.ReadLine()!;

            if (string.IsNullOrEmpty(department)){
                invalidDepartment = true;
            } else {
                invalidDepartment = false;
            }

        } while (invalidDepartment);

        // Creando Empleado
        var newEmployee = new Employee((int)id, name, email, balance, department);
        Storage.AddUser(newEmployee);
        Console.WriteLine("¡Usuario de tipo empleado creado!");

    }

}

void DeleteUser(){
    List<User> users = Storage.GetAllUsers() ?? new();

    if (users.Count == 0) {
        Console.WriteLine("No hay usuarios en la Base de datos ...");
        return;
    }

    bool invalidId = false;
    uint id = 0;

    do{

        if (invalidId) Console.WriteLine("¡Ingrese un valor válido (tiene que ser un número entero positivo y un número relacionado con un usuario existente en la base de datos)!");
        Console.WriteLine("Ingresa el ID del usuario a eliminar: ");

        if (!uint.TryParse(Console.ReadLine()!, out id)){
            invalidId = true;
        } else {
            if (id >= 0){

                bool idExists = false;

                foreach (var user in users) {
                    if (((uint)user.GetID()).Equals(id)) {
                        idExists = true;
                    }
                }

                if (idExists) invalidId = false;
                else {
                    Console.WriteLine("¡El ID proporcionado no existe en la base de datos!");
                    invalidId = true;
                }

            } else {
                invalidId = true;
            }
        }

    } while (invalidId);

    Storage.DeleteUser((int)id);
    Console.WriteLine($"¡El usuario con ID {id} ha sido eliminado!");

}

bool CheckIfIDTaken (List<User> users, uint id) {

    bool isIDTaken = false;

    foreach(var user in users){

        if (((uint)user.GetID()).Equals(id)){
            isIDTaken = true;
        }

    }

    return isIDTaken;

}

bool isThisEmailValid(string email){

    bool isEmailValid = true;

    if (string.IsNullOrEmpty(email)) return false;

    if (email.Contains(" ")) return false;

    if (!email.Contains("@") || !email.Contains(".")) isEmailValid = false;
    else {

        var atIndex = email.IndexOf("@");
        var dotIndex = email.LastIndexOf(".");
        var lastAtIndex = email.LastIndexOf("@");

        if (!atIndex.Equals(lastAtIndex) || atIndex >= dotIndex) isEmailValid = false;
        else {

            var emailSplitted = email.Split(".");
            var lastPart = emailSplitted.Last();
            if (lastPart.Length < 2) isEmailValid = false;

        }

    } 

    return isEmailValid;
}