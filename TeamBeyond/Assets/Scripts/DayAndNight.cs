using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    [Header("Gradients")]
    [SerializeField] private Gradient fogGradient;
    [SerializeField] private Gradient ambientGradient;
    [SerializeField] private Gradient directionLightGradient;
    [SerializeField] private Gradient skyboxTintGradient;

    [Header("Enviromental Assets")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Material skyboxMaterial;

    [Header("Variables")]
    [SerializeField] private float dayDurationInSeconds = 60f;
    [SerializeField] private float rotationSpeed = 1f;

    public float currentTime;

    public Material mat;

    public Material mountainMat;
    public Material cloudMat;

    public void Start()
    {
        currentTime = currentTime == 0 ? PlayerPrefs.GetFloat("daytime", 0f) : currentTime;
    }

    private void Update()
    {
        UpdateTime();
        UpdateDayNightCycle();
        RotateSkybox();

        mountainMat.SetColor("_FogColor", fogGradient.Evaluate(currentTime));
        mountainMat.SetColor("_HazeColor", fogGradient.Evaluate(currentTime));
        mountainMat.SetFloat("_GlowStrength", fogGradient.Evaluate(currentTime).grayscale / 2.0f);

        cloudMat.SetColor("_Color", skyboxTintGradient.Evaluate(currentTime));
    }

    private void UpdateTime()
    {
        currentTime += Time.deltaTime / dayDurationInSeconds;
        currentTime = Mathf.Repeat(currentTime, 1f);

        if(Input.GetKey("]")) currentTime += 0.01f;
        if(Input.GetKey("[")) currentTime -= 0.01f;

        if(currentTime > 0.4f && currentTime < 0.7f)
        {
            mat.SetColor("_EmissionColor", new Color(0.8196f,0.583f,0) * 10);
        }
        else
        {
            mat.SetColor("_EmissionColor", new Color(0.8196f,0.583f,0) * -10);
        }

        PlayerPrefs.SetFloat("daytime", currentTime);
    }

    private void UpdateDayNightCycle()
    {
        float sunPosition = Mathf.Repeat(currentTime + 0.25f, 1f);
        directionalLight.transform.rotation = Quaternion.Euler(sunPosition * 360f, 0f, 0f);

        RenderSettings.fogColor = fogGradient.Evaluate(currentTime);
        RenderSettings.ambientLight = ambientGradient.Evaluate(currentTime);
        RenderSettings.reflectionIntensity = ambientGradient.Evaluate(currentTime).grayscale;

        directionalLight.color = directionLightGradient.Evaluate(currentTime);

        skyboxMaterial.SetColor("_Tint", skyboxTintGradient.Evaluate(currentTime));
    }

    private void RotateSkybox()
    {
        float currentRotation = skyboxMaterial.GetFloat("_Rotation");
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;
        newRotation = Mathf.Repeat(newRotation, 360f);
        skyboxMaterial.SetFloat("_Rotation", newRotation);
    }

    private void OnApplicationQuit()
    {
        skyboxMaterial.SetColor("_Tint", new Color(0.5f, 0.5f, 0.5f));
    }
}
