public class HurtState : EnemyState
{
    private void OnEnable()
    {
        EnemyAnimator.PlayHurt();
    }
}
