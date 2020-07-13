using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        var isSelectingLocation = true;
        GameObject teleporterMarker = Instantiate(teleportMarker);

        Ray ray = abilityCamera.ScreenPointToRay(Input.mousePosition);

        while (isSelectingLocation)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 15f))
            {
                teleporterMarker.transform.position = hit.point;         
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isSelectingLocation = false;
            }
        }
        
        //transform.DOMove(transform.position + transform.forward * teleportDistance, teleportTravelTime).SetEase(Ease.Flash);
        yield return new WaitForEndOfFrame();
        StartCoroutine(StartCooldown());
    }
}
