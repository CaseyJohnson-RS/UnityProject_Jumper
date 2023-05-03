using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, IGamePlayListener
{
    [SerializeField]
    private MovingController movingController;

    // В будущем это должно быть реализовано через
    // путь к папке с префабами, и вытаскивать объекты
    // по именам 'character_[num]' и platform_[num]'
    public GameObject character;
    public List<GameObject> platforms;

    [SerializeField]
    private Transform characterStart;
    [SerializeField]
    private Transform platformStart;

    private GameObject spawnedCharacter;
    private List<GameObject> spawedPlatforms = new List<GameObject>();

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Start()
    {

        SetupGame();

    }

    public void OnPlay()
    {
        StartGame();
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void SetupGame()
    {
        if (platforms.Count == 0)
        {
            Debug.LogWarning("'GameController.platforms' должен содержать хотя бы одну платформу!");
            return;
        }

        // Setup character
        spawnedCharacter = Instantiate(character);
        spawnedCharacter.transform.position = characterStart.transform.position;

        // Setup the first platform
        spawedPlatforms.Add(Instantiate(platforms[0]));
        spawedPlatforms[0].transform.position = platformStart.transform.position;

    }

    private void StartGame()
    {
        movingController._chr = spawnedCharacter.GetComponent<Character>();
    }


}
