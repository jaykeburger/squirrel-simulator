using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int piecesOfPaperCollected { get; private set; }
    public int pencilsCollected { get; private set; }

    public void paperCollected()
    {
        piecesOfPaperCollected++;
    }

    public void pencilCollected()
    {
        pencilsCollected++;
    }
}
