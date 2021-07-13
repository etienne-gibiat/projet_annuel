using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    private vThirdPersonInput Player;
    [Header("Dégats élémentaires")]
    public float FireDamage; // Dégats de feu
    public float WaterDamage; // Dégats de glace
    public float EarthDamage; // Dégats de terre
    public float TornadoDamage; // Dégats de tornade
    [Header("Sorts")]
    public GameObject spellFeu;
    public GameObject spellEau;
    public GameObject spellTerre;
    public GameObject spellAir;
    private float baseDamageFire;
    private float baseDamageWater;
    private float baseDamageEarth;
    private float baseDamageTornado;
    public float damageSupp;
    private void Start()
    {
        baseDamageFire = FireDamage;
        baseDamageWater = WaterDamage;
        baseDamageEarth = EarthDamage;
        baseDamageTornado = TornadoDamage;
        Player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<vThirdPersonInput>();
    }
    public void UpdateXp(int xp)
    {
        WaterDamage = baseDamageWater + (baseDamageWater * (xp * 0.3f)) + damageSupp;
        FireDamage = baseDamageFire + (baseDamageFire * (xp*0.2f)) + damageSupp;
        EarthDamage = baseDamageEarth + (baseDamageEarth * (xp * 0.2f)) + damageSupp;
        TornadoDamage = baseDamageTornado + (baseDamageTornado * (xp * 0.2f)) + damageSupp;
    }


}
