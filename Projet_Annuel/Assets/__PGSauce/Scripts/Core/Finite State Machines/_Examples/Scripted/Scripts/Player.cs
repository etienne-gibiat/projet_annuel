using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core.FSM.Base;

namespace PGSauce.Core.FSM.Scripted.Example
{
    public class Player : MonoStateMachineScripted<Player>
    {
        private PlayerState _walk;
        private PlayerState _run;
        private PlayerState _dead;
        private PlayerState _immortal;

        private bool IsWalking { get; set; }
        private bool IsDead { get; set; }
        private bool IsImmortal { get; set; }

        protected override StateBase<Player> InitialState => _run;

        protected override void InitializeStates()
        {
            _walk = new WalkState();
            _run = new RunState();
            _dead = new DeadState();
            _immortal = new ImmortalState();
        }

        protected override void CreateTransitions()
        {
            AddNewTransition(_walk, _run, () => !IsWalking, false);
            AddNewTransition(_run, _walk, () => IsWalking, false);
            AddNewTransition(_run, _immortal, () => IsImmortal, false);
            AddNewTransition(_walk, _immortal, () => IsImmortal, false);
            
            AddAnyTransition(_dead, () => IsDead, false, new List<StateBase<Player>>() {_immortal});
        }

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
