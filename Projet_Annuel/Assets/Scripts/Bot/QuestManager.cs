using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int camp1 = 0;
    public int camp2 = 0;
    public int camp3 = 0;
    public int boss = 0;

    public GameObject portail;

    private Text queteCampText;
    private Text queteKemalText;

    // Start is called before the first frame update
    void Start()
    {
        queteCampText = this.transform.Find("Canvas/quete1").GetComponent<Text>();
        queteKemalText = this.transform.Find("Canvas/quete2").GetComponent<Text>();
        portail.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TextChange());
    }

    public IEnumerator TextChange()
    {
        yield return new WaitForSeconds(1f);
        if (camp1 + camp2 + camp3 == 0)
        {
            queteCampText.color = new Color(0f, 0.7f, 0f);
            portail.SetActive(false);
        }

        if(boss == 0)
        {
            queteKemalText.color = new Color(0f, 0.7f, 0f);
            portail.SetActive(true);
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
            camp1--;
        }
        if (name == "Spawn Biancheur 2")
        {
            camp2--;
        }
        if (name == "Spawn Biancheur 3")
        {
            camp3--;
        }
        if (name == "Kemal le Bengali")
        {
            boss--;
        }
    }
}
