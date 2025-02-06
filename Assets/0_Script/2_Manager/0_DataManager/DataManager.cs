using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public partial class DataManager : MonoBehaviour // Data Field
{

}
public partial class DataManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
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

    public void LoadCsv<T>(Dictionary<string, T> dataDict, string path) where T : BaseInformation, new()
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
            for (int i = 0; i < csvValues.Length; i++)
            {
                FieldInfo currentField = fieldInfoArray[i];
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