using UnityEngine;

namespace Components
{
    public class CollectObjectComponent : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private Hero _hero;
        public void OnCollectable()
        {
            _hero.AddMoney(_price);
        }
    }
}
