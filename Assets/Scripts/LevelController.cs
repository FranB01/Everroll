using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    int platformNumber = 1;
    const int PLATFORM_LENGTH = 100;
    const int DISTANCE_TO_SPAWN = 150;

    public List<GameObject> tiles;

    private PlayerMovement player;


    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > platformNumber * PLATFORM_LENGTH - DISTANCE_TO_SPAWN)
        {
            AddTile();
        }
    }

    public void AddTile()
    {
        float newPlatformZ = platformNumber * PLATFORM_LENGTH;
        Instantiate(tiles[UnityEngine.Random.Range(0, tiles.Count)], new Vector3(0, 0, newPlatformZ), Quaternion.Euler(-90, 0, 0));
        platformNumber++;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }

}
