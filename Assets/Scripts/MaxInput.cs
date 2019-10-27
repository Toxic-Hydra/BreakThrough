﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxInput : MonoBehaviour
{
    public bool AI;
    public bool training;

    public float horizontal;
    public float vertical;
    public bool square;
    public bool triangle;
    public bool circle;
    public bool cross;
    public bool rBumper;
    public bool rTrigger;
    public bool lBumper;
    public bool lTrigger;
    public FighterAgent player2;

    public float horizontal1;
    public float vertical1;
    public bool square1;
    public bool triangle1;
    public bool circle1;
    public bool cross1;
    public bool rBumper1;
    public bool rTrigger1;
    public bool lBumper1;
    public bool lTrigger1;
    public FighterAgent player1;

    void Start()
    {
        ClearInput("Player1");
        ClearInput("Player2");
    }

    public float GetAxisRaw(string axis)
    {
        if ((!AI || axis.Contains("P1")) && !training)
        {
            return Input.GetAxisRaw(axis);
        }
        else
        {
            if (axis.Contains("Horizontal_P2"))
            {
                return horizontal;
            }
            else if (axis.Contains("Vertical_P2"))
            {
                return vertical;
            }
            else if (axis.Contains("Horizontal_P1"))
            {
                return horizontal1;
            }
            else if (axis.Contains("Vertical_P1"))
            {
                return vertical1;
            }
            else
            {
                throw new System.Exception("Axis " + axis + " is unknown axis.");
            }
        }
    }

    public float GetAxis(string axis)
    {
        if ((!AI || axis.Contains("P1")) && !training)
        {
            return Input.GetAxis(axis);
        }
        else
        {
            if (axis.Contains("Horizontal_P2"))
            {
                return horizontal;
            }
            else if (axis.Contains("Vertical_P2"))
            {
                return vertical;
            }
            else if (axis.Contains("Horizontal_P1"))
            {
                return horizontal1;
            }
            else if (axis.Contains("Vertical_P1"))
            {
                return vertical1;
            }
            else
            {
                throw new System.Exception("Axis " + axis + " is unknown axis.");
            }
        }
    }

    public bool GetButtonDown(string button)
    {
        if ((!AI || button.Contains("P1")) && !training)
        {
            return Input.GetButtonDown(button);
        }
        else
        {
            switch (button)
            {
                case "Square_P2":
                    return square;
                case "Triangle_P2":
                    return triangle;
                case "Circle_P2":
                    return circle;
                case "Cross_P2":
                    return cross;
                case "R1_P2":
                    return rBumper;
                case "R2_P2":
                    return rTrigger;
                case "L1_P2":
                    return lBumper;
                case "L2_P2":
                    return lTrigger;
                case "Square_P1":
                    return square1;
                case "Triangle_P1":
                    return triangle1;
                case "Circle_P1":
                    return circle1;
                case "Cross_P1":
                    return cross1;
                case "R1_P1":
                    return rBumper1;
                case "R2_P1":
                    return rTrigger1;
                case "L1_P1":
                    return lBumper1;
                case "L2_P1":
                    return lTrigger1;
                case "Start_P2":
                case "Select_P2":
                    return false;
                default:
                    throw new System.Exception("Button " + button + " is an invalid button.");
            }
        }
    }

    public bool GetButton(string button)
    {
        if ((!AI || button.Contains("P1")) && !training)
        {
            return Input.GetButton(button);
        }
        else
        {
            switch (button)
            {
                case "Square_P2":
                    return square;
                case "Triangle_P2":
                    return triangle;
                case "Circle_P2":
                    return circle;
                case "Cross_P2":
                    return cross;
                case "R1_P2":
                    return rBumper;
                case "R2_P2":
                    return rTrigger;
                case "L1_P2":
                    return lBumper;
                case "L2_P2":
                    return lTrigger;
                case "Square_P1":
                    return square1;
                case "Triangle_P1":
                    return triangle1;
                case "Circle_P1":
                    return circle1;
                case "Cross_P1":
                    return cross1;
                case "R1_P1":
                    return rBumper1;
                case "R2_P1":
                    return rTrigger1;
                case "L1_P1":
                    return lBumper1;
                case "L2_P1":
                    return lTrigger1;
                case "Start_P2":
                case "Select_P2":
                    return false;
                default:
                    throw new System.Exception("Button " + button + " is an invalid button.");
            }
        }
    }

    public void Hit(string player)
    {
        if (training || AI)
        {
            if (player == "Player1")
            {
                player1.GotHit();
                player2.HitEnemy();
            }
            else if (player == "Player2")
            {
                player2.GotHit();
                player1.HitEnemy();
            }
        }
    }

    public void ClearInput(string name)
    {
        Debug.Log(name);

        if (name == "Player2")
        {
            horizontal = 0;
            vertical = 0;
            square = false;
            triangle = false;
            circle = false;
            cross = false;
            rBumper = false;
            rTrigger = false;
            lBumper = false;
            lTrigger = false;
        }
        else if (name == "Player1")
        {
            horizontal1 = 0;
            vertical1 = 0;
            square1 = false;
            triangle1 = false;
            circle1 = false;
            cross1 = false;
            rBumper1 = false;
            rTrigger1 = false;
            lBumper1 = false;
            lTrigger1 = false;
        }
}

    public void moveLeft(string name)
    {
        if (name == "Player1")
        {
            horizontal1 = -1;
        }
        else
        {
            horizontal = -1;
        }
    }

    public void moveRight(string name)
    {
        if (name == "Player1")
        {
            horizontal1 = 1;
        }
        else
        {
            horizontal = 1;
        }
    }

    public void Jump(string name)
    {
        if (name == "Player1")
        {
            vertical1 = 1;
        }
        else
        {
            vertical = 1;
        }
    }

    public void Crouch(string name)
    {
        if (name == "Player1")
        {
            vertical1 = -1;
        }
        else
        {
            vertical = -1;
        }
    }

    public void Square(string name)
    {
        if (name == "Player1")
        {
            square1 = true;
        }
        else
        {
            square = true;
        }
    }
    public void Triangle(string name)
    {
        if (name == "Player1")
        {
            triangle1 = true;
        }
        else
        {
            triangle = true;
        }
    }

    public void Circle(string name)
    {
        if (name == "Player1")
        {
            circle1 = true;
        }
        else
        {
            circle = true;
        }
    }

    public void Cross(string name)
    {
        if (name == "Player1")
        {
            cross1 = true;
        }
        else
        {
            cross = true;
        }
    }

    public void RBumper(string name)
    {
        if (name == "Player1")
        {
            rBumper1 = true;
        }
        else
        {
            rBumper = true;
        }
    }

    public void RTrigger(string name)
    {
        if (name == "Player1")
        {
            rTrigger1 = true;
        }
        else
        {
            rTrigger = true;
        }
    }

    public void LBumper(string name)
    {
        if (name == "Player1")
        {
            lBumper1 = true;
        }
        else
        {
            lBumper = true;
        }
    }

    public void LTrigger(string name)
    {
        if (name == "Player1")
        {
            lTrigger1 = true;
        }
        else
        {
            lTrigger = true;
        }
    }

}
