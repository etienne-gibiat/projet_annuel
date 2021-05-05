using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private vThirdPersonInput Player;
    [Header("Dégats élémentaires")]
    public float FireDamage; // Dégats de feu
    [Header("Sorts")]
    public GameObject spellFireBall;
    public GameObject spellWater;
    public GameObject spellgrounds;
    public GameObject spells;
    private float baseDamage;
    public float damageSupp;
    private void Start()
    {
        baseDamage = FireDamage;
        Player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<vThirdPersonInput>();
    }
    private void Update()
    {

    }
    public void UpdateXp(int xp)
    {
        FireDamage = baseDamage + (baseDamage * (xp*0.2f)) + damageSupp;
    }


}
