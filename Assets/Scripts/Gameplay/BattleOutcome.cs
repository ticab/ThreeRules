using UnityEngine;

public static class BattleOutcome
{
    public static PopUpUI.PopUpType DetermineOutcome(CardType enemy, CardUI user)
    {
        CardType userCard = user.CardType;
        if (enemy == userCard)
        {
            MusicSystem.Instance.PlayDrawSFX();
            EventSystem.TriggerScore(10);
            return PopUpUI.PopUpType.Draw;
        }

        if ((enemy == CardType.Rock && userCard == CardType.Paper) ||
            (enemy == CardType.Paper && userCard == CardType.Scissors) ||
            (enemy == CardType.Scissors && userCard == CardType.Rock))
        {
            MusicSystem.Instance.PlayHitSFX();
            EventSystem.TriggerScore(user.IsBooster ? 35 : 20);
            return PopUpUI.PopUpType.Win;
        }

        EventSystem.TriggerScore(-10);
        MusicSystem.Instance.PlayMissSFX();
        return PopUpUI.PopUpType.Lose;
    }
}
