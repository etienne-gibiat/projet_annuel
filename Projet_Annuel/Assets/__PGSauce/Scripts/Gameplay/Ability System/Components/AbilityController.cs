using System;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using PGSauce.Gameplay.AbilitySystem.Ability_System;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem
{
    public class AbilityController : MonoBehaviour
    {
        [SerializeField] private AbilityData[] abilities;
        [SerializeField] private InitializeStatsAbilityData[] abilityInitialization;
        [SerializeField] private Ability_System.AbilitySystem abilitySystem;

        private AbilitySpec[] _abilitySpecs;

        private void Start()
        {
            ActivateInitialisationAbilities();
            GrantCastableAbilities();
        }

        private void Update()
        {
            Debug.Log($"I am {name}");
            
            if (Input.GetMouseButtonDown(0))
            {
                UseAbility(0);
            }

            foreach (var abilitySpec in _abilitySpecs)
            {
                Debug.Log($"Cooldown for {abilitySpec.Ability.name} is {abilitySpec.CheckCooldown().TimeRemaining}");
            }

            foreach (var attributeValue in abilitySystem.AttributesManagerInstance.AttributeValuesReadOnly)
            {
                Debug.Log($"Current value for {attributeValue.attribute.name} is {attributeValue.CurrentValue}");
            }
        }

        public void UseAbility(int i)
        {
            if (_abilitySpecs.OutOfRange(i))
            {
                return;
            }
            var spec = _abilitySpecs[i];
            StartCoroutine(spec.TryActivateAbility(() => {}));
        }

        private void GrantCastableAbilities()
        {
            _abilitySpecs = new AbilitySpec[abilities.Length];
            for (int i = 0; i < abilities.Length; i++)
            {
                var spec = abilitySystem.GrantAbility(abilities[i]);
                _abilitySpecs[i] = spec;
            }
        }

        private void ActivateInitialisationAbilities()
        {
            foreach (var abilityData in abilityInitialization)
            {
                var spec = abilitySystem.GrantAbility(abilityData);
                StartCoroutine(spec.TryActivateAbility(() => {}));
            }
        }
        
        
    }
}