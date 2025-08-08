using UnityEngine;

public static class BattleOutcome
{
    public static PopUp.PopUpType DetermineOutcome(CardType enemy, CardType user)
    {
        if (enemy == user)
        {
            GameEvents.TriggerScore(10);
            return PopUp.PopUpType.Draw;
        }

        if ((enemy == CardType.Rock && user == CardType.Paper) ||
            (enemy == CardType.Paper && user == CardType.Scissors) ||
            (enemy == CardType.Scissors && user == CardType.Rock))
        {
            MusicManager.Instance.PlayHitSFX();
            GameEvents.TriggerScore(20);
            return PopUp.PopUpType.Win;
        }

        GameEvents.TriggerScore(-10);
        MusicManager.Instance.PlayMissSFX();
        return PopUp.PopUpType.Lose;
    }
}
