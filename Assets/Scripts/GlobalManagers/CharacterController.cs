using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour,
    IGamePlayListener,
    IGameFinishListener,
    IGameGetFreeStateListener
{

    [SerializeField]
    private Transform characterStart;

    [SerializeField]
    private string characterPrefabsFolder = "Prefabs/";

    private List<GameObject> characterPrefabs = new List<GameObject>();

    private GameObject character;
    public GameObject Character {  get { return character; } }

    private float score;
    public float Score { get { return score; } }

    private float lastCharacterY;

    private bool CharacterIsDeadFirstTime = false;

    // Included services
    private PlatformsContoller platformsContoller;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Start()
    {
        platformsContoller = ServiceProvider.GetService<PlatformsContoller>();

        Initialize();
        SpawnCharacter();
    }

    private void Update()
    {
        UpdateScore();
    }

    public void OnPlay()
    {
        StartGame();
    }

    public void OnFinish()
    {
        DestroyCharacter();
    }

    public void OnGetFreeState()
    {
        SpawnCharacter();
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Initialize()
    {
        for (int i = 0; ; ++i)
        {
            string path = characterPrefabsFolder + "character_" + i.ToString();

            if (System.IO.File.Exists("Assets/Resources/" + path + ".prefab"))
            
                characterPrefabs.Add(Resources.Load<GameObject>(path));
            
            else 

                break;
        }
    }

    private void SpawnCharacter()
    {
        character = Instantiate(characterPrefabs[Random.Range(0,2)]);
        character.transform.position = characterStart.transform.position;
    }

    private void DestroyCharacter()
    {
        Destroy(character);
        character = null;
    }

    private void StartGame()
    {
        Character _charcater = character.GetComponent<Character>();
        _charcater.OnDie.AddListener(CharacterIsDead);

        CharacterIsDeadFirstTime = true;

        lastCharacterY = character.transform.position.y;
        score = 0f;
    }

    private void CharacterIsDead()
    {
        if (CharacterIsDeadFirstTime)
        {
            CharacterIsDeadFirstTime = false;

            if (SuggestToRevive())

                ReviveCharacter();
        }
        else
            
            GameStateMachine.Finish();

    }

    private void UpdateScore()
    {
        if (GameStateMachine.state == GameStateMachine.GameState.PLAY)
        {
            float d = character.transform.position.y - lastCharacterY;
            score += d > 0 ? d : 0;

            lastCharacterY = character.transform.position.y;
        }
    }

    private bool SuggestToRevive()
    {
        //Somecode...
        Debug.Log("SuggestToRevive...");

        return true;
    }

    private void ReviveCharacter()
    {
        Vector3 position = platformsContoller.GetSpawnPlatform(character.transform.position.y).position;
        character.GetComponent<Character>().Revive(position + Vector3.up);
    }

}
