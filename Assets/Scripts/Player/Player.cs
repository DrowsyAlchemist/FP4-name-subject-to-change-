using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _initialhealth = 10;
    [SerializeField] private SpellCaster _spellCaster;

    private Mover _mover;

    public Health Health { get; private set; }
    public ManaStorage ManaStorage { get; private set; }

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        Health = new Health(_initialhealth);
        ManaStorage = new ManaStorage();
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