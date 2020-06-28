using DG.Tweening;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Goes from world origin point to whatever distance from it, not current position but where player spawned? 
            transform.DOMove(transform.position , 1f);
        }
    }
}
