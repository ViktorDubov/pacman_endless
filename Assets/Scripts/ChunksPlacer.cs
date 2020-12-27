using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksPlacer : MonoBehaviour
{
    public Transform Player;
    public Chunk[] ChunkPrefabs;
    public Chunk firstChunk;
    public Dotmap dotmap;
    public Blocklock block;

    public EnemyMove enemyPrefab;
    
    public int numberoflevel;   

    private List<Chunk> spawnedChunks = new List<Chunk>();    
        
    void Start()
    {
        spawnedChunks.Add(firstChunk);
        numberoflevel = 1;
    }

    void Update()
    {
        if(Player.position.y > spawnedChunks[spawnedChunks.Count - 1].End.position.y - 15)
        {
            SpawnChunk();
            numberoflevel = numberoflevel + 1;
        }        
    }
    
    private void SpawnChunk()
    {
        Chunk newChunk = Instantiate(ChunkPrefabs[Random.Range(0, ChunkPrefabs.Length)]);
                
        newChunk.transform.position = spawnedChunks[spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;
        Dotmap newDotmap = Instantiate(dotmap);
        newDotmap.transform.position = newChunk.transform.position;
        newDotmap.transform.SetParent(newChunk.transform);

        spawnedChunks.Add(newChunk);
        for (int i = 0; i < numberoflevel; i++)
        {
            EnemyMove enemyMove = Instantiate(enemyPrefab, newChunk.Startpoint);
        }

        if (spawnedChunks.Count>3)
        {
            Destroy(spawnedChunks[0].gameObject);
            spawnedChunks.RemoveAt(0);

            Blocklock newblock = Instantiate(block);
            newblock.transform.position = spawnedChunks[0].transform.position;
            newblock.transform.SetParent(spawnedChunks[0].transform);
        }
    }
}
