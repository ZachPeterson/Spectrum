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
using System.IO;

namespace SpaceWars
{
    class AssetList
    {
        //Dynamically loads Textures into memory
        public ContentManager aaContent;
        public string[] mAssetNames;
        public Texture2D[] mAssets;

        public AssetList(ContentManager pContent)
        {
            aaContent = pContent;
        }

        public bool Unload()
        {
            mAssets = null;
            return true;
        }

        public bool LoadAssetsIntoMemory(List<string> pFileList)
        {
            //Set asset Array Length to Length of File List
            mAssets = new Texture2D[pFileList.Count];
            mAssetNames = new string[pFileList.Count];
            //Load all Assets into System Memory
            int cIndex = 0;
            foreach (string fName in pFileList)
            {
                mAssetNames[cIndex] = fName;
                mAssets[cIndex] = aaContent.Load<Texture2D>(fName);
                cIndex++;
            }

            return true;
        }

        public Texture2D getAsset(string pFileName)
        {
            if (Array.IndexOf(mAssetNames, pFileName) < mAssets.Length)
            {
                return mAssets[Array.IndexOf(mAssetNames, pFileName)];
            }
            else
            {
                return null;
            }
        }
    }
}
