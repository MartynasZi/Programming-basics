using System.Collections.Generic;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [Header("Light Settings")]
    [SerializeField] private float minIntensity = 0f;
    [SerializeField] private float maxIntensity = 5f;
    [SerializeField] private float speed = 2f; // how fast it pulses

    private List<Light> _alarmLights = new List<Light>();
    private float _timeOffset;

    private void Start()
    {
        // find all Light components in children
        _alarmLights.AddRange(GetComponentsInChildren<Light>());

        // random offset to desync multiple alarms
        _timeOffset = Random.Range(0f, 100f);
    }

    private void Update()
    {
        float t = (Mathf.Sin((Time.time + _timeOffset) * speed) + 1f) / 2f;
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, t);

        foreach (var l in _alarmLights)
        {
            if (l != null)
                l.intensity = intensity;
        }
    }
}
