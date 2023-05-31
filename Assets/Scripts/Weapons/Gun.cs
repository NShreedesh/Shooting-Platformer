using PickupScripts;
using UnityEngine;

namespace Weapons
{
    public class Gun : PickableItem
    {
        [Header("Components")]
        private BoxCollider2D boxCollider;

        private void Awake()
        {
            boxCollider= GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            ToggleCollider();
        }

        public override void Pick()
        {
            base.Pick();
            ToggleCollider();
        }

        private void ToggleCollider()
        {
            if (IsPicked)
                boxCollider.enabled = false;
            else
                boxCollider.enabled = true;
        }
    }
}
