using System;

public class EventSystem 
{
    public static event Action OnPlay;
    public static event Action OnStartGame;
    public static event Action<int> OnScore;
    public static event Action<int> OnTimerFinished;
    public static event Action OnHighScoresChanged;
    public static event Action OnHandEmptied;

    public static void TriggerCountdownStart()
    {
        OnPlay?.Invoke();
    }
    public static void TriggerGameStart()
    {
        OnStartGame?.Invoke();
    }
    public static void TriggerScore(int amount)
    {
        OnScore?.Invoke(amount);
    }
    public static void TriggerHighScoresUI()
    {
        OnHighScoresChanged?.Invoke();
    }

    public static void TriggerEmptyHand()
    {
        OnHandEmptied?.Invoke();
    }
}