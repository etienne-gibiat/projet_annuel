using UnityEngine;

namespace PGSauce.Core.FSM.WithSo.Example
{
    public class Player : MonoStateMachineWithSo<Player>
    {
        public bool IsWalking { get; set; }
        public bool IsDead { get; set; }
        public bool IsImmortal { get; set; }
        
        protected override void BeforeFsmUpdate()
        {
            base.BeforeFsmUpdate();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsWalking = !IsWalking;
            }
            
            if (Input.GetKeyDown(KeyCode.D))
            {
                IsDead = true;
            }
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                IsImmortal = true;
            }
        }
    }
}