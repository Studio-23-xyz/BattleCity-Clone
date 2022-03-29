using Cysharp.Threading.Tasks;
using GameUtils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


// ReSharper disable All

public class LevelGeneration : MonoBehaviour
{
    public List<GameObject> AllRoomPoint;
    public GameObject[] Rooms;




    public Transform[] StartingPosForEnemy;


    public GameObject CityBlock;
    public Transform CityGenerationPoint;
    public float CityGenerationXOffset;

    public GameObject[] PlayerSpawner;




    public Collider2D[] blocksForEnemy;
    public Collider2D[] blocksForPlayer;
    public Waves wave;


    public float _resetTime;


    private int _direction;
    private bool _generateRoom = true;
    private GameObject CurrentRoom;
    private int DownCounter;

    private float _timeToGenerateRoom;

    public UnityEvent GeneratePlayer;
    public AudioClip LevelStartAudio;
    public AudioClip PlayerIdleAudio;



    void Awake()
    {

    }

    async void Start()
    {
        GenerateCity();
        AudioManager.Instance.PlayBGM(LevelStartAudio);
        await UniTask.Delay(TimeSpan.FromSeconds(LevelStartAudio.length - 2f), ignoreTimeScale: false);
        GeneratePlayerPosition();
    }



    [ContextMenu("Generate City")]
    void GenerateCity()
    {
        Instantiate(CityBlock, CityGenerationPoint.position, Quaternion.identity);

        GenerateRoom();
    }


    [ContextMenu("Generate Room")]
    async void GenerateRoom()
    {
        foreach (var room in AllRoomPoint)
        {
            var chunk = Instantiate(Rooms[Random.Range(0, Rooms.Length)], room.transform.position, Quaternion.identity);
            SpawnPoint chunkPoint = chunk.GetComponent<SpawnPoint>();
            chunkPoint.Initialize();


            await UniTask.Delay(TimeSpan.FromSeconds(.1f), ignoreTimeScale: false);
        }

        DestroyBlock();

    }

    [ContextMenu("Destroy Block")]
    void DestroyBlock()
    {
        for (int i = 0; i < StartingPosForEnemy.Length; i++)
        {
            blocksForEnemy = Physics2D.OverlapCircleAll(StartingPosForEnemy[i].position, .5f);

            if (blocksForEnemy != null)
            {
                foreach (var block in blocksForEnemy)
                {
                    if (!block.gameObject.CompareTag("barrier"))
                        Destroy(block.gameObject);
                }
            }
        }

        int sign = 1;

        for (int i = 0; i < 2; i++)
        {
            sign = -sign;
            blocksForPlayer = Physics2D.OverlapCircleAll(new Vector3(CityGenerationPoint.position.x - CityGenerationXOffset * sign,
                CityGenerationPoint.position.y, CityGenerationPoint.position.z), Random.Range(.5f, 2f));

            if (blocksForPlayer != null)
            {
                foreach (var block in blocksForPlayer)
                {
                    if (!block.gameObject.CompareTag("barrier") && !block.gameObject.CompareTag("Player"))
                        Destroy(block.gameObject);
                }
            }
        }
    }

    [ContextMenu("Generate Player")]

    async void GeneratePlayerPosition()
    {

        GeneratePlayer?.Invoke();

        /*foreach (var spawner in PlayerSpawner)
        {
            spawner.GetComponent<Spawner>().AnimationObject.SetActive(true);
        }

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), ignoreTimeScale: false);


        foreach (var spawner in PlayerSpawner)
        {
            spawner.GetComponent<Spawner>().AnimationObject.SetActive(false);
        }*/




        /*PlayerTransform1.transform.position = new Vector3(CityGenerationPoint.position.x - CityGenerationXOffset,
            CityGenerationPoint.position.y, CityGenerationPoint.position.z);

        PlayerTransform2.transform.position = new Vector3(CityGenerationPoint.position.x + CityGenerationXOffset,
            CityGenerationPoint.position.y, CityGenerationPoint.position.z);*/

        GenerateWave();

    }

    [ContextMenu("Generate Enemy Wave")]
    void GenerateWave()
    {
        wave.WaveGeneration = true;
        Game.Instance.Triggers.PlayerGenerated?.Invoke();
    }


}
