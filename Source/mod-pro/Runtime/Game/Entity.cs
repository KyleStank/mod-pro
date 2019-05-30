using System;

using UnityEngine;

using MoonSharp.Interpreter;

namespace ModPro.Runtime.Game
{
    /// <summary>
    /// Base class that controls parts of a GameObject that can be easily replaced through mods.
    /// </summary>
    public class Entity : MonoBehaviour
    {
        /// <summary>
        /// Contains data that a Entity can use.
        /// </summary>
        [SerializeField]
        private EntityData m_EntityData = null;

        [SerializeField]
        private SpriteRenderer[] m_ModdableSprites = new SpriteRenderer[0];

        #region Properties

        public EntityData Data
        {
            get
            {
                return m_EntityData;
            }
        }

        /// <summary>
        /// Returns the list of Sprites that are moddable.
        /// </summary>
        public SpriteRenderer[] ModdableSprites
        {
            get
            {
                return m_ModdableSprites;
            }
        }

        #endregion

        #region Unity Methods
        
        private void OnEnable()
        {
            // Randomly generate new ID.
            Guid guid = Guid.NewGuid();
        }

        #endregion

        #region Public Methods

        public void SetNewSprite(int index, Sprite sprite)
        {
            for(int i = 0; i < m_ModdableSprites.Length; i++)
            {
                if(index == i)
                {
                    if(m_ModdableSprites[i] == null || sprite == null)
                    {
                        continue;
                    }

                    m_ModdableSprites[i].sprite = sprite;
                }
            }
        }

        #endregion
    }
}
