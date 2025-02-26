using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public partial class DataManager : MonoBehaviour // Data Field
{
    public EnemyManagerData EnemyManagerData { get; private set; } = null;
    public PlayerStatData PlayerStatData { get; private set; } = null;
    public EnemyStatData EnemyStatData { get; private set; } = null;
}
public partial class DataManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        EnemyManagerData = new EnemyManagerData();
        PlayerStatData = new PlayerStatData();
        EnemyStatData = new EnemyStatData();
    }
    public void Initialize()
    {
        Allocate();
        Setup();
        EnemyManagerData.Initialize();
        PlayerStatData.Initialize();
        EnemyStatData.Initialize();
    }
    private void Setup()
    {

    }
}
public partial class DataManager : MonoBehaviour // Property
{
    #region Json
    private Wrapper<T> LoadJson<T>(string path) where T : BaseInformation
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
            string[] csvFieldName = csvFileArray[0].Split(",");

            for (int i = 1; i < csvFileArray.Length; i++) // 0번째 레이블 == 변수 이름
            {
                string[] csvValue = csvFileArray[i].Split(",");
                dataDict.Add(csvValue[0], ParseCsv<T>(csvFieldName, csvValue));
            }
        }
    }

    public T ParseCsv<T>(string[] csvFieldName, string[] csvValues) where T : BaseInformation, new()
    {
        T data = new T();
        FieldInfo[] fieldInfoArray = typeof(T).GetFields();
        string[] arrayData;
        /* if (csvValues.Length == fieldInfoArray.Length)
         {
             data.index = csvValues[0];

             for (int i = 0; i + 1 < csvValues.Length; i++)
             {
                 FieldInfo currentField = fieldInfoArray[i];
                 print(currentField.Name);
                 string currentCsvValue = csvValues[i + 1];

                 currentField.SetValue(data, Convert.ChangeType(currentCsvValue, currentField.FieldType));

                 //if (currentField.FieldType == typeof(string))
                 //    currentField.SetValue(data, currentCsvValue);

                 //else if (currentField.FieldType == typeof(int))
                 //{
                 //    if (int.TryParse(currentCsvValue, out int parseValue))
                 //        currentField.SetValue(data, parseValue);
                 //}
                 //else if (currentField.FieldType == typeof(float))
                 //{
                 //    if (float.TryParse(currentCsvValue, out float parseValue))
                 //        currentField.SetValue(data, parseValue);
                 //}
                 //else
                 //    Debug.LogWarning($"ParsingError : Current Data Type is {currentField.FieldType.ToString()}");
             }
         }
         else
         {
             Debug.LogWarning($"ParsingError : {typeof(T)}");
             return null;
         }
        */

        for (int i = 0; i < csvFieldName.Length; i++)
        {
            string csvKey = csvFieldName[i].Replace("\r", "");
            string csvValue = csvValues[i].Replace("\r", "");
            FieldInfo fieldInfo = fieldInfoArray.FirstOrDefault(p => p.Name == csvKey);
            if (fieldInfo != null)
            {
                if (fieldInfo.FieldType.IsArray)
                {
                    Type elementType = fieldInfo.FieldType.GetElementType();
                    arrayData = csvValue.Split(' ');
                    Array array = Array.CreateInstance(elementType, arrayData.Length);
                    for (int j = 0; j < array.Length; j++)
                    {
                        try
                        {
                            var element = Convert.ChangeType(arrayData[j], elementType);
                            array.SetValue(element, j);
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning($"Error converting value '{arrayData[j]}' to {elementType.Name}: {e.Message}");
                        }
                    }
                    fieldInfo.SetValue(data, array);
                }
                else
                    fieldInfo.SetValue(data, Convert.ChangeType(csvValues[i], fieldInfo.FieldType));
            }
            else
                Debug.LogWarning($"ParsingError : {typeof(T)}");
        }
        return data;
    }

    #endregion 
}