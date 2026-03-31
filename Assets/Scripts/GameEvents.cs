using System;
using UnityEngine;

public class GameEvents
{
    public static Action<int> OnPlayerHurt;
    public static Action OnTryToInteract;
    public static Action OnQuestAccept;
    public static Action OnQuestFinish;
}
