using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using DG.Tweening;

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
    
    public int Column { get; set; }
    public int Row { get; set; }

    private void Awake()
    {
        _config = FindObjectOfType<Config>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdatePosition()
    {
        var transformPosition = transform.position;
        transformPosition.x = Row;
        transformPosition.y = Column;
        transform.DOMove(transformPosition, 0.2f);
    }
    
    
    public List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        while (hit.collider != null && mark == hit.collider.GetComponent<GameTile>().mark)
        {
            matchingTiles.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
        }
        return matchingTiles;
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
}
