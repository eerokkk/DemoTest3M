using System;
using System.ComponentModel;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Helpers
{
    public static void ChangeMarkRandom(this GameTile gameTile)
    {
        var lengthMarkEnum = Enum.GetValues(typeof(Mark)).Length - 1;
        gameTile.Mark = (Mark) Random.Range(0, lengthMarkEnum);
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
}
