using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : BaseAbility
{
    [SerializeField] private float teleportDistance = 0;
    [SerializeField] private float teleportTravelTime = 0;
    [SerializeField] private float coolDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cooldown = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKeyDown(KeyCode.Q)) { return; }

        if(!isOffCooldown) { return; }

        StartCoroutine(UseAbility()); //Need vector3 position from camera;
        //Ray ray = Camera.main.ray
    }

    public override IEnumerator UseAbility()
    {
        isOffCooldown = false;
        StartCoroutine(StartCooldown());

        transform.DOMove(transform.position + transform.forward * teleportDistance, teleportTravelTime).SetEase(Ease.Flash);
        yield return new WaitForEndOfFrame();
    }
}
