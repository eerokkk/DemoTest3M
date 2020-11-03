using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public Vector2Int boardSize = new Vector2Int(5, 6);
    public GameTile[] GameTiles;
    public GameTile mark;
    public Sprite[] allSprite;
}
