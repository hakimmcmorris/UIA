using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text scoreLabel;
    [SerializeField] SettingsPopup settingsPopup;

    int _score;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();

        settingsPopup.Close();
    }

    void OnEnemyHit()
    {
        _score++;
        scoreLabel.text = _score.ToString();
    }

    public void OnOpenSettings()
    {
        Debug.Log("open settings!!!");
        settingsPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down!!");
    }

}
