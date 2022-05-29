using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Animancer;

namespace PGSauce.Core.Pool
{
    public class OnAnimationEndPoolable : Poolable
    {
        [SerializeField] private ClipState.Transition clip;
        [SerializeField] private AnimancerComponent animancerComponent;

        protected override void OnSpawnCustom()
        {
            base.OnSpawnCustom();
            var state = animancerComponent.Play(clip);
            state.Time = 0;
            state.Events.OnEnd = Despawn;
        }
    }
}
