using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Game Manager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] TMP_Text _winText;

    private void Awake()
    {
        instance = this;
    }

    public void StopGame(string winnersName)
    {
        _winText.text = winnersName + " Won";
    }
}
