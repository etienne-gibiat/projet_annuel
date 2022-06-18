using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace _Elementis.Scripts
{
    public class InterruptorPuzzle : MonoBehaviour
    {
        public UnityEvent onPuzzleFinished;
        
        private HashSet<IPuzzlePiece> _interruptors;
        private bool _isFinished;

        private void Awake()
        {
            _interruptors = GetComponentsInChildren<IPuzzlePiece>().ToHashSet();
            foreach (var interruptor in _interruptors)
            {
                interruptor.SetPuzzle(this);
            }

            _isFinished = _interruptors.Count == 0;
        }

        public void OnInterruptorActivation(IPuzzlePiece interruptor)
        {
            if (_isFinished || !_interruptors.Contains(interruptor)) {return;}
            if (!_interruptors.All(inte => inte.IsActivated)) {return;}
            onPuzzleFinished.Invoke();
            _isFinished = true;
        }
    }
}