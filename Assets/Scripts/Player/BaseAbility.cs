using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    public float Cooldown { get; protected set; }
    public int Charges { get; protected set; }
    protected bool isOffCooldown = true;
    public abstract IEnumerator UseAbility();

    protected virtual IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        isOffCooldown = true;
    }
}
