using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    public float Cooldown { get; protected set; }
    public int Charges { get; protected set; }
    public abstract IEnumerator UseAbility();
}
