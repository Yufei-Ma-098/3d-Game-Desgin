using UnityEngine;
using System.Collections;

public class TicTacToe : MonoBehaviour
{
    public Texture2D img;
    public Texture2D img1;
    public Texture2D img2;

    private int result = 0;
    private int turn = 1;
    private int[,] board = new int[3, 3];


    void Reset()
    {
        result = 0;
        turn = 1;
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                board[i, j] = 0;
            }
        }
    }

    void Start()
    {
        Reset();
    }

    void OnGUI()
    {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(255, 255, 255);
        fontStyle.fontSize = 40;

        GUIStyle fontStyle2 = new GUIStyle();
        fontStyle2.normal.background = null;
        fontStyle2.normal.textColor = new Color(178, 34, 34);
        fontStyle2.fontSize = 25;

        GUIStyle fontStyle3 = new GUIStyle();
        fontStyle3.normal.background = null;
        fontStyle3.normal.textColor = new Color(65, 105, 225);
        fontStyle3.fontSize = 20;

        GUIStyle fontStyle4 = new GUIStyle();
        fontStyle4.normal.background = null;
        fontStyle4.normal.textColor = new Color(255, 255, 255);
        fontStyle4.fontSize = 70;

        GUI.Label(new Rect(0, 0, 1024, 900), img);
        GUI.Label(new Rect(225, 20, 100, 100), "Welcome to TicTacToe", fontStyle);
        GUI.Label(new Rect(80, 175, 50, 50), img1);
        GUI.Label(new Rect(150, 200, 100, 50), "Player1", fontStyle3);
        GUI.Label(new Rect(80, 250, 50, 50), img2);
        GUI.Label(new Rect(150, 275, 100, 50), "Player2", fontStyle3);

        GUI.Label(new Rect(80, 125, 100, 50), "Turns:", fontStyle3);
        if (turn == 1)
        {
            GUI.Label(new Rect(50, 170, 200, 200), "·", fontStyle4);
        }
        else
        {
            GUI.Label(new Rect(50, 235, 200, 200), "·", fontStyle4);
        }

        if (GUI.Button(new Rect(400, 100, 70, 50), "RESET"))
            Reset();

        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (board[i, j] == 1)
                    GUI.Button(new Rect(325 + i * 70, 175 + j * 70, 70, 70), img1);
                if (board[i, j] == 2)
                    GUI.Button(new Rect(325 + i * 70, 175 + j * 70, 70, 70), img2);
                if (GUI.Button(new Rect(325 + i * 70, 175 + j * 70, 70, 70), ""))
                {
                    if (result == 0)
                    {
                        if (turn == 1)
                            board[i, j] = 1;
                        else
                            board[i, j] = 2;
                        turn = 1 - turn;
                    }
                }
            }
        }

        GUI.Label(new Rect(50, 335, 100, 50), "Result:", fontStyle2);
        result = check();
        if (result == 1)
        {
            GUI.Label(new Rect(140, 335, 100, 50), "Player1 wins!", fontStyle2);
        }
        else if (result == 2)
        {
            GUI.Label(new Rect(140, 335, 100, 50), "Player2 wins!", fontStyle2);
        }
        else if (result == 3)
        {
            GUI.Label(new Rect(140, 335, 100, 50), "No one wins", fontStyle2);
        }
        else
        {
            GUI.Label(new Rect(140, 335, 100, 50), "Playing...", fontStyle2);
        }

        
    }

    int check()
    {
        int cnt = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] != 0)
                    cnt++;
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }
        }
        for (int j = 0; j < 3; ++j)
        {
            if (board[0, j] != 0 && board[0, j] == board[1, j] && board[1, j] == board[2, j])
            {
                return board[0, j];
            }
        }
        if (board[1, 1] != 0 &&
            board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] ||
            board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[1, 1];
        }
        if (cnt == 9)
            return 3;
        return 0;
    }
}