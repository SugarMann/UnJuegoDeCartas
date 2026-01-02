public class PlayCardGA : GameAction
{
    public PlayCardGA(Card card)
    {
        Card = card;
        ManualTarget = null;
    }

    public PlayCardGA(Card card, EnemyView target)
    {
        Card = card;
        ManualTarget = target;
    }

    public EnemyView ManualTarget { get; private set; }
    public Card Card { get; set; }
}