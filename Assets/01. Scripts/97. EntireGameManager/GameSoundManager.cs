using UnityEngine;

namespace ObservedProject
{
    public class GameSoundManager : MonoBehaviour
    {
        public static GameSoundManager Instance;

        [Header("Audio Source")]
        public AudioSource bgmSource;
        public AudioSource sfxSource;

        #region BGM

        [Header("BGM")]
        public AudioClip titleBGM;
        public AudioClip mainGameBGM;
        public AudioClip miniGameABGM;
        public AudioClip miniGameBBGM;
        public AudioClip gameOverBGM;

        #endregion

        #region Title

        [Header("Title")]
        public AudioClip startButton;
        public AudioClip quitButton;

        #endregion

        #region Main Game

        [Header("Main Game")]
        public AudioClip beastGrowl;
        public AudioClip beastRoar;
        public AudioClip disease;
        public AudioClip eat;
        public AudioClip drink;
        public AudioClip sleep;
        public AudioClip purchase;
        public AudioClip interaction; // F 상호작용
        public AudioClip uiMove; // 이벤트 스크립트 넘기기
        public AudioClip buttonClick; // 버튼 클릭

        #endregion

        #region Mini Game A

        [Header("Mini Game A")]
        public AudioClip countDown;
        public AudioClip raceStart;
        public AudioClip horseRun;
        public AudioClip audience;
        public AudioClip raceSuccess;
        public AudioClip raceFail;

        #endregion

        #region Mini Game B

        [Header("Mini Game B")]
        public AudioClip countDownB;
        public AudioClip order;
        public AudioClip correct;
        public AudioClip wrong;
        public AudioClip heartEmpty;

        #endregion

        #region Game Over

        [Header("Game Over")]
        public AudioClip returnToTitle;

        #endregion

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayBGM(AudioClip clip)
        {
            if (bgmSource.clip == clip) return;
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        public void StopBGM()
        {
            bgmSource.Stop();
        }

        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;
            sfxSource.PlayOneShot(clip);
        }

        public void PlayLoop(AudioClip clip)
        {
            if (clip == null) return;
            sfxSource.clip = clip;
            sfxSource.loop = true;
            sfxSource.Play();
        }

        public void StopLoop()
        {
            sfxSource.Stop();
            sfxSource.loop = false;
        }
    }
}
