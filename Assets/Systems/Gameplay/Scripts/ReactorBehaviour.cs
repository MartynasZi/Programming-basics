using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        // Quick & dirty visual update
        for (int i = 0; i < rods.Count; i++)
        {
            rods[i].gameObject.SetActive(i < _RodsInReactor);
        }
    }




    public void LaunchRod()
    {
        GameObject newControlRod = Instantiate(_controlRodPrefab, transform);
        ControlRodBehaviour controlRodBehaviour = newControlRod.GetComponent<ControlRodBehaviour>();
        controlRodBehaviour.LaunchControlRod(_minMaxLaunchForce.x, _minMaxLaunchForce.y);
        _RodsInReactor--;
    }

    public void ReturnRod()
    {
        _RodsInReactor++;
    }

    private void InitRods()
    {
        rods = GetComponentsInChildren<StaticControlRod>().ToList();
        _RodsInReactor = rods.Count;
    }

    IEnumerator RepeatEveryTwoSeconds()
    {
        while (true)
        {
            if (_RodsInReactor > 0)
            {
                LaunchRod();
            }
            yield return new WaitForSeconds(2f);
        }
    }
}
