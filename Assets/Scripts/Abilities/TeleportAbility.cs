using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class TeleportAbility : BaseAbility
{
    [SerializeField] private float teleportDistance = 0;
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

        StartCoroutine(UseAbility());
    }

    public override IEnumerator UseAbility()
    {
        isOffCooldown = false;
        var isSelectingLocation = true;
        //TODO: Set event to actually do something
        GameEvents.Instance.TeleportAbilityInUse();
        LayerMask layer = LayerMask.GetMask("Ground");

        while (isSelectingLocation)
        {
            Ray ray = abilityCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, teleportDistance, layer))
            {
                teleportMarker.SetActive(true);
                teleportMarker.transform.position = hit.point;         
            }
            else
            {
                teleportMarker.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isSelectingLocation = false;
                teleportMarker.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, teleportDistance, layer))
            {
                isSelectingLocation = false;
                transform.DOMove(teleportMarker.transform.position, 0).SetEase(Ease.Flash);
                teleportMarker.SetActive(false);
            }

            yield return null;
        }
        
        yield return new WaitForEndOfFrame();
        StartCoroutine(StartCooldown());
    }
}
