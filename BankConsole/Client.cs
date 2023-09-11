namespace BankConsole;

public class Client : User, IPerson{

    public char TaxRegime { get; set; }

    public Client(){

    }

    public Client(int id, string name, string email, decimal balance, char taxRegime) : base(id, name, email){
        TaxRegime = taxRegime;
        SetBalance(balance);
    }

    public override void SetBalance(decimal amount){
        base.SetBalance(amount);

        if (TaxRegime.Equals('M')){
            Balance += amount * 0.02m;
        }
    }

    public override string ShowData(){
        return base.ShowData() + $"Régimen físcal: {TaxRegime}";
    }

    public string GetName()
    {
        return Name + "!" ?? "Desconocido";
    }

    public string GetCountry()
    {
        return "México";
    }
}