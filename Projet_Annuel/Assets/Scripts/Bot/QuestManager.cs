using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public int camp1 = 0;
    public int camp2 = 0;
    public int camp3 = 0;
    public int boss = 0;

    public GameObject portail;

    private TextMeshProUGUI queteCampText;
    private TextMeshProUGUI queteKemalText;

    private int nb_camp = 21;
    private int nb_camp_total = 21;

    public GameObject victoirePanel;

    private bool victory = false;


    // Start is called before the first frame update
    void Start()
    {
        nb_camp = nb_camp_total;
        queteCampText = this.transform.Find("Canvas/quete1").GetComponent<TextMeshProUGUI>();
        queteKemalText = this.transform.Find("Canvas/quete2").GetComponent<TextMeshProUGUI>();
        portail.SetActive(true);
        victoirePanel.SetActive(false);
        queteCampText.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TextChange());
    }

    public IEnumerator TextChange()
    {
        yield return new WaitForSeconds(1f);
        if (camp1 + camp2 + camp3 > 0)
        {
            queteCampText.text = "Quête 1 : tuer les camps Biancheur (" + nb_camp + "/" + nb_camp_total + ")";
        }
        if (camp1 + camp2 + camp3 == 0)
        {
            queteCampText.color = new Color(0f, 0.7f, 0f);
            queteCampText.text = "Quête 1 : tuer les camps Biancheur";
            portail.SetActive(false);
        }

        if(boss == 0 && !victory)
        {
            queteKemalText.color = new Color(0f, 0.7f, 0f);
            portail.SetActive(true);
            victoirePanel.SetActive(true);
            victory = true;
            Destroy(victoirePanel, 3f);
        }
    }

    public void AttachToMob(string name)
    {
        if (name == "Spawn Biancheur 1")
        {
            camp1++;
        }
        if (name == "Spawn Biancheur 2")
        {
            camp2++;
        }
        if (name == "Spawn Biancheur 3")
        {
            camp3++;
        }
        if (name == "Kemal le Bengali")
        {
            boss++;
        }
    }

    public void DetachToMob(string name)
    {
        if (name == "Spawn Biancheur 1")
        {
            nb_camp--;
            camp1--;
        }
        if (name == "Spawn Biancheur 2")
        {
            nb_camp--;
            camp2--;
        }
        if (name == "Spawn Biancheur 3")
        {
            nb_camp--;
            camp3--;
        }
        if (name == "Kemal le Bengali")
        {
            boss--;
        }
    }
}
