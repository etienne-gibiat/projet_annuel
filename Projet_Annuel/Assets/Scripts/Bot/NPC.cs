using System.Collections;
using System.Collections.Generic;
using _Elementis.Scripts;
using _Elementis.Scripts.Character_Controller;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource hmmm;
    private Transform player;
    public Transform Exclamation;
    public Transform QuestTxt;
    public TMP_Text text;
    public NpcQuestTextProvider textProvider;
    private int rangeQuest = 6;
    private bool questGiven = false;
    private bool wasInRange;

    private void Start()
    {
        player = FindObjectOfType<ElementisCharacterController>().transform;
    }
    private void Update()
    {
        var inRange = Vector3.Distance(this.transform.position, player.transform.position) < rangeQuest;
        if (inRange)
        {
            if (!questGiven)
            {
                hmmm.Play();
                questGiven = true;
            }

            if (!wasInRange)
            {
                textProvider.PlayerEnteredInRange();
            }

            text.text = GetCurrentText();
            QuestTxt.gameObject.SetActive(true);
            Exclamation.gameObject.SetActive(false);
        }
        else
        {
            QuestTxt.gameObject.SetActive(false);
            Exclamation.gameObject.SetActive(true);
            
        }
        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

         wasInRange = inRange;
    }

    private string GetCurrentText()
    {
        return textProvider.GetCurrentText();
    }
}
