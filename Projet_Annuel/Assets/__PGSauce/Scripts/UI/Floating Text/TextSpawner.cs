using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using Sirenix.OdinInspector;

namespace PGSauce.UI
{
    public class TextSpawner : PGMonoBehaviour
    {
        [SerializeField] private UIElementData uiElementData;
        
        [BoxGroup("General"),Tooltip("Whether or not this spawner can spawn at this time")]
        public bool canSpawn = true;
        [BoxGroup("General"), Tooltip("Whether or not this spawner should spawn objects on unscaled time")]
        public bool useUnscaledTime = false;
        [BoxGroup("General"), Tooltip("The prefab to spawn")]
        public FloatingTextBase floatingTextPrefab;
        
        [Tooltip("The random min and max lifetime duration for the spawned texts (in seconds)")]
        public MinMax<float> lifeTime;
        
        [BoxGroup("Movement"), Tooltip("The random position at which to spawn the text, relative to its intended spawn position")]
        public MinMax<Vector3> spawnOffset;
        [BoxGroup("Movement"),Tooltip("Whether or not to animate the movement of spawned texts")]
        public bool animateMovement = true;
        [BoxGroup("Movement"),Tooltip("Whether or not to animate the X movement of spawned texts"), ShowIf("@animateMovement")]
        public bool animateX;
        [BoxGroup("Movement"), ShowIf("@animateMovement && animateX")]
        public Animation animationX;
        [BoxGroup("Movement"),Tooltip("Whether or not to animate the Y movement of spawned texts"), ShowIf("@animateMovement")]
        public bool animateY;
        [BoxGroup("Movement"), ShowIf("@animateMovement && animateY")]
        public Animation animationY;
        [BoxGroup("Movement"),Tooltip("Whether or not to animate the Z movement of spawned texts"), ShowIf("@animateMovement")]
        public bool animateZ;
        [BoxGroup("Movement"), ShowIf("@animateMovement && animateZ")]
        public Animation animationZ;
        
        [BoxGroup("Scale")]
        public bool animateScale;

        [BoxGroup("Movement"), ShowIf("@animateScale")]
        public Animation animationScale = new Animation()
        {
            curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.15f, 1f), new Keyframe(0.85f, 1f),
                new Keyframe(1f, 0f))
        };
        
        
        private Camera _targetCamera;
        
        [Serializable]
        public class Animation
        {
            public float remapZero;
            public float remapOne;
            public AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
        }

        public Color DefaultColor => floatingTextPrefab.DefaultColor;

        public void SpawnText(string value)
        {
            if (!canSpawn)
            {
                return;
            }

            var floatingText = GetNextFloatingText();
            
            floatingText.gameObject.SetActive(true);
            floatingText.transform.position = GetWorldSpawnPosition();

            floatingText.Initialize(uiElementData, value, _targetCamera);
        }

        public void SpawnText(string value, Color color, float scale = 1)
        {
            if (!canSpawn)
            {
                return;
            }

            var floatingText = GetNextFloatingText();
            
            floatingText.gameObject.SetActive(true);
            floatingText.transform.position = GetWorldSpawnPosition();

            floatingText.Initialize(uiElementData, value, color, scale, _targetCamera);
        }

        private Vector3 GetWorldSpawnPosition()
        {
            return transform.position;
        }

        private FloatingTextBase GetNextFloatingText()
        {
            //TODO Use pooling
            return Instantiate(floatingTextPrefab);
        }

        protected void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            InstantiatePool();
            _targetCamera = Camera.main;
        }

        private void InstantiatePool()
        {
            //TODO Use pooling
        }
    }
}
