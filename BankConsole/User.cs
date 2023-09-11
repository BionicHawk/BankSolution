using Newtonsoft.Json;

namespace BankConsole;

public class User{
    
    protected static int _NumberOfAccounts = 0;
    [JsonProperty]
    protected int ID {get; set;}
    [JsonProperty]
    protected string? Name { get; set; }
    [JsonProperty]
    protected string? Email { get; set; }
    [JsonProperty]
    protected decimal Balance { get; set; }
    [JsonProperty]
    protected DateTime RegisterDate { get; set; }

    public User(){
        ID = ++_NumberOfAccounts;
        Name = "Desconocido";
        Email = "Desconocido";
        RegisterDate = DateTime.Now;
    }

    public User(int id, string name, string email){
        ID = id;
        Name = name;
        Email = email;
        RegisterDate = DateTime.Now;
    }

    public DateTime GetRegisterDate(){
        return RegisterDate;
    }

    public virtual string ShowData(){
        return $"ID: {ID}\nNombre: {Name}\nCorreo: {Email}\nDinero: {Balance}\nDía de Registro: {RegisterDate.ToShortDateString()}\n";
    }

    public string ShowData(string initialMessage){
        return $"[{initialMessage}]\nID: {ID}\nNombre: {Name}\nCorreo: {Email}\nDinero: {Balance}\nDía de Registro: {RegisterDate.ToShortDateString()}\n";
    }

    public virtual void SetBalance(decimal amount){
        Balance += Math.Clamp(amount, 0, decimal.MaxValue);
    }
    public decimal GetBalance(){
        return Balance;
    }

    public int GetID() {
        return ID;
    }

}