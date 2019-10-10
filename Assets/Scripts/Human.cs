using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Human : MonoBehaviour
{
    public Image picture;
    internal string itemName;
    private MainCanvas canvas;
    public GameObject currentSymptom;
    public GameObject currentSymptom2;
    private float timeLeft;
    private float timeLeftStart;
    public GameObject startPanel;
    public TextMeshProUGUI humanCount, cured, dead, timer, curedOrDead;
    //private static int cures, deaths, humans;
    public static int cures = GameManager.cures;
    public static int deaths = GameManager.deaths;
    public static int humans = GameManager.humans;
    public Hair hair;
    public List<Sprite> skin;
    private bool onetime;
    public Ending ending;
    public AudioSource deathSound;
    public AudioSource cureSound;



    public Sprite sprite
    {
        set
        {
            if (itemName != "")
                GameManager.instance.RemoveItem(itemName);

            picture.sprite = value;
        }
    }

    private void Awake()
    {
        timeLeft = UnityEngine.Random.Range(260.0f, 270.0f);
        timeLeftStart = timeLeft;
        curedOrDead.text = "";
        canvas = GetComponentInParent<MainCanvas>();
        int randomskin = UnityEngine.Random.Range(0, (skin.Count - 1));

        int randomsymptom = UnityEngine.Random.Range(0, 4);
        int randomsymptom2 = UnityEngine.Random.Range(0, 4);
        gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).GetChild(3).gameObject.SetActive(false);

        currentSymptom = gameObject.transform.GetChild(1).GetChild(randomsymptom).gameObject;
        currentSymptom2 = gameObject.transform.GetChild(1).GetChild(randomsymptom2).gameObject;
        currentSymptom.SetActive(true);
        currentSymptom2.SetActive(true);



        if (currentSymptom.name == "Nausea" || currentSymptom2.name == "Nausea")
        {
            gameObject.GetComponent<Image>().sprite = skin[3];
        }
        else
        {
            for (int i = 0; i < (skin.Count - 1); i++)
            {
                gameObject.GetComponent<Image>().sprite = skin[randomskin];
            }
        }

        hair.GetComponent<Hair>().HairChange();
    }
    public void Respawn()
    {
        if (humans > 2 && gameObject.transform.parent.childCount == 3)
        {

            gameObject.SetActive(true);
            Awake();
            timeLeft = timeLeftStart;

        }


    }

    public void OnDrop(BaseEventData bases)
    {
        if (timer.enabled == true)
        {
            canvas.OnDrop(this);
            if ((currentSymptom.name == currentSymptom2.name && itemName == currentSymptom.name + " Cure") || itemName == currentSymptom.name + " " + currentSymptom2.name + " Cure" || itemName == currentSymptom2.name + " " + currentSymptom.name + " Cure")
            {
                StartCoroutine(Cured());
            }
            else
            {
                StartCoroutine(Dead());
            }
        }

    }


    IEnumerator Dead()
    {
        timer.enabled = false;
        curedOrDead.text = "Dead";
        curedOrDead.color = Color.red;
        deaths++;
        Ending.dead++;
        deathSound.Play();
        yield return new WaitForSeconds(1);
        AfterCuredOrDead();

    }

    IEnumerator Cured()
    {
        timer.enabled = false;
        curedOrDead.text = "Cured";
        curedOrDead.color = Color.green;
        cures++;
        Ending.cured++;
        cureSound.Play();
        yield return new WaitForSeconds(1);
        AfterCuredOrDead();
    }
    private void AfterCuredOrDead()
    {
        gameObject.SetActive(false);
        humans--;
        Respawn();
        timer.enabled = true;

    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            onetime = true;

        }
        if (startPanel.activeSelf == false)
        {
            if (timer.enabled == true)
            {
                timeLeft -= Time.deltaTime;
                timeLeft = (int)(timeLeft * 10.0f) / 10.0f; ;
                timer.text = timeLeft.ToString();
            }
        }
        humanCount.text = (humans.ToString() + " sick people left.");
        cured.text = (cures.ToString() + " people cured.");
        dead.text = (deaths.ToString() + " people dead.");
        if (timeLeft == 0)
            if (onetime == true)
            {
                {
                    StartCoroutine(Dead());
                    onetime = false;
                }
            }

    }
}
