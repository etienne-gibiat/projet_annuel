using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectInventaire : MonoBehaviour
{
    Inventaire inventaire_script;

    public GameObject violetteScoreText;
    public GameObject coquelicotscoreText;
    public GameObject orchideecoreText;

    public GameObject manaPotionScoreText;
    public GameObject healthPotionScoreText;
    public GameObject xpPotionScoreText;
    public GameObject player;
    public vThirdPersonInput scriptPlayer;

    private void Start()
    {
        inventaire_script = GameObject.Find("Inventaire").GetComponent<Inventaire>();
        scriptPlayer = player.GetComponent<vThirdPersonInput>();
    }

    public void Selection()
    {
        //Nr slot
        int nr_slot = transform.parent.GetSiblingIndex();
        if (nr_slot == 3)
            nr_slot = 5;
        else if (nr_slot == 5)
            nr_slot = 3;

        int nb_slot_count = inventaire_script.slot[nr_slot];
        //Decremente 1 création potion
        if (nr_slot == 5)  // Coquelicot
        {
            if (nb_slot_count >= 5)
            {
                CollectThings.coquelicotCount -= 5;
                inventaire_script.slot[0] += 1;
                healthPotionScoreText.GetComponent<Text>().text = "" + inventaire_script.slot[0];
                CollectThings.idCollected = 4;
            }
        }
        else if (nr_slot == 4) // Violette 
        {
            if (nb_slot_count >= 5)
            {
                CollectThings.violetteCount -= 5;
                inventaire_script.slot[1] += 1;
                manaPotionScoreText.GetComponent<Text>().text = "" + inventaire_script.slot[1];
                CollectThings.idCollected = 5;
            }
        }
        else if (nr_slot == 3)  // Orchidee
        {
            if (nb_slot_count >= 5)
            {
                CollectThings.orchideeCount -= 5;
                inventaire_script.slot[2] += 1;
                xpPotionScoreText.GetComponent<Text>().text = "" + inventaire_script.slot[2];
                CollectThings.idCollected = 6;
            }
        }
        else if (nr_slot == 0) // Potion vie
        {
            
            float AddHp = scriptPlayer.MaxHealth * 33 / 100;
            float diffHealth = scriptPlayer.MaxHealth - scriptPlayer.Health;
            if (nb_slot_count >= 1 && diffHealth != 0)
            {
                nb_slot_count -= 1;
                inventaire_script.slot[0] -= 1;
                healthPotionScoreText.GetComponent<Text>().text = "" + nb_slot_count;
                scriptPlayer.Health += AddHp;
                if (diffHealth >= AddHp)
                    scriptPlayer.Health += AddHp;
                else
                    scriptPlayer.Health += diffHealth;
            }
        }
        else if (nr_slot == 1) // Potion mana
        {
            float AddMana = scriptPlayer.MaxMana * 50 / 100;
            float diffMana = scriptPlayer.MaxMana - scriptPlayer.Mana;
            if (nb_slot_count >= 1 && diffMana != 0)
            {
                nb_slot_count -= 1;
                inventaire_script.slot[1] -= 1;
                manaPotionScoreText.GetComponent<Text>().text = "" + nb_slot_count;
                if (diffMana >= AddMana)
                    scriptPlayer.Mana += AddMana;
                else
                    scriptPlayer.Mana += diffMana;
            }
        }
        else if (nr_slot == 2) // Potion xp
        {
            float AddXp = scriptPlayer.MaxXpBeforeLevelUp * 33 / 100;
            if (nb_slot_count >= 1)
            {
                nb_slot_count -= 1;
                inventaire_script.slot[2] -= 1;
                xpPotionScoreText.GetComponent<Text>().text = "" + nb_slot_count;
                scriptPlayer.getXp(AddXp);
            }
        }
        else
        {
            if(nb_slot_count > 0)
                nb_slot_count -= 1;
        }
    }

    void Update()
    {
        inventaire_script.slot[inventaire_script.slot.Length - 2] = CollectThings.violetteCount;
        inventaire_script.slot[inventaire_script.slot.Length - 1] = CollectThings.coquelicotCount;
        inventaire_script.slot[inventaire_script.slot.Length - 3] = CollectThings.orchideeCount;

        violetteScoreText.GetComponent<Text>().text = "" + CollectThings.violetteCount;
        coquelicotscoreText.GetComponent<Text>().text = "" + CollectThings.coquelicotCount;
        orchideecoreText.GetComponent<Text>().text = "" + CollectThings.orchideeCount;
    }

}
