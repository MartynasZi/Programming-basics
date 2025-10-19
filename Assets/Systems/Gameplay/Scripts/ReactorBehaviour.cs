using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactorBehaviour : MonoBehaviour
{
    [Header("Prefabs & References")]
    [SerializeField] private GameObject _controlRodPrefab;

    [Header("Control Rod Settings")]
    [SerializeField] private Vector2 _minMaxLaunchForce = new Vector2(8f, 12f);

    [Header("Difficulty Settings")]
    [SerializeField] private float _launchIntervalStart = 2f;
    [SerializeField] private float _launchIntervalMin = 0.3f;
    [SerializeField] private float _difficultyRampTime = 60f;

    [Header("Instability & Overheat Settings")]
    [SerializeField] private float _instabilityMultiplier = 0.25f; // how strongly missing rods affect heat
    [Range(0f, 1f)] public float Overheat = 0f; // 0 = stable, 1 = meltdown

    [Header("Runtime Info")]
    public int _RodsInReactor;
    public List<StaticControlRod> rods;

    private float _timeSinceStart;
    private bool _isMeltdown = false;

    private void Start()
    {
        InitRods();
        StartCoroutine(ReactorLoop());
    }

    private void Update()
    {
        if (_isMeltdown)
            return;

        // Update rod visuals
        for (int i = 0; i < rods.Count; i++)
            rods[i].gameObject.SetActive(i < _RodsInReactor);

        if (rods.Count == 0) return;

        // Determine missing rods
        int rodsMissing = Mathf.Clamp(rods.Count - _RodsInReactor, 0, rods.Count);

        // If all rods are gone, treat as maximum instability
        if (_RodsInReactor <= 0)
            rodsMissing = rods.Count;

        // Progressive overheating: the more rods missing, the faster we heat
        float missingRatio = (float)rodsMissing / rods.Count;
        Overheat += missingRatio * _instabilityMultiplier * Time.deltaTime;

        // Small cooling effect when fully stable
        if (rodsMissing == 0)
            Overheat -= 0.01f * Time.deltaTime;

        Overheat = Mathf.Clamp01(Overheat);

        // Trigger meltdown only once Overheat reaches 1
        if (Overheat >= 1f)
        {
            TriggerMeltdown("Reactor overheated!");
        }

        _timeSinceStart += Time.deltaTime;

        // Dirty damage hookup
        var ph = FindAnyObjectByType<PlayerHealthScript>();
        if (ph != null) ph.TakeDamage(Overheat);
    }

    IEnumerator ReactorLoop()
    {
        while (!_isMeltdown)
        {
            if (_RodsInReactor > 0)
            {
                LaunchRod();
                _RodsInReactor--;
            }

            // Difficulty ramp: shorter intervals over time
            float t = Mathf.Clamp01(_timeSinceStart / _difficultyRampTime);
            float interval = Mathf.Lerp(_launchIntervalStart, _launchIntervalMin, t);

            yield return new WaitForSeconds(interval);
        }
    }

    public void LaunchRod()
    {
        GameObject newControlRod = Instantiate(_controlRodPrefab, transform);
        ControlRodBehaviour controlRodBehaviour = newControlRod.GetComponent<ControlRodBehaviour>();
        controlRodBehaviour.LaunchControlRod(_minMaxLaunchForce.x, _minMaxLaunchForce.y);
    }

    public void ReturnRod()
    {
        if (_isMeltdown) return;
        _RodsInReactor = Mathf.Min(_RodsInReactor + 1, rods.Count);

        Overheat -= 0.1f;
        if (Overheat < 0) Overheat = 0;
    }

    private void InitRods()
    {
        rods = GetComponentsInChildren<StaticControlRod>().ToList();
        _RodsInReactor = rods.Count;
    }

    private void TriggerMeltdown(string reason)
    {
        if (_isMeltdown) return;
        _isMeltdown = true;

        // Stop spawning, then eject remaining rods in a burst
        StopAllCoroutines();
        StartCoroutine(EjectRemainingRodsBurst(0.05f)); // tweak delay for effect

        Overheat = 1f; // ensure full overheat value
        Debug.LogWarning($"!!! MELTDOWN !!! — {reason}");
    }

    private IEnumerator EjectRemainingRodsBurst(float delayBetween = 0.05f)
    {
        // Launch as many times as there are rods left
        while (_RodsInReactor > 0)
        {
            LaunchRod();
            _RodsInReactor--;
            yield return new WaitForSeconds(delayBetween);
        }

        // Final visual cleanup: hide all static rods
        for (int i = 0; i < rods.Count; i++)
            rods[i].gameObject.SetActive(false);
    }
}
