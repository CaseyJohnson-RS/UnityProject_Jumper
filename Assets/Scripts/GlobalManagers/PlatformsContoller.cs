using System.Collections.Generic;
using UnityEngine;

public class PlatformsContoller : MonoBehaviour, IGameFinishListener
{
    [SerializeField]
    private Transform platformStart;

    [SerializeField]
    private string platformPrefabsFolder = "Prefabs/";

    [Range(0.1f,5f)]    public float SpawnMinDistanceY = 1f;
    [Range(0.1f, 5f)]   public float SpawnMaxDistanceY = 2.75f;

    private List<GameObject> platformPrefabs = new List<GameObject>();
    private List<GameObject> spawedPlatforms = new List<GameObject>();

    private float speed = 0f;
    public float Speed { get { return GameStateMachine.state == GameStateMachine.GameState.PLAY ? speed : 0f; } }

    // Included services
    private CharacterController ChContoller;
    private Character character;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 


    private void Start()
    {
        ChContoller = ServiceProvider.GetService<CharacterController>();
        character = ChContoller.Character.GetComponent<Character>();
        Initialize();
    }

    private void Update()
    {
        if (GameStateMachine.state == GameStateMachine.GameState.PLAY)
        {
            speed = CalculatePlatformsSpeed(ChContoller.Score);            
        }

        if (GameStateMachine.state == GameStateMachine.GameState.PLAY || GameStateMachine.state == GameStateMachine.GameState.FREE)

            UpdatePlatforms();
    }

    public void OnFinish()
    {
        DestroyPlatforms();
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

    private void Initialize()
    {
        for (int i = 0; ; ++i)
        {
            string path = platformPrefabsFolder + "platform_" + i.ToString();

            if (System.IO.File.Exists("Assets/Resources/" + path + ".prefab"))
            {
                platformPrefabs.Add(Resources.Load<GameObject>(path));
            }
            else break;
        }

        if (SpawnMinDistanceY > SpawnMaxDistanceY)
        {
            Debug.Log("Дурак? Минимальное и максимальное отличить не можешь?");
            var a = SpawnMinDistanceY;
            SpawnMinDistanceY = SpawnMaxDistanceY;
            SpawnMaxDistanceY = a;
        }
    }

    private float CalculatePlatformsSpeed(float score)
    {
        // Нужно хорошенько испытать и подумать над расчетом скорости
        // Пока это просто линейная зависимость
        float res = 0.003f + score * 0.00005f;

        if (ChContoller.Character == null)
        {
            return  res;
        }
        
        float posY = ChContoller.Character.transform.position.y;

        float fact = 1f;

        if (posY < -3f)
            fact = Mathf.Clamp(1f - (-3f - posY) / 2f, 0.7f, 1f);

        if (posY > 0.5f && character.IsGrounded)
            fact = 1f + (posY - 0.5f) / 4.5f * 8f;

        return res * fact;
    }

    private GameObject SpawnPlatform(int index)
    {
        GameObject platform = Instantiate(platformPrefabs[index]);

        spawedPlatforms.Add(platform);

        platform.GetComponent<Platform>().platformsController = this;

        return platform;
    }

    private GameObject SpawnRandomPlatform()
    {
        return SpawnPlatform(Random.Range(0, platformPrefabs.Count));

    }

    private void UpdatePlatforms()
    {
        if(spawedPlatforms.Count == 0)
        {
            GameObject platform = SpawnPlatform(0);
            platform.transform.position = platformStart.position;

            return;
        }

        if (spawedPlatforms[0].transform.position.y <= -10f)
        {
            Destroy(spawedPlatforms[0]);
            spawedPlatforms.RemoveAt(0);
        }

        float posY = spawedPlatforms[spawedPlatforms.Count - 1].transform.position.y;
        float posX = spawedPlatforms[spawedPlatforms.Count - 1].transform.position.x;

        if (posY <= 12f)
        {
            GameObject platform = SpawnRandomPlatform();
            float newY = posY + Random.Range(SpawnMinDistanceY, SpawnMaxDistanceY);
            float newX = posX;

            while (Mathf.Abs(newX - posX) <= 1.25f)
            {
                newX = Random.Range(-1.5f, 1.5f);
            }

            platform.transform.position = new Vector3(newX, newY, 1);
        }
    }

    private void DestroyPlatforms()
    {
        foreach( GameObject platform in spawedPlatforms)
        {
            Destroy(platform);
        }

        spawedPlatforms.Clear();
    }

    public Transform GetSpawnPlatform(float Y)
    {
        Y = Y < 0 ? 0 : Y;
        Y = Y > 1 ? 1 : Y;

        foreach ( GameObject platform in spawedPlatforms)
        {
            if (platform.transform.position.y >= Y)
            {
                return platform.transform;
            }
        }

        Debug.Log("Нет подходящих платформ!");

        return null;
    }
}
