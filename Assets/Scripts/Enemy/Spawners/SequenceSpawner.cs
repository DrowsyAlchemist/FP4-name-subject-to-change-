using System;
using UnityEngine;

public abstract class SequenceSpawner : MonoBehaviour
{
    private ISpawnSequence _currentSequence;
    private float _elapsedTime;
    private bool _isFinished = true;

    protected int SpawnedSequencesCount { get; private set; }
    protected bool CanSpawnNext => _elapsedTime > _currentSequence.SpawnDelay;
    protected bool HasNext => SpawnedSequencesCount < _currentSequence.Count;

    public event Action SequenceFinished;

    protected virtual void Awake()
    {
        enabled = false;
    }

    protected void Update()
    {
        if (HasNext == false)
            FinishSpawning();

        _elapsedTime += Time.deltaTime;

        if (CanSpawnNext && HasNext)
        {
            _elapsedTime = 0;
            SpawnNext();
            SpawnedSequencesCount++;
            enabled = false;
        }
    }

    protected abstract void SpawnNext();

    protected void SpawnSequence(ISpawnSequence sequence)
    {
        _currentSequence = sequence ?? throw new ArgumentNullException();

        if (_isFinished == false)
            throw new InvalidOperationException("Previous sequence is not finished.");

        SpawnedSequencesCount = 0;
        _elapsedTime = sequence.SpawnDelay;
        _isFinished = false;
        enabled = true;
    }

    private void FinishSpawning()
    {
        enabled = false;
        SequenceFinished?.Invoke();
        _isFinished = true;
    }
}