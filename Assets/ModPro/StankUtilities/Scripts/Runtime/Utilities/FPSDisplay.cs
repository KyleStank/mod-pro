using UnityEngine;

namespace StankUtilities.Runtime.Utilities
{
    /// <summary>
    /// Displays the game's FPS.
    /// </summary>
    public class FPSDisplay : MonoBehaviour
    {
        private float m_DeltaTime = 0.0f;

        #region Unity Methods

        private void Update()
        {
            m_DeltaTime += (Time.unscaledDeltaTime - m_DeltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            // Get the width and height of the screen.
            int width = Screen.width;
            int height = Screen.height;

            // Create a new GUI style.
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = height * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

            // Create a new rect using the screen's width and height.
            Rect rect = new Rect(0, 0, width, height * 2 / 100);

            // Calculate the FPS.
            float miliSecs = m_DeltaTime * 1000.0f;
            float fps = 1.0f / m_DeltaTime;

            // Create a string for the FPS.
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", miliSecs, fps);

            // Display the FPS.
            GUI.Label(rect, text, style);
        }

        #endregion
    }
}