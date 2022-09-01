using UnityEngine;

namespace Components
{
    public class CollectObjectComponent : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private Hero _object;
        public void OnCollectable()
        {
            _object.AddMoney(_price);
        }
    }
}
