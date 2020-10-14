using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    static GameEvents instance;

    public static GameEvents Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameEvents>();
            }
            return instance;
        }
    }

    private void Awake() => instance = this;

    public event Action OnTeleportAbilityInUse;
    public void TeleportAbilityInUse() => OnTeleportAbilityInUse?.Invoke();
}
