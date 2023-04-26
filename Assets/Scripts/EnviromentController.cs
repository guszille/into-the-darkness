using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    [SerializeField] private Transform[] groundArray;
    [SerializeField] private float translationSpeed = -10f;
    [SerializeField] private float translationOffsetXPosition = 60f;
    [SerializeField] private float lastXPosition = -20f;

    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.PLAYING)
        {
            foreach (Transform groundTransform in groundArray)
            {
                UpdateGroundPosition(groundTransform);
            }
        }
    }

    private void UpdateGroundPosition(Transform groundTransform)
    {
        groundTransform.transform.Translate(new Vector3(translationSpeed * Time.deltaTime, 0f, 0f), Space.World);
        
        if (groundTransform.transform.position.x <= lastXPosition)
        {
            groundTransform.transform.Translate(new Vector3(translationOffsetXPosition, 0f, 0f), Space.World);
        }
    }
}
