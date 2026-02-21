using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
    }
    #endregion

    [ContextMenu("Generate OnlyStat")]
    void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "ScriptableObjects/Selections/OnlyStats/OnlyStats.json");

        if (!File.Exists(path))
        {
            Debug.LogError($"JSON ������ ã�� �� �����ϴ�: {path}");
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

            // �̹� �����ϴ��� Ȯ��
            SelectEffect_Stat existingAsset = AssetDatabase.LoadAssetAtPath<SelectEffect_Stat>(assetPath);

            if (existingAsset != null)
            {
                Debug.LogWarning($"�̹� �����ϴ� ����: {data.fileName}");
                skippedCount++;
                continue;
            }

            // �� �ν��Ͻ� ���� �� ������ �Ҵ�
            SelectEffect_Stat asset = ScriptableObject.CreateInstance<SelectEffect_Stat>();

            asset.displayName = data.displayName;
            asset.stats = new GameHandlerScript.StatType[data.stats.Length];
            for (int i = 0; i < data.stats.Length; i++)
            {
                asset.stats[i] = (GameHandlerScript.StatType)data.stats[i];
            }
            asset.amounts = data.amounts;
            asset.aftermathDialog = data.aftermathDialog;

            // ���� ����
            AssetDatabase.CreateAsset(asset, assetPath);
            createdCount++;
        }

        // ������� ���� �� ���ΰ�ħ
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"�۾� �Ϸ�! ������: {createdCount}, �ǳʶ�: {skippedCount}");
    }
}
