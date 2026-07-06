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
        public AudioClip titleBGM; // 타이틀 화면 BGM
        public AudioClip mainGameBGM; // 메인 게임 BGM
        public AudioClip miniGameABGM; // 미니게임A BGM
        public AudioClip miniGameBBGM; // 미니게임B BGM
        public AudioClip gameOverBGM; // GameOverBGM

        #endregion

        #region Title

        [Header("Title")]
        public AudioClip startButton; // 게임 시작 버튼 누를 시 버튼선택음

        #endregion

        #region Main Game

        [Header("Main Game")]
        public AudioClip beastGrowl; // 맹수 상태 Agitated, Danger 일때 턴이 바뀌며 나옴
        public AudioClip beastRoar; // 맹수가 탈출하여 Player를 공격, eventScript 등장하는 순간에 나옴
        public AudioClip disease; // player가 질병에 걸렸을때 가장 우선해서 나옴
        public AudioClip eat; // player가 음식을 먹을 때 나오는 소리
        public AudioClip drink; // player가 물을 마실 때 나오는 소리
        public AudioClip sleep; // player가 수면 시작할 때 나오는 소리
        public AudioClip purchase; // exchangeMachine에서 아이템을 구매했을 때 나는 소리
        public AudioClip uiMove; // 이벤트 스크립트 넘길 때 나오는 소리
        public AudioClip buttonClick; // 버튼 클릭 시 나오는 소리

        #endregion

        #region Mini Game A

        [Header("Mini Game A")]
        public AudioClip countDown; // 경주 시작 전 카운트 다운 소리
        public AudioClip raceStart; // 휘슬소리나 경적소리, 또는 총소리로 게임의 시작을 알리는 소리
        public AudioClip horseRun; // 말이 달리는 말발굽 소리(여러 말이 달리는 소리가 나야 함)
        public AudioClip audience; // 관중이 환호하는 소리
        public AudioClip raceSuccess; // 선택한 말이 rank가 1,2일때 나오는 소리
        public AudioClip raceFail; // 선택한 말이 rank가 3,4일 때 나오는 소리

        #endregion

        #region Mini Game B

        [Header("Mini Game B")]
        public AudioClip countDownB; // 게임 시작 전 카운트다운 소리(A랑 같아도 상관 없을지도?
        public AudioClip gameStart;
        public AudioClip order; // 주문이 팝업될 때 나오는 소리
        public AudioClip correct; // 네개 전부 정답일때 나오는 소리
        public AudioClip wrong; // 하나라도 틀려서 하트가 사라질 때 나오는 소리
        public AudioClip heartEmpty; // 하트가 전부 사라져 최종적으로 게임오버되는 순간에 나오는 소리

        #endregion

        #region Game Over

        [Header("Game Over")]
        public AudioClip returnToTitle; // enter로 타이틀 화면으로 돌아갈 때 나오는 소리

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
