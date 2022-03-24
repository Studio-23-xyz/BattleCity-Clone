using Cysharp.Threading.Tasks;
using GameUtils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;




public class LevelGeneration : MonoBehaviour
{
    public List<GameObject> AllRoomPoint;
    public GameObject[] Rooms;



    
    public Transform[] StartingPosForEnemy;


    public GameObject CityBlock;
    public Transform CityGenerationPoint;
    public float CityGenerationXOffset;



    public Collider2D[] BlocksForEnemy;
    public Collider2D[] BlocksForPlayer;
    public Waves Wave;



    public UnityEvent GeneratePlayer;
    public AudioClip LevelStartAudio;


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
            var chunk=Instantiate(Rooms[Random.Range(0, Rooms.Length)], room.transform.position, Quaternion.identity);
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
            BlocksForEnemy = Physics2D.OverlapCircleAll(StartingPosForEnemy[i].position, .5f);

            if (BlocksForEnemy != null)
            {
                foreach (var block in BlocksForEnemy)
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
            BlocksForPlayer = Physics2D.OverlapCircleAll(new Vector3(CityGenerationPoint.position.x - CityGenerationXOffset * sign,
                CityGenerationPoint.position.y, CityGenerationPoint.position.z), Random.Range(.5f, 2f));

            if (BlocksForPlayer != null)
            {
                foreach (var block in BlocksForPlayer)
                {
                    if (!block.gameObject.CompareTag("barrier") && !block.gameObject.CompareTag("Player"))
                        Destroy(block.gameObject);
                }
            }
        }

    }

    [ContextMenu("Generate Player")]

    void GeneratePlayerPosition()
    {
        GeneratePlayer?.Invoke();
        GenerateWave();
    }

    [ContextMenu("Generate Enemy Wave")]
    void GenerateWave()
    {
        Wave.WaveGeneration = true;
    }


}
