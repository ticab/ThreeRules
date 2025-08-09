using UnityEngine;

public static class BattleOutcome
{
    public static PopUpUI.PopUpType DetermineOutcome(CardType enemy, CardType user)
    {
        if (enemy == user)
        {
            MusicManager.Instance.PlayDrawSFX();
            EventSystem.TriggerScore(10);
            return PopUpUI.PopUpType.Draw;
        }

        if ((enemy == CardType.Rock && user == CardType.Paper) ||
            (enemy == CardType.Paper && user == CardType.Scissors) ||
            (enemy == CardType.Scissors && user == CardType.Rock))
        {
            MusicManager.Instance.PlayHitSFX();
            EventSystem.TriggerScore(20);
            return PopUpUI.PopUpType.Win;
        }

        EventSystem.TriggerScore(-10);
        MusicManager.Instance.PlayMissSFX();
        return PopUpUI.PopUpType.Lose;
    }
}
