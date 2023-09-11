namespace BankConsole;

public class Employee : User, IPerson{

    public string? Department { get; set; }

    public Employee(){

    }

    public Employee(int id, string name, string email, decimal balance, string department) : base(id, name, email){
        Department = department;
        SetBalance(balance);
    }

    public override void SetBalance(decimal amount){
        base.SetBalance(amount);

        if (Department!.Equals("IT")){
            Balance += amount * 0.05m;
        }

    }
        

    public override string ShowData(){
        return base.ShowData() + $"Departamento: {Department}";
    }

    public string GetName()
    {
        return Name ?? "Desconocido";
    }

    public string GetCountry()
    {
        return "México";
    }
}