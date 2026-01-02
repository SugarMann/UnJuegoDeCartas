public class KillEnemyGA : GameAction
{
    public KillEnemyGA(EnemyView enemyView)
    {
        EnemyView = enemyView;
    }

    public EnemyView EnemyView { get; private set; }
}