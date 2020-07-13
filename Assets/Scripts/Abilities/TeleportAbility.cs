using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

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
        teleportMarker = Instantiate(teleportMarker);
        teleportMarker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetKeyDown(KeyCode.Q)) { return; }

        if(!isOffCooldown) { return; }

        StartCoroutine(UseAbility()); //Need vector3 position from camera;
    }

    public override IEnumerator UseAbility()
    {
        isOffCooldown = false;
        var isSelectingLocation = true;
        LayerMask layer = LayerMask.GetMask("Ground");

        while (isSelectingLocation)
        {
            Ray ray = abilityCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(abilityCamera.transform.position, abilityCamera.transform.forward*15f, Color.red);
            Debug.Log("In the loop");

            if (Physics.Raycast(ray, out RaycastHit hit, 15f, layer))
            {
                Debug.Log("RayCast Hit Somthing");
                teleportMarker.SetActive(true);
                teleportMarker.transform.position = hit.point;         
            }
            else
            {
                Debug.Log("Raycast hitting nothing");
                teleportMarker.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("Right mouse clicked");
                isSelectingLocation = false;
                teleportMarker.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isSelectingLocation = false;
                transform.DOMove(teleportMarker.transform.position, 0).SetEase(Ease.Flash);
                teleportMarker.SetActive(false);
            }

            yield return null;
        }
        
        //transform.DOMove(transform.position + transform.forward * teleportDistance, teleportTravelTime).SetEase(Ease.Flash);
        yield return new WaitForEndOfFrame();
        StartCoroutine(StartCooldown());
    }
}
