using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _damage;
        [SerializeField] private UnityEvent _die;

        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            _damage?.Invoke();
            if (_health <=0)
                _die?.Invoke();
        }
    }
}
