using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEditor;

public class GameTile : MonoBehaviour
{
    private Mark mark;
    public Mark Mark
    {
        get => mark;
        set
        {
            mark = value;
            _spriteRenderer.sprite = _config.allSprite[(int)value];
        }
    }

    private SpriteRenderer _spriteRenderer;
    private Config _config;
    private BoardFill _board;
    //private List<GameTile> matchingTiles1;

    public int Column { get; set; }
    public int Row { get; set; }

    private void Awake()
    {
        //matchingTiles1 = new List<GameTile>();
        _config = FindObjectOfType<Config>();
        _board = FindObjectOfType<BoardFill>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public TweenerCore<Vector3, Vector3, VectorOptions> UpdatePosition()
    {
        var transformPosition = transform.position;
        transformPosition.x = Row;
        transformPosition.y = Column;
        return transform.DOMove(transformPosition, 0.2f);
    }
    
    
    public List<GameTile> FindMatch()
    {
        var matchingTiles = new List<GameTile>();
        var directionsRow = new List<Vector3>(new []{Vector3.left, Vector3.right});
        var directionsColumn = new List<Vector3>(new []{Vector3.up, Vector3.down});
        foreach (var direction in directionsRow)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            var i = 0;
            while (hit.collider != null && mark == hit.collider.GetComponent<GameTile>().mark)
            {
                i++;
                if (i > 5) break;
                if(!matchingTiles.Contains(hit.collider.gameObject.GetComponent<GameTile>())) 
                    matchingTiles.Add(hit.collider.gameObject.GetComponent<GameTile>());
                hit = Physics2D.Raycast(hit.collider.transform.position + direction, direction);
            }
        }
        if (matchingTiles.Count < 3) matchingTiles.Clear();
        foreach (var direction in directionsColumn)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
            var i = 0;
            while (hit.collider != null && mark == hit.collider.GetComponent<GameTile>().mark)
            {
                i++;
                if (i > 5) break;
                if(!matchingTiles.Contains(hit.collider.gameObject.GetComponent<GameTile>())) 
                    matchingTiles.Add(hit.collider.gameObject.GetComponent<GameTile>());
                hit = Physics2D.Raycast(hit.collider.transform.position + direction, direction);
            }
        }
        return matchingTiles;
    }

    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, $"{Row}, {Column}");
    }
}


/*private void ClearMatch(Vector2[] paths)
{
    List<GameObject> matchingTiles = new List<GameObject>();
    for (int i = 0; i < paths.Length; i++)
    {
        matchingTiles.AddRange(FindMatch(paths[i]));
    }
    if (matchingTiles.Count >= 2) 
    {
        matchFound = true; 
    }
}*/
