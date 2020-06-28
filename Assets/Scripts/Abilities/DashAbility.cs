using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DashAbility : BaseAbility
{
    [SerializeField] private float dashDistance = 0;
    [SerializeField] private float dashTravelTime = 0;
    [SerializeField] private float coolDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cooldown = coolDown;
        isOffCooldown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.E)) { return; }

        if(!isOffCooldown) { return; }

        StartCoroutine(UseAbility());    
    }

    //Set up event to fire when this is used for effects?
    public override IEnumerator UseAbility()
    {
        isOffCooldown = false;
        StartCoroutine(StartCooldown());
        //Add another tween that changes rotation is I go with a camera facing dash instead of character facing dash
        transform.DOMove(transform.position + transform.forward * dashDistance, dashTravelTime).SetEase(Ease.Flash);
        //placeholder yield, maybe make it based on the charge time to line up with special effects?
        yield return new WaitForEndOfFrame();
    }
}
