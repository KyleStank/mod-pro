using System.IO;
using System.IO.Compression;
using System.Reflection;

using UnityEngine;

using ModPro.Runtime.Game;
using ModPro.Runtime.Utilities;

namespace ModPro.Runtime.Modding
{
    /// <summary>
    /// Loads a mod's texture to this game object.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class ModLoader : MonoBehaviour
    {
        [SerializeField]
        private string m_TextureName = "";

        private SpriteRenderer m_SpriteRenderer = null;
        private Sprite m_DefaultSprite = null;

        private Mod m_Mod = null;

        #region Unity Methods

        private void Awake()
        {
            // Get required components.
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            // Set the default Sprite.
            m_DefaultSprite = m_SpriteRenderer.sprite;
        }

        private void Start()
        {
            // TODO: Rewrite the ModLoader.
            //// Try to find the mod.
            //m_Mod = ModManager.Instance.GetActiveMod();
            //if(m_Mod == null)
            //{
            //    Debug.LogError("Mod couldn't be found.");
            //    return;
            //}

            //// Activate mod!
            //ModManager.Instance.ActivateMod(m_Mod);

            //Texture2D text = ResourceUtility.LoadTextureFromZIP(m_Mod.FilePath, "Calvin.png");

            //System.Type type = m_Mod.GetType();
            //PropertyInfo[] modProps = type.GetProperties();

            //using (FileStream file = File.OpenRead(m_Mod.FilePath))
            //using(ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
            //{
            //    for(int i = 0; i < modProps.Length; i++)
            //    {
            //        if(modProps[i].Name.ToLower() == m_TextureName.ToLower())
            //        {
            //            foreach (ZipArchiveEntry entry in zip.Entries)
            //            {
            //                using (Stream stream = entry.Open())
            //                {
            //                    if(m_Mod.PlayerTexture == entry.Name && m_Mod.PlayerTexture == modProps[i].GetValue(m_Mod).ToString())
            //                    {
            //                        m_SpriteRenderer.sprite = ResourceUtility.LoadNewSpriteFromZIP(m_Mod.FilePath, entry.Name, 64.0f);
            //                    }
            //                    else if(m_Mod.WallTexture == entry.Name && m_Mod.WallTexture == modProps[i].GetValue(m_Mod).ToString())
            //                    {
            //                        m_SpriteRenderer.sprite = ResourceUtility.LoadNewSpriteFromZIP(m_Mod.FilePath, entry.Name, 64.0f);
            //                    }
            //                    else if(m_Mod.TargetTexture == entry.Name && m_Mod.TargetTexture == modProps[i].GetValue(m_Mod).ToString())
            //                    {
            //                        m_SpriteRenderer.sprite = ResourceUtility.LoadNewSpriteFromZIP(m_Mod.FilePath, entry.Name, 64.0f);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        #endregion
    }
}
