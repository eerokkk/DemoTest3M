using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Gameplay : MonoBehaviour
{
    public  Sprite[] fruitsPrefab = new Sprite[5]; //массив со спрайтами фруктов
    public GameObject fruitPrefab; //префаб для спавна

    [Header("Размер игровой сетки")] 
    public Vector2Int boardSize = new Vector2Int(5,6);

    private GameObject[,] fruitsMassive; // двумерный массив, хранящий все фрукты на игровом поле
    private static bool isDestroy = false;
    
    private void Awake()
    {
        FillGameField();
    }

    private void FillGameField() // метод заполнения игрового поля
    {
        fruitsMassive = new GameObject[8,5];
        
        Sprite[] spriteLeft = new Sprite[8]; // массив со спрайтами в столбце
        Sprite spriteBelow = null; // переменная для хранения нижнего от проверяемого спрайта

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject gg = Instantiate(fruitPrefab, transform, true);
                fruitsMassive[i, j] = gg;
                List<Sprite> possibleSprite = new List<Sprite>(); // лист с возможными спрайтами для проверяемого спрайта
                possibleSprite.AddRange(fruitsPrefab);
                possibleSprite.Remove(spriteLeft[j]);
                possibleSprite.Remove(spriteBelow);
                Sprite sprite = possibleSprite[Random.Range(0, possibleSprite.Count)];
                gg.GetComponent<SpriteRenderer>().sprite = sprite;
                spriteLeft[j] = sprite;
                spriteBelow = sprite;
                gg.GetComponent<SpriteRenderer>().size = new Vector2(120, 120);
                gg.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
                Debug.Log(fruitsMassive[i,j]);
            }
        }
    }
}
