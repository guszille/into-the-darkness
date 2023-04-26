using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainCharacterVisualController : MonoBehaviour
{
    private const string IS_ROTATING_TRIGGER = "IsRotating";

    [SerializeField] private Light2D light2d;
    [SerializeField] private float lightRecoveryFactor = 4f;
    [SerializeField] private float lightIntensityMax = 4f;
    [SerializeField] private float lightOuterRadiusMax = 20f;

    private Animator animator;
    private float lightIntensity = 1f;
    private float lightIntensityMin = 1f;
    private float lightOuterRadius = 2.5f;
    private float lightOuterRadiusMin = 2.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        MainCharacterController.Instance.OnGrounded += MainCharacter_OnGrounded;
        MainCharacterController.Instance.OnJumped += MainCharacter_OnJumped;
    }

    private void Update()
    {
        float lightRecoveryValue = lightRecoveryFactor * Time.deltaTime;

        lightIntensity = Mathf.Max(lightIntensityMin, lightIntensity - lightRecoveryValue);
        lightOuterRadius = Mathf.Max(lightOuterRadiusMin, lightOuterRadius - lightRecoveryValue);

        light2d.intensity = lightIntensity;
        light2d.pointLightOuterRadius = lightOuterRadius;
    }

    private void MainCharacter_OnGrounded(object sender, System.EventArgs args)
    {
        lightIntensity = lightIntensityMax;
        lightOuterRadius = lightOuterRadiusMax;
    }

    private void MainCharacter_OnJumped(object sender, System.EventArgs args)
    {
        animator.SetTrigger(IS_ROTATING_TRIGGER);
    }
}
