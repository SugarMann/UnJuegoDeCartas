public class SpendManaGA : GameAction
{
    public SpendManaGA(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; private set; }
}