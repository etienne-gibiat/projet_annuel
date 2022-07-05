namespace _Elementis.Scripts.Lava_Dungeon
{
    using UnityEngine;
    public class FightTrigger : MonoBehaviour
    {
        private bool _triggered;

        private void Awake()
        {
            _triggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == Layers.PLAYER)
            {
                if (!_triggered)
                {
                    _triggered = true;
                    LavaDungeonManager.Instance.TriggerFight();
                }
            }
        }
    }
}