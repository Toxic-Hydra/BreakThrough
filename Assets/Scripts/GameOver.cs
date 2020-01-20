using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    private CharacterProperties PlayerProp1;
    private CharacterProperties PlayerProp2;
    public GameObject p1menu;
    public GameObject p2menu;
    public bool allowRounds;
    private GameObject child1;
    private GameObject child2;
    public TextMeshProUGUI p1WinCount;
    public TextMeshProUGUI p2WinCount;
    public TextMeshProUGUI roundTimerText;
    static public int p1Win;
    static public int p2Win;
    float endTimer;
    float replayTimer;
    float roundTimer;
    bool replaying;
    bool timerStart;

	void Start()
	{
		PlayerProp1 = GameObject.Find("Player1").transform.GetComponentInChildren<CharacterProperties>();
		PlayerProp2 = GameObject.Find("Player2").transform.GetComponentInChildren<CharacterProperties>();
		child1 = p1menu.transform.GetChild(0).gameObject;
		child2 = p2menu.transform.GetChild(0).gameObject;
		endTimer = -2;
		replayTimer = -2;
		roundTimer = 99;
		roundTimerText.text = roundTimer.ToString("F0");
		replaying = false;
		if (p1Win == 2 || p2Win == 2)
		{
    		p1Win = 0;
    		p2Win = 0;
    		PlayerProp1.armor = 4;
    		PlayerProp2.armor = 4;
    		PlayerProp1.durability = 100;
    		PlayerProp2.durability = 10;
    	}
    	p1WinCount.text = p1Win.ToString();
    	p2WinCount.text = p2Win.ToString();
    	timerStart = false;
    	child1.SetActive(false);
    	child2.SetActive(false);
	}

	void Update()
	{
		if (endTimer > 0)
		{
			timerStart = false;
			endTimer -= Time.deltaTime;
		}
		if (replayTimer > 0)
		{
			replayTimer -= Time.deltaTime;
		}
		if (StartText.startReady == true)
		{
			timerStart = true;
		}
		if (roundTimer > 0 && timerStart)
		{
			roundTimer -= Time.deltaTime;
		}
		roundTimerText.text = roundTimer.ToString("F0");
		if (PlayerProp1.currentHealth <= 0 && p2Win == 2)
		{
			if (endTimer == -2)
			{
				endTimer = 3;
			}
			if (endTimer <=  0 && endTimer > -2)
			{
			child2.SetActive(true);
			}
        }
        else if (PlayerProp2.currentHealth <= 0 && p1Win == 2)
        {
        	if (endTimer == -2)
        	{
				endTimer = 3;
			}
            if (endTimer <=  0 && endTimer > -2)
            {
			child1.SetActive(true);
			}
        }
        if (roundTimer <= 0)
        {
        	roundTimer = 0;
			timerStart = false;
			if (PlayerProp1.currentHealth > PlayerProp2.currentHealth)
			{
	        	PlayerProp2.currentHealth = 0;
	        }
	        if (PlayerProp2.currentHealth > PlayerProp1.currentHealth)
	        {
	        	PlayerProp1.currentHealth = 0;
	        }
		}
        
        if (allowRounds)
        {
	        if (PlayerProp1.currentHealth <= 0 && replaying == false && p2Win != 2)
	        {
				++p2Win;
				replayTimer = 5;
				replaying = true;
				p2WinCount.text = p2Win.ToString();
			}
			else if (PlayerProp2.currentHealth <= 0 && replaying == false && p1Win != 2)
			{
				++p1Win;
				replayTimer = 5;
				replaying = true;
				p1WinCount.text = p1Win.ToString();
			}
			if (replayTimer <= 0 && replayTimer > -2 && p1Win != 2 && p2Win != 2)
			{
				ReplayGame();
			}
		}
	}

    public void ReplayGame()
    {
    	PlayerProp1.currentHealth = PlayerProp1.maxHealth;
    	PlayerProp2.currentHealth = PlayerProp2.maxHealth;
    	Vector3 p1Start = new Vector3(-1.3f, 1.15f, -3);
    	Vector3 p2Start = new Vector3(1.3f, 1.15f, -3);
    	GameObject.Find("Player1").transform.GetChild(0).transform.position = p1Start;
    	GameObject.Find("Player2").transform.GetChild(0).transform.position = p2Start;
    	StartText.startReady = false;
    	replaying = false;
    	Start();
    }

    public void QuitToMenu()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
