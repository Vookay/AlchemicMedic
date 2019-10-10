using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

[System.Serializable]

public class ResourceItem
{
    public string name;
    public Sprite sprite;
}

[System.Serializable]

public class RecipeItem
{
    public List<string> resources;
    public string result;
}

public class GameManager : MonoBehaviour
{
    public static int deaths = 0;
    public static int cures = 0;
    public static int humans = 21;
    public static int startHumans = humans;

    public static GameManager instance;

    public List<ResourceItem> itemList;
    public List<RecipeItem> recipeList;

    public ResultObject resultObject;
    public GameObject buttonOK;
    public List<DropObject> dropList;

    public Ending ending;

    public GameObject startPanel, resumeButton, startButton, endingPanel, howToPlay;
    public AudioSource theme, endingTheme, click, mainMenuTheme;

    private bool endingBool = true;

    internal readonly List<string> currentRecipe = new List<string>();

    private void Awake()
    {
        //GameObject human = GameObject.Find("Human");
       // Human humanScript = human.GetComponent<Human>();
        //humanScript.humans = 21;
        
        instance = this;
        resumeButton.SetActive(false);
        buttonOK.SetActive(false);
        startPanel.SetActive(true);
        endingPanel.SetActive(false);
        theme.Stop();
        endingTheme.Stop();
        mainMenuTheme.Play();
    }

    public void ClearRecipe()
    {
        currentRecipe.Clear();

        foreach (DropObject obj in dropList)
        {
            obj.picture.sprite = null;
        }

        resultObject.picture.sprite = null;
        resultObject.itemName = "";
    }

    public void AddItem(string item)
    {
        currentRecipe.Add(item);
        CheckRecipe();
    }

    public void RemoveItem(string item)
    {
        currentRecipe.Remove(item);
    }

    public void CheckRecipe()
    {
        if (currentRecipe.Count < 2)
          return;

        bool found = true;
        RecipeItem result = null;
        foreach (RecipeItem recipe in recipeList)
        {
            if (recipe.resources.Count != currentRecipe.Count)
                return;

            found = true;
            foreach (string itm in currentRecipe) { 
               if ( recipe.resources.IndexOf(itm) < 0)
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                result = recipe;
                break;
            }
        }

        if (!found)
        {
            ClearRecipe();
            return;
        }


        Debug.Log("Recipe found:" + result.result);

        ResourceItem rItem = itemList.Find((item) => item.name == result.result);
        if (rItem == null)
            return;

        resultObject.itemName = rItem.name;
        resultObject.picture.sprite = rItem.sprite;


        buttonOK.SetActive(true);


    }

    public void OnStartGame()
    {
        ClearRecipe();

        buttonOK.SetActive(false);

        startPanel.SetActive(false);

        theme.Play();

        mainMenuTheme.Stop();
    }
    public void OnExitMenu()
    {
        resumeButton.SetActive(true);

        startPanel.SetActive(true);

        startButton.SetActive(false);

        theme.Stop();

        mainMenuTheme.Play();

    }
    public void OnResumeGame()
    {
        startPanel.SetActive(false);

        theme.Play();

        mainMenuTheme.Stop();
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnHowToPlay()
    {
        howToPlay.SetActive(true);
    }

    public void OnBack()
    {
        howToPlay.SetActive(false);
    }

    public void OnClick()
    {
        click.Play();
    }

    void Update()
    {     
        if (Human.humans==0 && endingBool == true)
        { 
            ending.GameEnding();
            theme.Stop();
            endingBool = false;
        }
    }
}
