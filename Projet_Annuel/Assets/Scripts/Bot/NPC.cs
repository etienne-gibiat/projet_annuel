using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource hmmm;
    public string[] dialog;
    private Transform player;
    private Animator anim;
    private Transform Exclamation;
    private Transform QuestTxt;
    private Transform QuestManager;
    private int rangeQuest = 4;
    private bool questGiven = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Exclamation = transform.Find("Quest").transform.Find("Exclamation");
        QuestTxt = transform.Find("Quest").transform.Find("QuestText");
        anim = this.GetComponent<Animator>();
        QuestManager = GameObject.Find("HUD").transform.Find("Quest").GetChild(0);
    }
    private void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) < rangeQuest)
        {
            if (!questGiven)
            {
                StartCoroutine(getQuete());
                questGiven = true;
            }
            anim.SetTrigger("PlayerAround");
            QuestTxt.gameObject.SetActive(true);
            Exclamation.gameObject.SetActive(false);
            
        }
        else
        {
            QuestTxt.gameObject.SetActive(false);
            Exclamation.gameObject.SetActive(true);
            hmmm.Play();
        }
        var lookPos = player.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
    }

    /// <summary>
    /// Rend la quête active
    /// </summary>
    /// <returns></returns>
    IEnumerator getQuete()
    {
        yield return new WaitForSeconds(2f);
        QuestManager.gameObject.SetActive(true);
        
    }

}
