using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int GridRows = 2;
    public const int GridCol = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    [SerializeField] MemoryCard originalCard;
    [SerializeField] Sprite[] images;
    [SerializeField] TextMesh scoreLabel;

    MemoryCard _firstRevealed;
    MemoryCard _secondRevealed;
    int _score = 0;

    public bool canReveal()
    {
        return _secondRevealed == null;
    }



    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < GridCol; i++)
        {
            for (int j = 0; j < GridRows; j++)
            {
                MemoryCard card;

                if (i == 0 && j == 0)
                {
                    card = originalCard;
                } else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * GridCol + i;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = - (offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardReveal(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        } else
        {
            _secondRevealed = card;
            Debug.Log("Match? " + (_firstRevealed.Getid() == _secondRevealed.Getid()));
            StartCoroutine(CheckMatch());
        }
    }

    public int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];

        for (int i = 0; i < newArray.Length; i++)
        {
            int temp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = temp;
        }

        return newArray;
    }

    IEnumerator CheckMatch()
    {
        if (_firstRevealed.Getid() == _secondRevealed.Getid())
        {
            _score++;
            scoreLabel.text = "Score: " + _score;

        } else
        {
            yield return new WaitForSeconds(2);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
