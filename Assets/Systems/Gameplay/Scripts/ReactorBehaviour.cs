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
    [SerializeField] private float _instabilityMultiplier = 0.25f;
    [Range(0f, 1f)] public float Overheat = 0f;

    [Header("Runtime Info")]
    public int _RodsInReactor;
    public List<StaticControlRod> rods;

    // --- SCORE SYSTEM ---
    [Header("Score System")]
    public float Score;                 // total score
    private int _rodsReturnedTotal;     // total rods brought back
    private float _timeAlive;           // time survived
    [SerializeField] private float _scorePerSecond = 10f;
    [SerializeField] private int _scorePerRod = 250;

    private float _timeSinceStart;
    private bool _isMeltdown = false;

    private GameManager gm;

    private void Start()
    {
        InitRods();
        gm = FindAnyObjectByType<GameManager>();
        StartCoroutine(ReactorLoop());
    }

    private void Update()
    {
        if (_isMeltdown)
            return;

        // --- Score grows by survival time ---
        _timeAlive += Time.deltaTime;
        Score += _scorePerSecond * Time.deltaTime;

        // Update rod visuals
        for (int i = 0; i < rods.Count; i++)
            rods[i].gameObject.SetActive(i < _RodsInReactor);

        if (rods.Count == 0) return;

        int rodsMissing = Mathf.Clamp(rods.Count - _RodsInReactor, 0, rods.Count);
        if (_RodsInReactor <= 0)
            rodsMissing = rods.Count;

        float missingRatio = (float)rodsMissing / rods.Count;
        Overheat += missingRatio * _instabilityMultiplier * Time.deltaTime;

        if (rodsMissing == 0)
            Overheat -= 0.01f * Time.deltaTime;

        Overheat = Mathf.Clamp01(Overheat);

        if (Overheat >= 1f)
            TriggerMeltdown("Reactor overheated!");

        _timeSinceStart += Time.deltaTime;

        var ph = FindAnyObjectByType<PlayerHealthScript>();
        if (ph != null) ph.TakeDamage(Overheat);

        gm.TotalScore = Score;
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

        Overheat = Mathf.Max(0f, Overheat - 0.1f);

        // --- Add score for successful return ---
        _rodsReturnedTotal++;
        Score += _scorePerRod;
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

        StopAllCoroutines();
        StartCoroutine(EjectRemainingRodsBurst(0.05f));

        Overheat = 1f;
        Debug.LogWarning($"!!! MELTDOWN !!! — {reason}");
        Debug.Log($"Final Score: {Mathf.RoundToInt(Score)} (Rods Returned: {_rodsReturnedTotal}, Time Alive: {_timeAlive:F1}s)");
    }

    private IEnumerator EjectRemainingRodsBurst(float delayBetween = 0.05f)
    {
        while (_RodsInReactor > 0)
        {
            LaunchRod();
            _RodsInReactor--;
            yield return new WaitForSeconds(delayBetween);
        }

        for (int i = 0; i < rods.Count; i++)
            rods[i].gameObject.SetActive(false);
    }
}
