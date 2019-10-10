using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ending : MonoBehaviour
{
    public TMPro.TextMeshProUGUI humanCount, curedCount, deadCount, rating, ratingDesc;
    private int humans = GameManager.startHumans;
    public static int cured = 0;
    public static int dead = 0;
    public AudioSource endingTheme;
    public void GameEnding()
    {
        endingTheme.Play();
        gameObject.SetActive(true);
        humanCount.text = "Sick people: " + humans;
        curedCount.text = "Cured people: " + cured + " out of " + humans;
        deadCount.text = "Dead people: " + dead + " out of " + humans;
        if (dead==0)
        {
            rating.text = "Rating : Perfect";
            rating.color = Color.cyan;
            ratingDesc.text = "Your medical skills have saved every single villager, making you a well known hero among locals. Good job!";
        }
        else if (dead<=4)
        {
            rating.text = "Rating : Good";
            rating.color = Color.green;
            ratingDesc.text = "Even though you couldn't save everyone, you still saved many lives, which made you highly respected by the locals. Well done!";
        }
        else if (dead > 4  && dead <=10)
        {
            rating.text = "Rating : Average";
            rating.color = Color.yellow;
            ratingDesc.text = "You tried your best, but you let some people die. Of course, the ones you cured are thankful, but the other locals are questioning your medical skills.";
        }
        else if (dead > 10)
        {
            rating.text = "Rating : Bad";
            rating.color = Color.red;
            ratingDesc.text = "You clearly aren't the best choice for this job. You have been exiled from the village for your mistakes that cost many lives.";
        }
        else if (dead == humans)
        {
            rating.text = "Rating : Horrible";
            rating.color = Color.grey;
            ratingDesc.text = "Every one of your patients died. You have absolutely no medical skills. An angry mob of villagers is chasing you right now. Did you even try?";
        }
    }

    void Update()
    {

    }

}
