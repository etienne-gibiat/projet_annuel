using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.FSM.Base;
using PGSauce.Core.FSM.Scripted;
using PGSauce.Core.PGDebugging;

namespace PGSauce.Core.Utilities
{
    /// <summary>
    /// Used for selecting objects from a list. For example you have a previous and next button, when you click on it, it changes the current object
    /// The children classes decide how to display the change
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Selector<T> : MonoStateMachineScripted<Selector<T>>
    {
        private SelectorState<T> _focused;
        private SelectorState<T> _transitionning;

        private bool _beginTransition;

        
        public bool BeginTransition
        {
            get => HasTransitions && _beginTransition;
            set => _beginTransition = HasTransitions && value;
        }

        protected sealed override StateBase<Selector<T>> InitialState => _focused;
        
        /// <summary>
        /// The object being focused
        /// </summary>
        protected T CurrentObject => Objects[CurrentIndex];

        /// <summary>
        /// The previous object
        /// </summary>
        protected T PreviousObject => Objects[PreviousIndex];

        /// <summary>
        /// The next object
        /// </summary>
        protected T NextObject => Objects[NextIndex];

        /// <summary>
        /// The next index, taking cycling into account
        /// </summary>
        protected int NextIndex
        {
            get
            {
                var index = CurrentIndex + 1;
                if (Cyclic)
                {
                    index %= Objects.Count;
                }
                else
                {
                    index = Mathf.Clamp(index,0, Objects.Count - 1);
                }

                return index;
            }
        }

        /// <summary>
        /// The previous index, taking cycling into account
        /// </summary>
        protected int PreviousIndex {
            get
            {
                var index = CurrentIndex - 1;
                if (Cyclic)
                {
                    if (index < 0)
                    {
                        index += Objects.Count;
                    }
                }
                else
                {
                    index = Mathf.Clamp(index,0, Objects.Count - 1);
                }

                return index;
            }
        }

        /// <summary>
        /// Set to true if the selector has transitions (like animations), else set to false
        /// </summary>
        protected abstract bool HasTransitions { get; }

        /// <summary>
        /// The selector has finished transitioning, it is doing nothing
        /// </summary>
        public bool Focused => Fsm.CurrentState.Equals(_focused);

        /// <summary>
        /// All the available objects
        /// </summary>
        protected abstract List<T> Objects
        {
            get;
            set;
        }

        protected sealed override void CustomInit()
        {
            base.CustomInit();
        }

        protected void OnEnable()
        {
            CustomOnEnable();
        }

        protected sealed override void InitializeStates()
        {
            _focused = new FocusedSelectorState<T>();
            _transitionning = new TransitioningSelectorState<T>();
        }

        protected sealed override void CreateTransitions()
        {
            AddNewTransition(_focused, _transitionning, ShouldBeginTransition, false);
            AddNewTransition(_transitionning, _focused, FinishedTransitioning, false);
        }

        /// <summary>
        /// If the selector has transitions, set this to true when the transition is finished, useless if !HasTransitions
        /// </summary>
        protected bool CustomFinishedTransitioning { get; set; } = true;
        /// <summary>
        /// Once we reach the end of the list, should we go back to index 0 ?
        /// </summary>
        protected abstract bool Cyclic { get; }

        /// <summary>
        /// The index of the focused object
        /// </summary>
        protected int CurrentIndex { get; set; }

        /// <summary>
        /// Called when we click the previous button
        /// </summary>
        protected virtual void OnPreviousObject(){}
        /// <summary>
        /// Called when we click the next button
        /// </summary>
        protected virtual void OnNextObject(){}
        /// <summary>
        /// Called when we click either the previous or next button
        /// </summary>
        protected virtual void OnObjectChanged(){}
        /// <summary>
        /// Called when the selector is Enabled in the hierarchy
        /// </summary>
        protected abstract void CustomOnEnable();

        /// <summary>
        /// The Method to call when we want to go to next object
        /// </summary>
        protected void SelectNextObject()
        {
            if(!Focused) {return;}

            CurrentIndex = NextIndex;

            OnNextObject();
            OnObjectChanged();

            BeginTransition = true;
        }

        /// <summary>
        /// The Method to call when we want to go to previous object
        /// </summary>
        protected void SelectPreviousObject()
        {
            if(!Focused) {return;}

            CurrentIndex = PreviousIndex;
            
            OnPreviousObject();
            OnObjectChanged();
            
            BeginTransition = true;
        }

        /// <summary>
        /// Called when the object begins being focused
        /// </summary>
        public virtual void OnEnterFocusedState()
        {
        }

        /// <summary>
        /// Called When the object is being focused in update loop
        /// </summary>
        public virtual void OnUpdateFocusedState()
        {
        }
        
        /// <summary>
        /// Called when the object ends being focused (we switch to a different object)
        /// </summary>
        public virtual void OnExitFocusedState()
        {
        }

        /// <summary>
        /// Called when we begin to switch to another object
        /// </summary>
        public virtual void OnEnterTransitioningState()
        {
        }

        /// <summary>
        /// Called when we are switching to another object in update loop
        /// </summary>
        public virtual void OnUpdateTransitioningState()
        {
        }
        
        /// <summary>
        /// Called when we finished switching to another object
        /// </summary>
        public virtual void OnExitTransitioningState()
        {
        }
        
        private bool FinishedTransitioning()
        {
            return !HasTransitions || CustomFinishedTransitioning;
        }

        private bool ShouldBeginTransition()
        {
            return BeginTransition;
        }
    }
}
