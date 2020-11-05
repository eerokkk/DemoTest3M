using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;

public class Logic : MonoBehaviour
{
    private static BoardFill _boardFill;
    private static Config _config;
    private bool matchFound = false;
    private bool isChecking;

    private void Start()
    {
        _config = FindObjectOfType<Config>();
        _boardFill = FindObjectOfType<BoardFill>();
        _boardFill.OnDestroyedTile += BoardOnDestroyedTile;
    }

    private void BoardOnDestroyedTile(int x, int y)
    {
        SettleBlockNew(x, y).OnComplete(CheckComboRay);
        //CheckComboRay();
    }

    private void CheckComboRay()
    {
        List<Tuple<int, int>> tuples = new List<Tuple<int, int>>();
        for (int x = 0; x < _config.boardSize.x; x++)
        {
            for (int y = 0; y < _config.boardSize.y; y++)
            {
                var findMatch = _boardFill.GameTiles[x, y].FindMatch();
                if (findMatch.Count >= 3)
                {
                    Debug.Log(findMatch.Count);
                    _boardFill.OnDestroyedTile -= BoardOnDestroyedTile;
                    foreach (var gameTile in findMatch)
                    {
                        var tuple = _boardFill.GameTiles.CoordinatesOf(gameTile);
                        tuples.Add(tuple);
                        _boardFill.DestroyTile(tuple.Item1, tuple.Item2);
                    }

                    var disTuples = tuples.Where(tile => tile.Item2 != y).ToList();
                    //disTuples.ForEach(tuple => SettleBlockNew(tuple.Item1, tuple.Item2));
                    _boardFill.OnDestroyedTile += BoardOnDestroyedTile;
                    return;
                }
            }
        }
    }

    private void CheckCombo(int x, int y)
    {
        bool endCombo = false;
        List<Vector2Int> comboList = new List<Vector2Int>();
        comboList.Add(new Vector2Int(x,y));
        var mark = _boardFill.GameTiles[x,y].Mark;
        for (; y <= _config.boardSize.y - 1; y++)
        {
            for (int left = x - 1; left >= 0; left--)
            {
                if (_boardFill.GameTiles[left, y].Mark == mark && comboList.Last().x == left+1)
                {
                    comboList.Add(new Vector2Int(left, y));
                    comboList.ForEach(tile => Debug.Log(tile));
                }
                else
                {
                    endCombo = true;
                    break;
                }
            }
            for (int right = x + 1; right < _config.boardSize.x; right++)
            {
                if (_boardFill.GameTiles[right, y].Mark == mark && comboList.First().x == right-1)
                {
                    comboList.Insert(0,new Vector2Int(right, y));
                }
                else
                {
                    endCombo = true;
                    break;
                }
            }
            if (endCombo)
            {
                break;
            }
        }
        
        if (comboList.Count < 3) 
            return;
        //Debug.Log(comboList.Count);
        //comboList.ForEach(tile => Debug.Log($"Гавно {tile.x}, {tile.y})"));
        comboList.ForEach(tile => _boardFill.DestroyTile(tile.x,tile.y));
    }
    
    private TweenerCore<Vector3, Vector3, VectorOptions> SettleBlockNew(int x, int y)
    {
        var tile = _boardFill.GameTiles[x, y];
        tile.DeathAnimation();
        for (; y < _config.boardSize.y - 1; y++)
        {
            _boardFill.GameTiles[x, y] = _boardFill.GameTiles[x, y + 1];
            _boardFill.GameTiles[x, y + 1] = null;
            _boardFill.GameTiles[x, y].Column = y - 4;
            _boardFill.GameTiles[x,y].UpdatePosition();
        }
        tile.BornAnimation();
        _boardFill.GameTiles[x, _config.boardSize.y - 1] = tile;
        _boardFill.GameTiles[x, y].Column = _config.boardSize.y - 1 - 4;
        _boardFill.GameTiles[x, y].ChangeMarkRandom(_boardFill.GameTiles);
        return _boardFill.GameTiles[x, y].UpdatePosition();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var xRound = (int) Math.Round(mouseWorldPos.x) + 3;
            var yRound = (int) Math.Round(mouseWorldPos.y) + 4;
            var tile = _boardFill.GameTiles[xRound, yRound];
            _boardFill.DestroyTile(xRound,yRound);
        }
    }
}
