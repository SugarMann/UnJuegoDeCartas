public class DrawCardsGA : GameAction
{
    public DrawCardsGA(int amount)
    {
        Amount = amount;
    }

    public int Amount { get; set; }
}