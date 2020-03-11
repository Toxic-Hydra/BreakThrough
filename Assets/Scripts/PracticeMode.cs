﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeMode : MonoBehaviour
{
    GameObject Player1;
    GameObject Player2;

    CharacterProperties P1Prop;
    CharacterProperties P2Prop;
    HitDetector P1hit;
    HitDetector P2hit;
    AcceptInputs P1Input;
    AcceptInputs P2Input;
    HUD HUD;
    public MaxInput MaxInput;
    public GameObject MaxInputObject;
    public GameObject GameOverManager;
    public GameObject PracticeModeSettings;

    private bool P1inCombo;
    private bool P2inCombo;
    private bool P2inTrueCombo;
    private bool fixAnimBug;
    private float InputTimer;
    double p1x;
    double p2x;

    public bool enableArmorRefill = true;
    public bool enableCPUAirTech;
    public string dummyState = "Stand";

    public int P1ValorSetting = 100;
    public int P2ValorSetting = 100;

    private float P1PrevHealth;
    private float P2PrevHealth;
    private float P1CurrentHitDamage;
    private float P2CurrentHitDamage;
    private float P1CurrentComboTotalDamage;
    private float P2CurrentComboTotalDamage;
    private float P1HighestComboDamage;
    private float P2HighestComboDamage;

    public Text P1HitDamage;
    public Text P2HitDamage;
    public Text P1ComboDamage;
    public Text P2ComboDamage;
    public Text P1HighComboDamage;
    public Text P2HighComboDamage;

    public GameObject DamageDisplays;
    public GameObject P1Displays;
    public GameObject P2Displays;

    // Start is called before the first frame update
    void Start()
    {       
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        P1Prop = GameObject.Find("Player1").transform.GetComponentInChildren<CharacterProperties>();
        P2Prop = GameObject.Find("Player2").transform.GetComponentInChildren<CharacterProperties>();
        P1hit = GameObject.Find("Player1").transform.GetComponentInChildren<HitDetector>();
        P2hit = GameObject.Find("Player2").transform.GetComponentInChildren<HitDetector>();
        P1Input = GameObject.Find("Player1").transform.GetChild(0).transform.GetComponentInChildren<AcceptInputs>();
        P2Input = GameObject.Find("Player2").transform.GetChild(0).transform.GetComponentInChildren<AcceptInputs>();
        HUD = GameObject.Find("HUD").GetComponent<HUD>();
        P1PrevHealth = P1Prop.maxHealth;
        P2PrevHealth = P2Prop.maxHealth;
        P1HitDamage.text = "";
        P2HitDamage.text = "";
        P1ComboDamage.text = "Total Damage: ";
        P2ComboDamage.text = "Total Damage: ";
        P1HighComboDamage.text = "Highest Combo Damage: 0";
        P2HighComboDamage.text = "Highest Combo Damage: 0";

        if (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P1Side == "Left")
        {
            P2Displays.SetActive(false);
        }
        else
        {
            P1Displays.SetActive(false);
        }

        if (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().gameMode == "Practice")
        {
            GameOverManager = GameObject.Find("GameOverManager");
            GameOverManager.SetActive(false);
            DamageDisplays.SetActive(true);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Player1.GetComponent<MovementHandler>().Actions.superFlash);
        //Practice Mode Handler
        if (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().gameMode == "Practice")
        {
            //Check Settings from Practice Pause Menu
            //CPUState Check
            switch (PracticeModeSettings.GetComponent<PauseMenu>().CPUState)
            {
                case 0:
                    dummyState = "Stand";
                    break;
                case 1:
                    dummyState = "Crouch";
                    break;
                case 2:
                    dummyState = "Jump";
                    break;
                case 3:
                    dummyState = "Guard";
                    break;
                case 4:
                    dummyState = "LowGuard";
                    break;
                case 5:
                    dummyState = "CPU";
                    break;
                case 6:
                    dummyState = "Player";
                    break;
            }
            
            switch(PracticeModeSettings.GetComponent<PauseMenu>().ArmorRefill)
            {
                case 0:
                    enableArmorRefill = true;
                    break;
                case 1:
                    enableArmorRefill = false;
                    break;
            }

            switch (PracticeModeSettings.GetComponent<PauseMenu>().CPUAirRecover)
            {
                case 0:
                    enableCPUAirTech = true;
                    break;
                case 1:
                    enableCPUAirTech = false;
                    break;
            }

            if (!PracticeModeSettings.GetComponent<PauseMenu>().isPaused)
            {
                //Refill Armor Meters Option
                if (enableArmorRefill)
                {
                    //Refill P1 Armor when P1 combo finishes
                    if (HUD.combogauge1.enabled == false && P2inCombo && P1Prop.HitDetect.comboCount == 0)
                    {
                        P1Prop.armor = 4;
                        P1Prop.durability = 100;
                    }
                    //Refill P2 Armor when P2 combo finishes
                    if (HUD.combogauge1.enabled == false && P2inCombo && P2Prop.HitDetect.comboCount == 0)
                    {
                        P2Prop.armor = 4;
                        P2Prop.durability = 100;
                    }
                    //Refill P1 armor after move whiffed
                    if (P1Prop.HitDetect.Actions.acceptSuper && !P2inCombo)
                    {
                        P1Prop.armor = 4;
                        P1Prop.durability = 100;
                    }
                    //Refill P2 armor after move whiffed
                    if (P2Prop.HitDetect.Actions.acceptSuper && !P1inCombo)
                    {
                        P2Prop.armor = 4;
                        P2Prop.durability = 100;
                    }
                }

                //Update Valor settings from menu
                P1ValorSetting = PracticeModeSettings.GetComponent<PauseMenu>().P1Valor;
                P2ValorSetting = PracticeModeSettings.GetComponent<PauseMenu>().P2Valor;

                //Refill Health Meters/Manage whiff detection for Armor refill            
                //Refill P1 HP after P2 combo finishes
                if (P1Prop.HitDetect.hitStun > 0)
                {
                    //P1inCombo = true;
                }
                if (P2Prop.HitDetect.comboCount == 0)
                {
                    P1Prop.currentHealth = P1Prop.maxHealth * (P1ValorSetting/100f);
                    //P1inCombo = false;
                    P2CurrentHitDamage = 0;
                    P1PrevHealth = P1Prop.currentHealth;
                    P2CurrentComboTotalDamage = 0;
                }
                //Refill P2 HP after P1 combo finishes  
                if (P2Prop.HitDetect.hitStun > 0)
                {
                    P2inCombo = true;
                    InputTimer = 0.0f;
                }
                if (P1Prop.HitDetect.comboCount == 0)
                {
                    P2Prop.currentHealth = P2Prop.maxHealth * (P2ValorSetting/ 100f);
                    P2inCombo = false;
                    P1CurrentHitDamage = 0;
                    P2PrevHealth = P2Prop.currentHealth;
                    P1CurrentComboTotalDamage = 0;
                }

                //Manage Hit/Combo Damage Display
                //Display Current hit damage
                if (P2Prop.currentHealth < P2PrevHealth)
                {
                    P1CurrentHitDamage = P2PrevHealth - P2Prop.currentHealth;
                    P1CurrentComboTotalDamage += P1CurrentHitDamage;
                    P1HitDamage.text = "";
                    P1HitDamage.text = "Damage: ";
                    P1HitDamage.text += P1CurrentHitDamage;
                    P1ComboDamage.text = "";
                    P1ComboDamage.text = "Total Damage : ";
                    P1ComboDamage.text += P1CurrentComboTotalDamage;
                    P2PrevHealth = P2Prop.currentHealth;

                    P2CurrentHitDamage = P2PrevHealth - P2Prop.currentHealth;
                    P2CurrentComboTotalDamage += P1CurrentHitDamage;
                    P2HitDamage.text = "";
                    P2HitDamage.text = "Damage: ";
                    P2HitDamage.text += P1CurrentHitDamage;
                    P2ComboDamage.text = "";
                    P2ComboDamage.text = "Total Damage: ";
                    P2ComboDamage.text += P1CurrentComboTotalDamage;
                }
                if (HUD.Player1Combo.text == "" && P1Prop.HitDetect.comboCount != 1)
                {
                    P1HitDamage.text = "";
                }
                if (HUD.Player2Combo.text == "" && P1Prop.HitDetect.comboCount != 1)
                {
                    P2HitDamage.text = "";
                }
                //Update Highest Combo Damage
                if (P1CurrentComboTotalDamage > P1HighestComboDamage)
                {
                    P1HighestComboDamage = P1CurrentComboTotalDamage;
                    P1HighComboDamage.text = "";
                    P1HighComboDamage.text = "Highest Combo Damage: ";
                    P1HighComboDamage.text += P1HighestComboDamage;

                    P2HighestComboDamage = P1CurrentComboTotalDamage;
                    P2HighComboDamage.text = "";
                    P2HighComboDamage.text = "Highest Combo Damage: ";
                    P2HighComboDamage.text += P1HighestComboDamage;
                }

                //Handle Dummy State
                p1x = GameObject.Find("Player1").transform.GetChild(0).transform.position.x;
                p2x = GameObject.Find("Player2").transform.GetChild(0).transform.position.x;
                if (InputTimer > 0)
                {
                    InputTimer -= Time.deltaTime;
                }
                else
                {
                    InputTimer = 0;
                }
                switch (dummyState)
                {
                    case "CPU":
                        MaxInput.enableAI();
                        MaxInputObject.GetComponent<AI>().enabled = true;
                        break;
                    case "Stand":
                        MaxInput.ClearInput("Player2");
                        MaxInput.enableAI();
                        MaxInputObject.GetComponent<AI>().enabled = false;
                        if (enableCPUAirTech)
                        {
                            if (P2Prop.HitDetect.hitStun > 0 && Player2.transform.GetComponentInChildren<AcceptInputs>().airborne)
                            {
                                P2inTrueCombo = true;
                            }
                            else if (P2Prop.HitDetect.hitStun == 0 && Player2.GetComponentInChildren<AcceptInputs>().airborne && P2inTrueCombo)
                            {
                                MaxInput.Cross("Player2");
                                P2inTrueCombo = false;
                            }
                        }
                        break;
                    case "Crouch":
                        MaxInput.ClearInput("Player2");
                        MaxInput.enableAI();
                        MaxInputObject.GetComponent<AI>().enabled = false;
                        MaxInput.Crouch("Player2");
                        if (enableCPUAirTech)
                        {
                            if (P2Prop.HitDetect.hitStun > 0 && Player2.transform.GetComponentInChildren<AcceptInputs>().airborne)
                            {
                                P2inTrueCombo = true;
                            }
                            else if (P2Prop.HitDetect.hitStun == 0 && Player2.GetComponentInChildren<AcceptInputs>().airborne && P2inTrueCombo)
                            {
                                MaxInput.Cross("Player2");
                                P2inTrueCombo = false;
                            }
                        }
                        break;
                    case "Jump":
                        MaxInput.ClearInput("Player2");
                        MaxInput.enableAI();
                        MaxInputObject.GetComponent<AI>().enabled = false;

                        if (InputTimer == 0 && !P2inCombo)
                        {
                            MaxInput.Jump("Player2");
                            InputTimer = 1.0f;
                        }
                        if (enableCPUAirTech)
                        {
                            if (P2Prop.HitDetect.hitStun > 0 && Player2.transform.GetComponentInChildren<AcceptInputs>().airborne)
                            {
                                P2inTrueCombo = true;
                            }
                            else if (P2Prop.HitDetect.hitStun == 0 && Player2.GetComponentInChildren<AcceptInputs>().airborne && P2inTrueCombo)
                            {
                                MaxInput.Cross("Player2");
                                P2inTrueCombo = false;
                            }
                        }
                        break;
                    case "Guard":
                        MaxInput.ClearInput("Player2");
                        MaxInput.enableAI();
                        MaxInputObject.GetComponent<AI>().enabled = false;
                        if (p1x - p2x < 0)
                        {
                            MaxInput.MoveRight("Player2");
                        }
                        else
                        {
                            MaxInput.MoveLeft("Player2");
                        }
                        if (enableCPUAirTech)
                        {
                            if (P2Prop.HitDetect.hitStun > 0 && Player2.transform.GetComponentInChildren<AcceptInputs>().airborne)
                            {
                                P2inTrueCombo = true;
                            }
                            else if (P2Prop.HitDetect.hitStun == 0 && Player2.GetComponentInChildren<AcceptInputs>().airborne && P2inTrueCombo)
                            {
                                MaxInput.Cross("Player2");
                                P2inTrueCombo = false;
                            }
                        }
                        break;
                    case "LowGuard":
                        MaxInput.ClearInput("Player2");
                        MaxInput.enableAI();
                        MaxInputObject.GetComponent<AI>().enabled = false;
                        if (p1x - p2x < 0)
                        {
                            MaxInput.DownRight("Player2");
                        }
                        else
                        {
                            MaxInput.DownLeft("Player2");
                        }
                        if (enableCPUAirTech)
                        {
                            if (P2Prop.HitDetect.hitStun > 0 && Player2.transform.GetComponentInChildren<AcceptInputs>().airborne)
                            {
                                P2inTrueCombo = true;
                            }
                            else if (P2Prop.HitDetect.hitStun == 0 && Player2.GetComponentInChildren<AcceptInputs>().airborne && P2inTrueCombo)
                            {
                                MaxInput.Cross("Player2");
                                P2inTrueCombo = false;
                            }
                        }
                        break;
                    case "Player":
                        MaxInput.ClearInput("Player2");
                        MaxInput.disableAI();
                        break;
                }

                if (fixAnimBug)
                {
                    switch (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P1Character)
                    {
                        case "Dhalia":
                            resetDhalia(Player1);
                            break;
                    }

                    switch (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P2Character)
                    {
                        case "Dhalia":
                            resetDhalia(Player2);
                            break;
                    }
                    fixAnimBug = false;
                    InputTimer = 0.0f;
                }

                //Reset Positions back to start
                if (Input.GetButtonDown("Select_P1"))
                {
                    resetPositions();
                    //Reset Character Specific things
                    switch (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P1Character)
                    {
                        case "Dhalia":
                            resetDhalia(Player1);
                            break;
                    }

                    switch (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P2Character)
                    {
                        case "Dhalia":
                            resetDhalia(Player2);
                            break;
                    }
                    fixAnimBug = true;
                }
            }
            
        }
    }

    void resetPositions()
    {
        //Reset Player Knockback
        Player1.transform.GetChild(0).GetComponent<MovementHandler>().HitDetect.KnockBack = new Vector2(0, 0);
        Player1.transform.GetChild(0).GetComponent<MovementHandler>().HitDetect.ProjectileKnockBack = new Vector2(0, 0);
        Player1.transform.GetChild(0).GetComponent<MovementHandler>().rb.velocity = Vector2.zero;
        Player2.transform.GetChild(0).GetComponent<MovementHandler>().HitDetect.KnockBack = new Vector2(0, 0);
        Player2.transform.GetChild(0).GetComponent<MovementHandler>().HitDetect.ProjectileKnockBack = new Vector2(0, 0);
        Player2.transform.GetChild(0).GetComponent<MovementHandler>().rb.velocity = Vector2.zero;

        //Reset Camera
        Player1.transform.GetChild(0).GetComponent<MovementHandler>().Actions.superFlash = 0;
        Player2.transform.GetChild(0).GetComponent<MovementHandler>().Actions.superFlash = 0;
        GameObject.Find("CameraPos").transform.GetChild(1).transform.position = GameObject.Find("CameraPos").transform.position;

        //Refill Armor
        P1Prop.armor = 4;
        P1Prop.durability = 100;
        P2Prop.armor = 4;
        P2Prop.durability = 100;

        //Setting players to starting location vectors
        if (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P1Side == "Left")
        {
            GameObject.Find("Player1").transform.GetChild(0).transform.position = GameObject.Find("Player1").transform.position;
        }
        else
        {
            GameObject.Find("Player1").transform.GetChild(0).transform.position = GameObject.Find("Player1").transform.position;
        }
        if (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P2Side == "Right")
        {
            GameObject.Find("Player2").transform.GetChild(0).transform.position = GameObject.Find("Player2").transform.position;
        }
        else
        {
            GameObject.Find("Player2").transform.GetChild(0).transform.position = GameObject.Find("Player2").transform.position;
        }

        //Reset Character Specific things
        switch (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P1Character)
        {
            case "Dhalia":
                resetDhalia(Player1);
                break;
        }

        switch (GameObject.Find("PlayerData").GetComponent<SelectedCharacterManager>().P2Character)
        {
            case "Dhalia":
                resetDhalia(Player2);
                break;
        }
    }

    //Character Specific Reset Properties
    private void resetDhalia(GameObject player)
    {   
        player.transform.GetChild(0).GetComponentInChildren<AttackHandlerDHA>().anim.SetTrigger(Animator.StringToHash("Blitz"));
        player.transform.GetChild(0).GetComponentInChildren<AttackHandlerDHA>().Hitboxes.BlitzCancel();
        player.transform.GetChild(0).GetComponentInChildren<AttackHandlerDHA>().Actions.landingLag = 0;
        player.transform.GetChild(0).GetComponentInChildren<AttackHandlerDHA>().Move.HitDetect.KnockBack = Vector2.zero;
        player.transform.GetChild(0).GetComponentInChildren<AttackHandlerDHA>().anim.SetBool(Animator.StringToHash("Run"), false);
        player.transform.GetChild(2).gameObject.SetActive(false);
        player.transform.GetChild(3).gameObject.SetActive(false);
    }
}