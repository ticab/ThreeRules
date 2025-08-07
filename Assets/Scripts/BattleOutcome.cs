using UnityEngine;

public static class BattleOutcome
{
    public static PopUp.PopUpType DetermineOutcome(CardType enemy, CardType user)
    {
        if (enemy == user)
            return PopUp.PopUpType.Draw;

        if ((enemy == CardType.Rock && user == CardType.Paper) ||
            (enemy == CardType.Paper && user == CardType.Scissors) ||
            (enemy == CardType.Scissors && user == CardType.Rock))
        {
            return PopUp.PopUpType.Win;
        }

        return PopUp.PopUpType.Lose;
    }
}
