using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Text good;
    public Text bad;

    public AICharacter[] goodBoyz;
    public AICharacter[] badBoyz;

    private void Start()
    {
        goodBoyz = GameObject.FindObjectsOfType<AICharacter>().Where(boy => boy.name.Contains("Ally")).ToArray();
        badBoyz = GameObject.FindObjectsOfType<AICharacter>().Where(boy => boy.name.Contains("Enemy")).ToArray();
    }

    void Update()
    {

        AICharacter[] goodBoyz2 = goodBoyz.Where(boy => !boy.isDead).ToArray();
        AICharacter[] badBoyz2 = badBoyz.Where(boy => boy.isDead).ToArray();

        good.text = goodBoyz2.Count().ToString() + " / 21";
        bad.text = badBoyz2.Count().ToString() + " / 21";

        if (goodBoyz2.Count() == 21 && badBoyz2.Count() == 21)
            Win();
    }

    void Win()
    {
        Application.Quit();
    }
}
