using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter Instance;
    public int characterNum = 0;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnSelected(int number)
    {
        characterNum = number;
        SceneManager.LoadScene("GameScene");
    }
}
