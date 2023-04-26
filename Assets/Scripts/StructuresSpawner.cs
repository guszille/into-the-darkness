using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] structureArray;
    [SerializeField] private Vector3 spawnPosition = new Vector3(20f, -3.45f, 0f);
    [SerializeField] private float spawnCountdown = 5f;
    [SerializeField] private float translationSpeed = -10f;

    private float timeToSpawn = 0f;

    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.PLAYING)
        {
            timeToSpawn -= Time.deltaTime;

            if (timeToSpawn <= 0f)
            {
                int structureIndex = Random.Range(0, structureArray.Length);

                Transform structureTransform = Instantiate(structureArray[structureIndex], spawnPosition, Quaternion.identity, transform);
                StructureController structureController = structureTransform.GetComponent<StructureController>();

                structureController.SetTranslationSpeed(translationSpeed);

                timeToSpawn = spawnCountdown;
            }
        }
    }

    public void DestroyAllChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
