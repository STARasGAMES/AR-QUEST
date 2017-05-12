using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightPulse : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 1f;
    public float speed = 1f;

    Light _light;

    float random;

    void Start()
    {
        random = Random.Range(0.0f, 65535.0f);
        _light = GetComponent<Light>();
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time * speed);
        _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }

}