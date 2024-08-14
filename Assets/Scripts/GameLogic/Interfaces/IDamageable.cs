namespace Assets.Scripts.Interfaces
{
    internal interface IDamageable
    {
        public float Health { get; }
        public bool IsEnemy { get; }

        public void TakeDamage(float damage);
    }
}