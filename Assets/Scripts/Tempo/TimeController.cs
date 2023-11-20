using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
   [SerializeField] public float timeMultiplier;
   [SerializeField] public float startHour;
   [SerializeField]TextMeshProUGUI timeText;
   [SerializeField]TextMeshProUGUI dayOrNightText;

   [SerializeField] public Light sunLight;
   [SerializeField] public float sunriseHour;
   [SerializeField] public float sunsetHour;
   [SerializeField] public Color dayAmbientLight;
   [SerializeField] public Color nightAmbientLight;
   [SerializeField] public AnimationCurve lightChangeCurve;
   [SerializeField] public float maxSunLightIntensity;
   [SerializeField] public Light moonLight;
   [SerializeField] public float maxMoonLightIntensity;

   public DateTime currentTime;

   public TimeSpan sunriseTime;
   public TimeSpan sunsetTime;



    public void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    public void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    public void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            dayOrNightText.text = ("Day:");
        }
        else
        {
            dayOrNightText.text = ("Night:");
        }
    }

    public void RotateSun()
    {
        float sunLightRotation;

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes/sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0,180,(float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime,currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes/ sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360,(float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation,Vector3.right);
    }

    public void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0,maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity,0,lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    public TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if(difference.TotalSeconds < 0)
        {
                difference += TimeSpan.FromHours(24);
        }
        return difference;
    }
}
