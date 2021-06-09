using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private vThirdPersonInput Player;
    [Header("Dégats élémentaires")]
    public float FireDamage; // Dégats de feu
    public float WaterDamage;
    [Header("Sorts")]
    public GameObject spellFeu;
    public GameObject spellEau;
    public GameObject spellTerre;
    public GameObject spellAir;
    private float baseDamageFire;
    private float baseDamageWater;
    public float damageSupp;
    private void Start()
    {
        baseDamageFire = FireDamage;
        baseDamageWater = WaterDamage;
        Player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<vThirdPersonInput>();
    }
    private void Update()
    {

    }
    public void UpdateXp(int xp)
    {
        WaterDamage = baseDamageWater + (baseDamageWater * (xp * 0.3f)) + damageSupp;
        FireDamage = baseDamageFire + (baseDamageFire * (xp*0.2f)) + damageSupp;
    }


}
