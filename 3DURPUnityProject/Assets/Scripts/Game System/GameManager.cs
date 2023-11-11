using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.GameSystem
{
    public class GameManager : MigalhaSystem.Singleton<GameManager>
    {
        bool m_isPaused;
        [SerializeField] GameObject m_pauseCanvas;
        [SerializeField] GameObject m_uiCanvas;
        [SerializeField] GameObject m_deathCanvas;

        PlayerLifeSystem m_playerLifeSystem;
        
        public bool m_IsPaused => m_isPaused;
        protected override void Awake()
        {
            base.Awake();
            m_isPaused = false;

            DeathCanvas(false);
            UICanvas(true);
            PauseCanvas(false);
        }

        private void Start()
        {
            m_playerLifeSystem = PlayerManager.Instance.m_PlayerLifeSystem;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                RestartScene();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_isPaused = !m_isPaused;
            }



            if (m_playerLifeSystem.m_alive)
            {
                if (m_isPaused)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0;
                    DeathCanvas(false);
                    UICanvas(false);
                    PauseCanvas(true);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    DeathCanvas(false);
                    UICanvas(true);
                    PauseCanvas(false);
                    Time.timeScale = 1;
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                DeathCanvas(true);
                UICanvas(false);
                PauseCanvas(false);
            }
        }

        [ContextMenu("Restart Scene")]
        public void RestartScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        public void Unpause()
        {
            m_isPaused = false;
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void DeathCanvas(bool active)
        {
            m_deathCanvas.SetActive(active);
        }

        public void PauseCanvas(bool active)
        {
            m_pauseCanvas.SetActive(active);
        }

        public void UICanvas(bool active)
        {
            m_uiCanvas.SetActive(active);
        }
    }
}