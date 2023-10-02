using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameSystem
{
    public class GameManager : MigalhaSystem.Singleton<GameManager>
    {

        protected override void Awake()
        {
            base.Awake();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                RestartScene();
            }
        }

        [ContextMenu("Restart Scene")]
        public void RestartScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}