using TMPro.EditorUtilities;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public bool isHoldingRod = false;
    public int RodID;
    [SerializeField] GameObject RodObjDummy;


    void Start()
    {
        RodObjDummy.SetActive(isHoldingRod);
    }

    void Update()
    {
        
    }

    public void PickupRod()
    {
        isHoldingRod = true;
        RodObjDummy.SetActive(isHoldingRod);
    }

    public void DeliverRod()
    {
        if(isHoldingRod)
        { 
        isHoldingRod = false;
        RodObjDummy.SetActive(isHoldingRod);
        }
    }
}
