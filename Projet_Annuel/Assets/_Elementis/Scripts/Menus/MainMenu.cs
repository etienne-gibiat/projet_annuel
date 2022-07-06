using PGSauce.Core.SceneManagement;
using UnityEngine;

namespace _Elementis.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public SceneReference gameScene;
        
        public void LoadGameScene()
        {
            Cursor.visible = false;
            bl_SceneLoader.GetActiveLoader().LoadLevel(gameScene.SceneName);
        }
    }
}