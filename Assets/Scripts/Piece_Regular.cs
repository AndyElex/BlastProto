using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Regular : Piece
{
    

    private void Awake()
    {
        switch (colourCode)
        {
            case 0:
                GetComponent<Renderer>().material.color = Color.red;
                break;

            case 1:
                GetComponent<Renderer>().material.color = Color.green;
                break;

            case 2:
                GetComponent<Renderer>().material.color = Color.blue;
                break;

            case 3:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;

            case 4:
                GetComponent<Renderer>().material.color = Color.magenta;
                break;

            case 5:
                GetComponent<Renderer>().material.color = Color.cyan;
                break;
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.Instance.boardResolving)
            GameManager.Instance.boardManager.ResolveMatch(pieceId);
    }


}
