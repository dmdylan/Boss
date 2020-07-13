using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : BaseAbility
{
    [SerializeField] private float teleportDistance = 0;
    [SerializeField] private float teleportTravelTime = 0;
    [SerializeField] private float coolDown = 0;
    [SerializeField] private GameObject teleportMarker = null;
    private Camera abilityCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cooldown = coolDown;
        abilityCamera = Camera.main;
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

        Ray ray = abilityCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 15f))
        {
            GameObject teleporterMarker = Instantiate(teleportMarker);
            //this code crashes unity
            while (hit.collider)
            {
                teleporterMarker.transform.position = hit.point;      
            }
        }


        //transform.DOMove(transform.position + transform.forward * teleportDistance, teleportTravelTime).SetEase(Ease.Flash);
        yield return new WaitForEndOfFrame();
        StartCoroutine(StartCooldown());
    }
}
