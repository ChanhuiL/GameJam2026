using System.IO;
using UnityEditor;
using UnityEngine;
using System.Text;

public class Resource_Manager : MonoBehaviour
{
    #region Singleton Declare
    public static Resource_Manager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    [ContextMenu("Generate OnlyStat")]
    void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "ScriptableObjects/Selections/OnlyStats/OnlyStats.json");

        if (!File.Exists(path))
        {
            Debug.LogError($"JSON 파일을 찾을 수 없습니다: {path}");
            return;
        }

        string json = File.ReadAllText(path, Encoding.UTF8);
        OnlyStatDataWrapper dataList = JsonUtility.FromJson<OnlyStatDataWrapper>(json); 
        
        string folderPath = "Assets/ScriptableObjects/Selections/OnlyStats";
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        int skippedCount = 0;
        int createdCount = 0;

        foreach (var data in dataList.OnlyStats)
        {
            string assetPath = $"{folderPath}/{data.fileName}.asset";

            // 이미 존재하는지 확인
            SelectEffect_Stat existingAsset = AssetDatabase.LoadAssetAtPath<SelectEffect_Stat>(assetPath);

            if (existingAsset != null)
            {
                Debug.LogWarning($"이미 존재하는 에셋: {data.fileName}");
                skippedCount++;
                continue;
            }

            // 새 인스턴스 생성 및 데이터 할당
            SelectEffect_Stat asset = ScriptableObject.CreateInstance<SelectEffect_Stat>();

            asset.displayName = data.displayName;
            asset.stats = new GameHandlerScript.StatType[data.stats.Length];
            for (int i = 0; i < data.stats.Length; i++)
            {
                asset.stats[i] = (GameHandlerScript.StatType)data.stats[i];
            }
            asset.amounts = data.amounts;
            asset.aftermathDialog = data.aftermathDialog;

            // 파일 생성
            AssetDatabase.CreateAsset(asset, assetPath);
            createdCount++;
        }

        // 변경사항 저장 및 새로고침
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"작업 완료! 생성됨: {createdCount}, 건너뜀: {skippedCount}");
    }
}
