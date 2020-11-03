using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class BoardFill : MonoBehaviour
{
    public GameTile[,] GameTiles { get; private set;}
    public float deathDuration;

    private Config _config;
    
    public event Action<int,int> OnDestroyedTile;

    private void Start()
    {
        _config = FindObjectOfType<Config>();
        GameTiles = new GameTile[_config.boardSize.x , _config.boardSize.y];
        GenerateBoard();
    }

    public void DestroyTile(int x, int y)
    {
        if (GameTiles[x,y] == null)
            return;
        GameTiles[x, y].DeathAnimation()
            .OnComplete(() =>
            {
                OnDestroyedTile?.Invoke(x, y);
            });
    }

    private void GenerateBoard()
    {
        int[] spriteRight = new int[_config.boardSize.y];
        int spriteUp = -1;

        for (int x = 0; x < _config.boardSize.x; x++)
        {
            for (int y = 0; y < _config.boardSize.y; y++)
            {
                GameTiles[x, y] = Instantiate(_config.mark, new Vector3Int(x - 3, (y + 1) - 4, 0), quaternion.identity, transform);
                List<int> possibleSprite = new List<int>(); 
                possibleSprite.AddRange(new [] {0,1,2,3,4,5});
                possibleSprite.Remove(spriteRight[y]);
                possibleSprite.Remove(spriteUp);
                int sprite = possibleSprite[Random.Range(0, possibleSprite.Count)];
                spriteRight[y] = sprite;
                spriteUp = sprite;
                GameTiles[x, y].Mark = (Mark)sprite;
                GameTiles[x, y].BornAnimation();
                GameTiles[x, y].Row = x - 3;
                GameTiles[x, y].Column = y - 4;
                GameTiles[x, y].UpdatePosition();
            }
        }
    }
}
