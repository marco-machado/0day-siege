using ZeroDaySiege.Enemies;

namespace ZeroDaySiege.Core
{
    public readonly struct EnemySpawn
    {
        public readonly EnemyType Type;
        public readonly float SpawnX;
        public readonly float SpawnTime;

        public EnemySpawn(EnemyType type, float spawnX, float spawnTime)
        {
            Type = type;
            SpawnX = spawnX;
            SpawnTime = spawnTime;
        }
    }

    public readonly struct WaveDefinition
    {
        public readonly int WaveNumber;
        public readonly bool IsBoss;
        public readonly EnemySpawn[] Enemies;

        public WaveDefinition(int waveNumber, bool isBoss, EnemySpawn[] enemies)
        {
            WaveNumber = waveNumber;
            IsBoss = isBoss;
            Enemies = enemies;
        }
    }
}
