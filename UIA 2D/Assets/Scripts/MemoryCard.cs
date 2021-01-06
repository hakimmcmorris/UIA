using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] Sprite image;
    [SerializeField] SceneController controller;

    int _id;

    public int Getid()
    {
        return _id;
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    private void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal())
        {
            cardBack.SetActive(false);
            controller.CardReveal(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }

    private void Update()
    {
        
    }
}
