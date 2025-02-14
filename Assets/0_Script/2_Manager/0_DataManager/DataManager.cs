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
            for (int i = 1; i < csvFileArray.Length; i++) // 0번째 레이블 == 변수 이름
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
            // TODO 사고 발생 : BaseInformation 만 상속받았을 때 잘 돌아감,
            // 문제점 : combatBaseInfo가 있을 때 BaseInfo->CombatBaseInfo->PlayerStatData 이렇게 상속 받았을 때
            // 변수 순서가 꼬여서 값 순서가 뒤엉킴
            // GetFields()를 뽑아보면 1. playerStatData의 멤버, 2. combatBase의 멤버, 3.baseInfo의 멤버 순으로 들어감.

            // TODO 고민중인 해결 방법 :
            // 1. 매개변수로 csv에 선언된 변수 이름을 받거나 전역 변수로 해당 이름들을 저장.
            // 2. LinQ로 탐색하며 이름이 같은 변수에 해당 이름과 같은 index에 위치한 값을 저장. ( 성능 개 박살날수도?;;)
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