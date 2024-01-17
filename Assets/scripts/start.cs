using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class start : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject mainmenu;
    GameObject bgm;
    public GameObject pause;
    public GameObject gameui;
    public GameObject lose;
    bool game = false;
    GameObject badstuff;
    public static int points = 0;
    Vector2 topLeft, bottomRight;
    GameObject gifts;
    int difficulty = 0;
    public static bool paused = false;
    public static int hp = 5;
    void Start()
    {
        topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        mainmenu = GameObject.Find("main menu");
        badstuff = GameObject.Find("badstuff");
        bgm = GameObject.Find("bgm");
        gifts = GameObject.Find("gifts");
        mainmenu.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            hp = 5;
            mainmenu.SetActive(false);
            gameui.SetActive(true);
            game = true;
        });

        mainmenu.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });

        mainmenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            bgm.GetComponent<AudioSource>().enabled = !bgm.GetComponent<AudioSource>().enabled;
            string newtext = "Music: ";
            if (bgm.GetComponent<AudioSource>().enabled)
            {
                newtext += "True";
            }
            else {
                newtext += "False";
            }

            mainmenu.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = newtext;
        });

        mainmenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => {
            difficulty = difficulty + 1;
            if(difficulty >= 3)
            {
                difficulty = 0;
            }

            if(difficulty == 0)
            {
                mainmenu.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Difficulty: Easy";
            }
            else if(difficulty == 1)
            {
                mainmenu.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Difficulty: Medium";
            }
            else if(difficulty == 2)
            {
                mainmenu.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Difficulty: Hard";
            }
        });

        gameui.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
            paused = true;
            gameui.SetActive(false);
            pause.SetActive(true);
        });

        pause.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            paused = false;
            gameui.SetActive(true);
            pause.SetActive(false);
        });

        pause.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            paused = false;
            game = false;
            mainmenu.SetActive(true);
            pause.SetActive(false);
            foreach(gift gi in GameObject.FindObjectsOfType<gift>())
            {
                if (gi.gameObject.name.Contains("(Clone)"))
                {
                    Destroy(gi.gameObject);
                }
            }
            points = 0;
        });
    }

    float[] ranges = { 2, 1.5f, 1 };

    // Update is called once per frame
    void Update()
    {
        if (game && !paused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("bruh");
                Collider2D[] gifts = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)), ranges[difficulty]);
                foreach(Collider2D gift in gifts)
                {
                    print("delet");
                    gift.gameObject.GetComponent<gift>().click();
                }
            }

            foreach(Transform heart in gameui.transform.GetChild(2))
            {
                if (System.Convert.ToInt32(heart.gameObject.name) > hp) {
                    heart.gameObject.SetActive(false);
                }
                else
                {
                    heart.gameObject.SetActive(true);
                }

                if(hp <= 0)
                {
                    lose.SetActive(true);
                    gameui.SetActive(false);
                    foreach (gift gi in GameObject.FindObjectsOfType<gift>())
                    {
                        if (gi.gameObject.name.Contains("(Clone)"))
                        {
                            Destroy(gi.gameObject);
                        }
                    }
                    game = false;
                    lose.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Points: " + points;
                    lose.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                    {
                        hp = 5;
                        points = 0;
                        game = true;
                        gameui.SetActive(true);
                        lose.SetActive(false);
                    });

                    lose.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
                    {
                        hp = 5;
                        points = 0;
                        game = false;
                        mainmenu.SetActive(true);
                        lose.SetActive(false);
                    });
                }
            }
            gameui.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Points: " + points;
            if (difficulty == 0)
            {
                if (Random.Range(0, (int)(1 / Time.deltaTime)) == 0)
                {
                    if (Random.Range(0, 100) < 95)
                    {
                        Instantiate(gifts.transform.GetChild(Random.Range(0, gifts.transform.childCount)), new Vector2(Random.Range(topLeft.x, bottomRight.x), bottomRight.y + 1), Quaternion.Euler(0f, 0f, Random.Range(0, 360)), null).GetComponent<gift>().speed = Random.Range(1, 6);
                    }
                    else
                    {
                        Instantiate(badstuff.transform.GetChild(Random.Range(0, badstuff.transform.childCount)), new Vector2(Random.Range(topLeft.x, bottomRight.x), bottomRight.y + 1), Quaternion.Euler(0f, 0f, Random.Range(0, 360)), null).GetComponent<bad>().speed = Random.Range(1, 10);
                    }
                }
            }else if (difficulty == 1)
            {
                if (Random.Range(0, (int)((1 / Time.deltaTime) / 2)) == 0)
                {
                    if (Random.Range(0, 100) < 80)
                    {
                        Instantiate(gifts.transform.GetChild(Random.Range(0, gifts.transform.childCount)), new Vector2(Random.Range(topLeft.x, bottomRight.x), bottomRight.y + 1), Quaternion.Euler(0f, 0f, Random.Range(0, 360)), null).GetComponent<gift>().speed = Random.Range(2, 7);
                    }
                    else
                    {
                        Instantiate(badstuff.transform.GetChild(Random.Range(0, badstuff.transform.childCount)), new Vector2(Random.Range(topLeft.x, bottomRight.x), bottomRight.y + 1), Quaternion.Euler(0f, 0f, Random.Range(0, 360)), null).GetComponent<bad>().speed = Random.Range(1, 10);
                    }
                }
            }
            else if(difficulty == 2)
            {
                if (Random.Range(0, (int)((1 / Time.deltaTime) / 2.5)) == 0)
                {
                    if (Random.Range(0, 100) < 70)
                    {
                        Instantiate(gifts.transform.GetChild(Random.Range(0, gifts.transform.childCount)), new Vector2(Random.Range(topLeft.x, bottomRight.x), bottomRight.y + 1), Quaternion.Euler(0f, 0f, Random.Range(0, 360)), null).GetComponent<gift>().speed = Random.Range(3, 8);
                    }
                    else
                    {
                        Instantiate(badstuff.transform.GetChild(Random.Range(0, badstuff.transform.childCount)), new Vector2(Random.Range(topLeft.x, bottomRight.x), bottomRight.y + 1), Quaternion.Euler(0f, 0f, Random.Range(0, 360)), null).GetComponent<bad>().speed = Random.Range(1, 10);
                    }
                }
            }
        }
    }
}
