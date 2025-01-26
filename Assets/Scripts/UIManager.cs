 
    using System.Collections.Generic;
    using AYellowpaper.SerializedCollections;
    using Enums; 
    using UnityEngine;

    public class UIManager:Singleton<UIManager>
    {
        public SerializedDictionary<PageType, GameObject> pages;

        void Start()
        {
            OpenMainMenu();
        }

        public void ChangePage(PageType pageType)
        {
            foreach (var page in pages)
            { 
                page.Value.SetActive(false);
            }
            pages[pageType].SetActive(true);
            
        }

        public void OpenMainMenu()
        {
            ChangePage(PageType.MainScreen);
            SoundManager.Instance.PlayMainMenuMusic();
            SoundManager.Instance.SetMusicVolume(1f);
            GameManager.Instance.GameState = GameState.MainMenu;

        }

        public void OpenCredits()
        {
            ChangePage(PageType.Credits);
        }

        public void OpenGame()
        {
            ChangePage(PageType.Game);
            SoundManager.Instance.PlayGameMusic();
            SoundManager.Instance.SetMusicVolume(0.5f);
            GameManager.Instance.GameState = GameState.Playing;
        }

        public void OpenWinPage()
        {
            ChangePage(PageType.GameWin);
        }

        public void OpenLosePage()
        {
            ChangePage(PageType.GameOver);
        }

        public void Mute()
        { 
            SoundManager.Instance.Mute();
        }
        public void UnMute()
        { 
            SoundManager.Instance.UnMute();
        }

        protected override void Awake()
        {
            base.Awake(); 
        }
    }