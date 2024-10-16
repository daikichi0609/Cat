using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using DG.Tweening;
using System.Threading.Tasks;
using TsugamerLibrary;

public class FadeManager : Singleton<FadeManager>
{
    /// <summary>
    /// 黒画面
    /// </summary>
    [SerializeField]
    private Image m_BlackScreen;

    /// <summary>
    /// 白画面
    /// </summary>
    [SerializeField]
    private Image m_WhiteScreen;

    // フェイド速度
    private static readonly float FADE_SPEED = 1f;

    /// <summary>
    /// 明転
    /// </summary>
    /// <param name="whileEvent"></param>
    /// <returns></returns>
    public async Task StartFade<T>(T arg, Action<T> whileEvent)
    {
        await FadeOutScreen(m_BlackScreen);
        whileEvent?.Invoke(arg);
        await FadeInScreen(m_BlackScreen);
    }

    /// <summary>
    /// 暗転
    /// </summary>
    /// <param name="whileEvent"></param>
    /// <returns></returns>
    public async Task TurnBright()
    {
        await FadeInScreen(m_BlackScreen);
    }

    /// <summary>
    /// ホワイトアウト
    /// </summary>
    /// <returns></returns>
    public async Task StartFadeWhite<T>(T arg, Action<T> whileEvent)
    {
        await FadeOutScreen(m_WhiteScreen);
        whileEvent?.Invoke(arg);
        await FadeInScreen(m_WhiteScreen);
    }

    /// <summary>
    /// スクリーン暗転
    /// </summary>
    private Task FadeOutScreen(Image screen, float speed = 1f) => screen.DOFade(1f, speed).AsyncWaitForCompletion();

    /// <summary>
    /// スクリーン明転
    /// </summary>
    private Task FadeInScreen(Image screen, float speed = 1f) => screen.DOFade(0f, speed).AsyncWaitForCompletion();
}
