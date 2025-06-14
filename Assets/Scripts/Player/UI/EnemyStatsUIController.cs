using System;
using Manager;
using UnityEngine;

namespace Player.UI
{
    public class EnemyStatsUIController: MonoBehaviour
    {
        [SerializeField] private StatsItem _health;
        [SerializeField] private StatsItem _damage;


        public void InitializeStats(EnemyStats enemyStats)
        {
            _health.SetStat(enemyStats.MaxHealth, enemyStats.Health);
            _damage.SetStat(enemyStats.MaxDamage(), enemyStats.MinDamage());
        }
    }
}