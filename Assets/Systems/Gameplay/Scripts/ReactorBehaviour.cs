using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBehaviour : MonoBehaviour
{
    public GameObject _controlRodPrefab;

    [Header("Control Rod Settings")]
    [SerializeField] private Vector2 _minMaxLaunchForce = new Vector2(5f, 10f);

    private void Start()
    {        
    }

    private void Update()
    {        
    }

    public void LaunchRod()
    {
        GameObject newControlRod;
        ControlRodBehaviour controlRodBehaviour;
        
        newControlRod = Instantiate(_controlRodPrefab, transform);                          //Make the newControlRod the one we are instantiating.
        controlRodBehaviour = newControlRod.GetComponent<ControlRodBehaviour>();            //Get the ControlRod component.

        controlRodBehaviour.LaunchControlRod(_minMaxLaunchForce.x, _minMaxLaunchForce.y);   //Finally launch the rod with the random values our controller dictates.
    }

}
