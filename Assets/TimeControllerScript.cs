using TMPro;
using UnityEngine;

public class TimeControllerScript : MonoBehaviour
{
    private double startupTime;
    private double MonthToSeconds = 5;
    
    public TextMeshProUGUI timeText;
    
    void Awake()
    {
        startupTime = Time.realtimeSinceStartupAsDouble;
    }

    // Update is called once per frame
    void Update()
    {
        var totalSeconds = GetTime();
        var year = ((int)(totalSeconds / (MonthToSeconds * 12))).ToString() + "년 ";
        if (year == "0년 ")
            year = "";
        var month = ((int)(totalSeconds % (MonthToSeconds * 12) / MonthToSeconds)).ToString() + "개월";
        if (month == "0개월")
            month = "";
        if (year == "" && month == "")
            timeText.text = "새로운 시작";
        else
            timeText.text = "왕국력 " + year + month;
    }

    public double GetTime()
    {
        return Time.realtimeSinceStartupAsDouble - startupTime;
    }
}
