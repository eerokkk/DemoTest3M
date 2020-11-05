using System;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Helpers
{
    public static void ChangeMarkRandom(this GameTile gameTile, GameTile[,] tiles)
    {
        int[] spriteRight = new int[tiles.GetLength(1)];
        int spriteUp = -1;

        for (int y = 0; y < tiles.GetLength(1); y++)
        {
            List<int> possibleSprite = new List<int>();
            possibleSprite.AddRange(new [] {0,1,2,3,4,5});
            possibleSprite.Remove(spriteRight[y]);
            possibleSprite.Remove(spriteUp);
            int sprite = possibleSprite[Random.Range(0, possibleSprite.Count)];
            spriteRight[y] = sprite;
            spriteUp = sprite;
            gameTile.Mark = (Mark)sprite;
        }
    }
    
    public static TweenerCore<Color, Color, ColorOptions> DeathAnimation(this GameTile gameTile)
    {
        gameTile.gameObject.transform.DOScale(1.25f, 0.2f);
        var spriteRenderer = gameTile.gameObject.GetComponent<SpriteRenderer>();
        return spriteRenderer.DOFade(0f, 0.2f);
    }
    
    public static TweenerCore<Color, Color, ColorOptions> BornAnimation(this GameTile gameTile)
    {
        gameTile.GetComponent<SpriteRenderer>().DOFade(0f, 0f);
        gameTile.transform.DOScale(1f, 0.2f);
        return gameTile.GetComponent<SpriteRenderer>().DOFade(1f, 0.4f);
    }
    
    public static Tuple<int, int> CoordinatesOf(this GameTile[,] matrix, GameTile value)
    {
        int w = matrix.GetLength(0);
        int h = matrix.GetLength(1);

        for (int x = 0; x < w; ++x)
        {
            for (int y = 0; y < h; ++y)
            {
                if (matrix[x, y].Equals(value))
                    return Tuple.Create(x, y);
            }
        }

        return Tuple.Create(-1, -1);
    }
}
