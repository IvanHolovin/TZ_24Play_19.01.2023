using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    [SerializeField] private List<GameObject> _obstacleCube;
    [SerializeField] private List<GameObject> _pickUpCubes;


    public void ResetTrack()
    {
        foreach (var yellowCube in _pickUpCubes)
        {
            yellowCube.gameObject.SetActive(true);
        }
        foreach (var redCube in _obstacleCube)
        {
            Collider redBlockCollider = redCube.GetComponent<Collider>();
            redBlockCollider.isTrigger = true;
        }
    }
}