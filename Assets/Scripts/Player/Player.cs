using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _initialhealth = 10;
    [SerializeField] private SpellCaster _spellCaster;
    [SerializeField] private HealthRenderer _healthRenderer;

    private Mover _mover;

    public Health Health { get; private set; }
    public Health ManaStorage { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        Health = new Health(_initialhealth);
        _healthRenderer.Render(Health);
    }

    public void Play()
    {
        _mover.StartMovement();
        _spellCaster.StartCasting();
    }

    public void StopPlaying()
    {
        _mover.StopMovement();
        _spellCaster.StopCasting();
    }
}