using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReactorBehaviour : MonoBehaviour
{
    public GameObject _controlRodPrefab;

    [Header("Control Rod Settings")]
    [SerializeField] private Vector2 _minMaxLaunchForce = new Vector2(8f, 12f);

    public int _RodsInReactor;
    public List<StaticControlRod> rods;



    private void Start()
    {        
        InitRods();
        
        StartCoroutine(RepeatEveryTwoSeconds());
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

    public void ReturnRod()
    {
        _RodsInReactor++;
    }






    private void InitRods()
    {
        int RodID = 1;

        foreach (StaticControlRod rod in gameObject.GetComponentsInChildren<StaticControlRod>())
        {
            rod._ControlRodID = RodID;
            rod._InReactor = true;

            _RodsInReactor = RodID;
            RodID++;
        }
            rods = GetComponentsInChildren<StaticControlRod>().ToList();
    }










    //Testing ATM
    IEnumerator RepeatEveryTwoSeconds()
    {
        while (true)
        {
            if (_RodsInReactor > 0)
            {
                LaunchRod();
                _RodsInReactor -= 1;
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
