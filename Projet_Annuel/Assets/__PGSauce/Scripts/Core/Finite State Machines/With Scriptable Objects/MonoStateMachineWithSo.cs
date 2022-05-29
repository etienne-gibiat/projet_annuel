using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.FSM.Base;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// The MonoBehaviour base class to all SO FSMs
    /// </summary>
    /// <typeparam name="T">Type of the FSM</typeparam>
    [HelpURL(DocsPaths.MonoStateMachineWithSo)]
    public abstract class MonoStateMachineWithSo<T> : MonoStateMachineBase<T>, IMonoStateMachineWithSo where T : MonoStateMachineWithSo<T>
    {
        [SerializeField] private SoState<T> initialState;
        [SerializeField] private SoAny<T> any;
        
        private HashSet<SoState<T>> _allStates;
        private HashSet<SoState<T>> _visitedStates;
        private Dictionary<SoState<T>, StateWithSo<T>> _soToState;
        private Dictionary<StateBase<T>, SoState<T>> _stateToSo;
        private List<AnyTransitionBase<T>> _anyTransition;
        
        /// <summary>
        /// Initial State
        /// </summary>
        protected override StateBase<T> InitialState => _soToState == null ? new StateWithSo<T>(initialState): _soToState[initialState];
        /// <summary>
        /// Any transition list
        /// </summary>
        protected override List<AnyTransitionBase<T>> AnyTransitions => _anyTransition;
        
        /// <summary>
        /// Converts a SO State (Scriptable Object) to an abstract state
        /// </summary>
        /// <param name="soState">The Scriptable Object</param>
        /// <returns></returns>
        public StateBase<T> GetState(SoState<T> soState)
        {
            if (!_soToState.ContainsKey(soState))
            {
                AddNewStateToDict(soState);
            }
            return _soToState[soState];
        }

        protected sealed override void InitFsm()
        {
            base.InitFsm();
            _allStates = new HashSet<SoState<T>>();
            _visitedStates = new HashSet<SoState<T>>();
            _soToState = new Dictionary<SoState<T>, StateWithSo<T>>();
            _stateToSo = new Dictionary<StateBase<T>, SoState<T>>();
            FindAllStates(initialState);
            foreach (var soState in _allStates)
            {
                foreach (var transition in soState.Transitions)
                {
                    var t = transition.to;

                    if (!t.IsNullState)
                    {
                        // Create transition from state to t
                        var stateTransition = new TransitionWithSo<T>(_soToState[t], transition.decision, transition.reverseValue, transition.allowLoop);
                        _soToState[soState].Transitions.Add(stateTransition);
                    }
                }
            }
            _anyTransition = any.GetTransitions(this);
        }

        private void FindAllStates(SoState<T> soSoStateBase)
        {
            if (soSoStateBase.IsNullState) { return; }
            if (_visitedStates.Contains(soSoStateBase)) { return; }

            _allStates.Add(soSoStateBase);
            _visitedStates.Add(soSoStateBase);
            if (!_soToState.ContainsKey(soSoStateBase))
            {
                AddNewStateToDict(soSoStateBase);
            }

            foreach (var transition in soSoStateBase.Transitions)
            {
                var t = transition.to;
                if (!t.IsNullState)
                {
                    if (!_soToState.ContainsKey(t))
                    {
                        AddNewStateToDict(t);
                    }
                    _allStates.Add(t);
                }

                FindAllStates(t);
            }
        }

        private void AddNewStateToDict(SoState<T> soSoStateBase)
        {
            var state = new StateWithSo<T>(soSoStateBase) {debugIndex = soSoStateBase.debugIndex};
            _soToState[soSoStateBase] = state;
            _stateToSo[state] = soSoStateBase;
        }
    }
}