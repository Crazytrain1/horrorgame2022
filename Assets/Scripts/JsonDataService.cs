using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

public class JsonDataService : IDataService
{
    private const string KEY = "2";
    private const string IV = "3";
    public bool SaveData<T>(string RelativePath, T Data, bool Encrypted)
    {


        string path = Application.persistentDataPath + RelativePath;

       
            try
            {

                if (System.IO.File.Exists(path))
                {
                    Debug.Log("Data exists. Deleting old file and writing a new one!");
                    System.IO.File.Delete(path);
                Debug.Log(path);
                }
                else
                {
                    Debug.Log("Writing file for the first time!");
                }



            using FileStream stream = System.IO.File.Create(path);
            if (Encrypted)
            {
                WriteEncryptedData(Data, stream);
            }
            stream.Close();
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
                
            }
            catch (Exception e )
            {
                Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
                return false;
            }
        
      
    }

    //need to rewrite this function if we use encryption
    public bool deleteFile(string RelativePath)
    {
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            if (System.IO.File.Exists(path))
            {
                Debug.Log("Data exists. Deleting old file");
                System.IO.File.Delete(path);
                Debug.Log(path);
                
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }
    public bool pathExist(string RelativePath)
    {

        string path = Application.persistentDataPath + RelativePath;
        if (System.IO.File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void WriteEncryptedData<T> (T Data, FileStream Stream)
    {
        using Aes aesProvider = Aes.Create();
        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);
        using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(
            Stream,
            cryptoTransform,
            CryptoStreamMode.Write );

        //one time use to create an IV and Key 
        //REMOVE THIS
        Debug.Log($"Initialization Vector : {Convert.ToBase64String(aesProvider.IV)}");
        Debug.Log($"Key : {Convert.ToBase64String(aesProvider.Key)}");


        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Data)));


    }

  

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + RelativePath;

        if(!System.IO.File.Exists(path))
        {
            Debug.LogError($"Cannot load file at {path}. File does not exist!");
            throw new FileNotFoundException($"{path} does not exist!");
        }

        try
        {
            T data;
            if (Encrypted)
            {
                data = ReadEncryptedData<T>(path);
            }
            else
            {
                data = JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(path));
            }
            
            return data;
        }

        catch (Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message}{e.StackTrace}");
            throw e;

        }
    }

    private T ReadEncryptedData<T>(string Path)
    {
        byte[] fileBytes = System.IO.File.ReadAllBytes(Path);

        using Aes aesProvider = Aes.Create();
        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(
            aesProvider.Key,
            aesProvider.IV);
        using MemoryStream decryptionStream = new MemoryStream(fileBytes);
        using CryptoStream cryptoStream = new CryptoStream(
            decryptionStream,
            cryptoTransform, CryptoStreamMode.Read);

        using StreamReader reader = new StreamReader(cryptoStream);
        string result = reader.ReadToEnd();

        Debug.Log($"Decrypted result (if the following is not legible, probably wrong key or iv): {result}");
        return JsonConvert.DeserializeObject<T>(result);

    }




}
