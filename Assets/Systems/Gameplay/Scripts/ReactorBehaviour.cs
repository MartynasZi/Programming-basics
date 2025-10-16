using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBehaviour : MonoBehaviour
{
    public GameObject _controlRodPrefab;

    [Header("Control Rod Settings")]
    [SerializeField] private Vector2 _minMaxLaunchForce = new Vector2(8f, 12f);

    public int _RodsInReactor;


    private void Start()
    {        
        InitRods();
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

    private void InitRods()
    {
        //Method called at the beginning when all Rods are still present. We count how many rods are present and assign an ID to each Rod.
        int RodID = 1;

        foreach (StaticControlRod rod in gameObject.GetComponentsInChildren<StaticControlRod>()) 
        { 
            rod._ControlRodID = RodID;
            rod._InReactor = true;

            _RodsInReactor = RodID;
            RodID++;
        }
    }


}
