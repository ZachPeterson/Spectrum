using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;
using System.IO;

namespace SpaceWars
{
    [Serializable]
    public struct ScoreData
    {
        public string[] mName;
        public int[] mScore;
        public int[] mWave;

        public int Size;

        public ScoreData(int size)
        {
            mName = new string[size];
            mScore = new int[size];
            mWave = new int[size];

            Size = size;
        }
    }

    [Serializable]
    public struct AbilityData
    {
        public int[] mAbility1;
        public int[] mAbility2;
        public int[] mAbility3;
        public int[] mAbility4;
        public int[] mUnlocks;

        public int Size;

        public AbilityData(int size)
        {
            mAbility1 = new int[4] { -1, -1, -1, 0 };
            mAbility2 = new int[4] { -2, -2, -2, 1 };
            mAbility3 = new int[4] { -3, -3, -3, 2 };
            mAbility4 = new int[4] { -4, -4, -4, 3 };
            mUnlocks = new int[9] {0,0,0,0,0,0,0,0,0};

            Size = size;
        }
    }

    public class GameIO
    {
        public ScoreData Scoredata;
        public AbilityData Abilitydata;
        public string dataDirectory = "Spectrum/Data";
        public string fileName = "scores.dat";

        public GameIO()
        {
            Scoredata = new ScoreData(5);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName))
            {
                SaveScores();
            }
            else
            {
                LoadScores();
            }

            dataDirectory = "Spectrum/Data";
            fileName = "abilities.dat";
            Abilitydata = new AbilityData(4);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName))
            {
                SaveAbilities();
            }
            else
            {
                LoadAbilities();
            }
        }

        public void SaveScores()
        {
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName);
            FileStream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName, FileMode.OpenOrCreate);

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(ScoreData));
                s.Serialize(stream, Scoredata);
            }
            finally
            {
                stream.Close();
            }
        }

        public void SaveAbilities()
        {
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName);
            FileStream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName, FileMode.OpenOrCreate);

            try
            {
                XmlSerializer s = new XmlSerializer(typeof(AbilityData));
                s.Serialize(stream, Abilitydata);
            }
            finally
            {
                stream.Close();
            }
        }

        public void LoadScores()
        {
            FileStream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(ScoreData));
                Scoredata = (ScoreData)s.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        public void LoadAbilities()
        {
            FileStream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dataDirectory + "/" + fileName, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(AbilityData));
                Abilitydata = (AbilityData)s.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        public void AddScore(string pName, int pScore, int pWave)
        {
            for (int i = 0; i < Scoredata.Size; i++)
            {
                if (pScore > Scoredata.mScore[i])
                {
                    for (int j = Scoredata.Size - 1; j > i; j--)
                    {
                        Scoredata.mName[j] = Scoredata.mName[j - 1];
                        Scoredata.mScore[j] = Scoredata.mScore[j - 1];
                        Scoredata.mWave[j] = Scoredata.mWave[j - 1];
                    }
                    Scoredata.mName[i] = pName;
                    Scoredata.mScore[i] = pScore;
                    Scoredata.mWave[i] = pWave;
                    break;
                }
            }
        }
    }
}
