using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public partial class DataManager : MonoBehaviour // Data Field
{
    public PlayerStatData PlayerStatData { get; private set; } = default;
}
public partial class DataManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        PlayerStatData = new PlayerStatData();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        PlayerStatData.Initialize();
    }
    private void Setup()
    {

    }
}
public partial class DataManager : MonoBehaviour // Property
{
    #region Json
    public Wrapper<T> LoadJson<T>(string path) where T : BaseInformation
    {
        string jsonData = Resources.Load<TextAsset>($"Json/{path}").ToString();

        if (string.IsNullOrEmpty(jsonData))
            Debug.LogWarning("JsonData is Null or Empty");

        return JsonUtility.FromJson<Wrapper<T>>(jsonData);
    }
    public void SetupData<T>(Dictionary<string, T> dataDict, string path) where T : BaseInformation
    {
        dataDict.Clear();
        Wrapper<T> jsonData = LoadJson<T>(path);
        foreach (T data in jsonData.array)
        {
            dataDict.Add(data.index, data);
        }
    }
    #endregion

    #region CSV

    public void LoadCsv<T>(string path, Dictionary<string, T> dataDict) where T : BaseInformation, new()
    {
        dataDict.Clear();
        TextAsset csvFile = Resources.Load<TextAsset>($"Csv/{path}");
        if (csvFile != null)
        {
            string[] csvFileArray = csvFile.text.Split("\n");
            for (int i = 1; i < csvFileArray.Length; i++) // 0��° ���̺� == ���� �̸�
            {
                string[] csvValue = csvFileArray[i].Split(",");
                dataDict.Add(csvValue[0], ParseCsv<T>(csvValue));
            }
        }
    }

    public T ParseCsv<T>(string[] csvValues) where T : BaseInformation, new()
    {
        T data = new T();
        FieldInfo[] fieldInfoArray = typeof(T).GetFields();
        if (csvValues.Length == fieldInfoArray.Length)
        {
            data.index = csvValues[0];
            // TODO ��� �߻� : BaseInformation �� ��ӹ޾��� �� �� ���ư�,
            // ������ : combatBaseInfo�� ���� �� BaseInfo->CombatBaseInfo->PlayerStatData �̷��� ��� �޾��� ��
            // ���� ������ ������ �� ������ �ھ�Ŵ
            // GetFields()�� �̾ƺ��� 1. playerStatData�� ���, 2. combatBase�� ���, 3.baseInfo�� ��� ������ ��.

            // TODO ������� �ذ� ��� :
            // 1. �Ű������� csv�� ����� ���� �̸��� �ްų� ���� ������ �ش� �̸����� ����.
            // 2. LinQ�� Ž���ϸ� �̸��� ���� ������ �ش� �̸��� ���� index�� ��ġ�� ���� ����. ( ���� �� �ڻ쳯����?;;)
            for (int i = 0; i + 1 < csvValues.Length; i++)
            {
                FieldInfo currentField = fieldInfoArray[i];
                print(currentField.Name);
                string currentCsvValue = csvValues[i + 1];

                if (currentField.FieldType == typeof(string))
                    currentField.SetValue(data, currentCsvValue);

                else if (currentField.FieldType == typeof(int))
                {
                    if (int.TryParse(currentCsvValue, out int parseValue))
                        currentField.SetValue(data, parseValue);
                }
                else if (currentField.FieldType == typeof(float))
                {
                    if (float.TryParse(currentCsvValue, out float parseValue))
                        currentField.SetValue(data, parseValue);
                }
                else
                    Debug.LogWarning($"ParsingError : Current Data Type is {currentField.FieldType.ToString()}");
            }
        }
        else
        {
            Debug.LogWarning($"ParsingError : {typeof(T)}");
            return null;
        }

        return data;
    }

    #endregion 
}