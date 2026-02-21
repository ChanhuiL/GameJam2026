using System.IO;
using UnityEngine;
using System.Text;
using System; // Serializable 사용을 위해 추가

public class Resource_Manager : MonoBehaviour
{
    #region Singleton
    public static Resource_Manager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    #endregion

    [ContextMenu("Generate OnlyStat")]
    public void LoadData()
    {
        // 유니티 에디터 상에서만 동작하도록 보장
#if UNITY_EDITOR
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

            // AssetDatabase 앞에 UnityEditor.을 꼭 붙여야 합니다.
            SelectEffect_Stat existingAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<SelectEffect_Stat>(assetPath);

            if (existingAsset != null)
            {
                Debug.LogWarning($"이미 존재하는 에셋: {data.fileName}");
                skippedCount++;
                continue;
            }

            SelectEffect_Stat asset = ScriptableObject.CreateInstance<SelectEffect_Stat>();

            asset.displayName = data.displayName;
            asset.stats = new GameHandlerScript.StatType[data.stats.Length];
            for (int i = 0; i < data.stats.Length; i++)
            {
                asset.stats[i] = (GameHandlerScript.StatType)data.stats[i];
            }
            asset.amounts = data.amounts;
            asset.aftermathDialog = data.aftermathDialog;

            // 에셋 생성
            UnityEditor.AssetDatabase.CreateAsset(asset, assetPath);
            createdCount++;
        }

        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        Debug.Log($"작업 완료! 생성됨: {createdCount}, 건너뜀: {skippedCount}");
#else
        Debug.LogWarning("AssetDatabase는 에디터에서만 사용할 수 있습니다.");
#endif
    }
}