using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{
    [SerializeField] private float despawnXPosition = -20f;

    private float translationSpeed = 0f;

    private void Update()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.PLAYING)
        {
            transform.Translate(new Vector3(translationSpeed * Time.deltaTime, 0f), Space.World);

            if (transform.position.x <= despawnXPosition)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetTranslationSpeed(float translationSpeed)
    {
        this.translationSpeed = translationSpeed;
    }
}
