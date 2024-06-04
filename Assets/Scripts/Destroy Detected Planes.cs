using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class DestroyDetectedPlanes : MonoBehaviour
{
    private ARPlaneManager arPlaneManager;

    private void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        StartCoroutine(DestroyPlanesCoroutine());
    }

    private IEnumerator DestroyPlanesCoroutine()
    {
        while (true)
        {
            // Wait for 4 seconds
            yield return new WaitForSeconds(20f);

            // Destroy all detected planes
            foreach (var plane in arPlaneManager.trackables)
            {
                Destroy(plane.gameObject);
            }

            // Wait for 30 seconds before restarting the coroutine
            yield return new WaitForSeconds(15f);
        }
    }
}
