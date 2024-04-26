using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[AddComponentMenu("Actions/Game Over")]
public class ActionGameOver : Action
{
    [HorizontalLine(color: EColor.Green)]
    [SerializeField, TextArea]
    private string reason;

    protected override bool OnRun()
    {
        GameOverUI gameOver = FindObjectOfType<GameOverUI>(true);
        if (gameOver == null) return false;

        gameOver.EnableGameOver(reason);

        return true;
    }
}
