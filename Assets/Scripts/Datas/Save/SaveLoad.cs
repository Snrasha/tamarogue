
using UnityEngine;
using System.IO;
namespace Assets.Scripts.Datas.Save
{
	public class SaveLoad
	{
		public static DataSave currentSave;
		public static Prefs currentPrefs;

		public static void Load(bool nosave)
        {
			if ( !SaveLoad.currentPrefs.loaded)
			{
				SaveLoad.currentPrefs = SaveLoad.LoadPrefs();
			}
			if (SaveLoad.currentSave==null || !SaveLoad.currentSave.loaded)
			{
				SaveLoad.currentSave = SaveLoad.LoadFile(nosave,SaveLoad.currentPrefs.currentSlot);
			}
		}

		public static Prefs LoadPrefs()
		{
			string destination = Application.persistentDataPath + "/prefs.pref";
			FileStream file;

			if (File.Exists(destination)) file = File.OpenRead(destination);
			else
			{

				Debug.LogError("File not found");
				Prefs pref = new Prefs();
				pref.loaded = true;
				pref.effectsVolume = 50;
				pref.musicVolume = 50;
				pref.totalVolume = 50;
				pref.sizeScreenChoice = 0;
				pref.languageSelect = "en";
				//pref.languageSelect = "fr";



				return pref;
			}
			string json = "";
			byte[] buffer = new byte[4096];
			int i = 1;
			while (i > 0)
			{
				i = file.Read(buffer, 0, 4096);
				if (i > 0)
				{
					json += System.Text.Encoding.Default.GetString(buffer, 0, i);
				}

			}
			if (json.Length == 0)
			{
				return new Prefs();
			}


			Prefs myObject = JsonUtility.FromJson<Prefs>(json);
			myObject.loaded = true;

			file.Close();
			return myObject;
		}
		public static void SavePrefs()
		{
			SavePrefs(SaveLoad.currentPrefs);
		}
		public static void SavePrefs(Prefs prefs)
		{
			string destination = Application.persistentDataPath + "/prefs.pref";
			string destinationbackup = Application.persistentDataPath + "/prefs_pref.dat";

			FileStream file;
			if (File.Exists(destination))
			{
				File.Copy(destination, destinationbackup, true);
				//file = File.OpenWrite(destination);
				File.Delete(destination);
				file = File.Create(destination);
			}
			else file = File.Create(destination);
			string json = JsonUtility.ToJson(prefs, true);
			file.Write(System.Text.Encoding.Default.GetBytes(json));

			file.Close();
		}

		public static void SaveFile(bool nosave)
		{
            if (nosave)
            {
				return;
            }

			DataSave dataStruct = currentSave;
			//	dataStruct.slot = (dataStruct.slot + 1) % 10;
			dataStruct.slot = currentPrefs.currentSlot;





				string destination = Application.persistentDataPath + "/save" + dataStruct.slot + ".dat";
			string destinationbackup = Application.persistentDataPath + "/save" + dataStruct.slot + "_backup.dat";

			FileStream file;

			//Debug.Log(destination);


			if (File.Exists(destination))
			{
				File.Copy(destination, destinationbackup, true);
				//file = File.OpenWrite(destination);
				File.Delete(destination);
				file = File.Create(destination);
			}
			else file = File.Create(destination);

			SaveStruct saveStruct = new SaveStruct(dataStruct);
			string json = JsonUtility.ToJson(saveStruct, true);

			file.Write(System.Text.Encoding.Default.GetBytes(json));



			file.Close();
		}

		public static DataSave LoadFile(bool nosave,int number)
		{
			string destination = Application.persistentDataPath + "/save" + number + ".dat";
			FileStream file;


			if (!nosave && File.Exists(destination)) file = File.OpenRead(destination);
			else
			{
			//	Debug.LogError("File not found");
				DataSave dataStruct1= new DataSave();

				return dataStruct1;
			}
			string json = "";
			byte[] buffer = new byte[4096];
			int i = 1;
			while (i > 0)
			{
				i = file.Read(buffer, 0, 4096);
				if (i > 0)
				{
					json += System.Text.Encoding.Default.GetString(buffer, 0, i);
				}
				//Debug.Log(json);
			}
			if (json.Length == 0)
			{
				DataSave dataStruct1 = new DataSave();
				return dataStruct1;
			}




			//FromStringToKitClass().


			//	Debug.Log(json);


			SaveStruct myObject = JsonUtility.FromJson<SaveStruct>(json);

			DataSave dataStruct = new DataSave(myObject);
			dataStruct.loaded = true;
			currentPrefs.currentSlot = dataStruct.slot;

			file.Close();
			return dataStruct;
		}





	}
}