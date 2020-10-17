using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera = null;
    [SerializeField] private GameObject aimCamera = null;
    [SerializeField] private GameObject aimReticle = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            StartCoroutine(ShowReticle());
        }
        else
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
            aimReticle.SetActive(false);
        }
    }

    IEnumerator ShowReticle()
    {
        yield return new WaitForSeconds(0.5f);
        aimReticle.SetActive(true);
    }
}
