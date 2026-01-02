using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public void OnClick()
    {
        EnemyTurnGA enemyTurnGa = new();
        ActionSystem.Instance.Perform(enemyTurnGa);
    }
}