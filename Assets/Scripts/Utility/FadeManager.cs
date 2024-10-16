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
    /// �����
    /// </summary>
    [SerializeField]
    private Image m_BlackScreen;

    /// <summary>
    /// �����
    /// </summary>
    [SerializeField]
    private Image m_WhiteScreen;

    // �t�F�C�h���x
    private static readonly float FADE_SPEED = 1f;

    /// <summary>
    /// ���]
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
    /// �Ó]
    /// </summary>
    /// <param name="whileEvent"></param>
    /// <returns></returns>
    public async Task TurnBright()
    {
        await FadeInScreen(m_BlackScreen);
    }

    /// <summary>
    /// �z���C�g�A�E�g
    /// </summary>
    /// <returns></returns>
    public async Task StartFadeWhite<T>(T arg, Action<T> whileEvent)
    {
        await FadeOutScreen(m_WhiteScreen);
        whileEvent?.Invoke(arg);
        await FadeInScreen(m_WhiteScreen);
    }

    /// <summary>
    /// �X�N���[���Ó]
    /// </summary>
    private Task FadeOutScreen(Image screen, float speed = 1f) => screen.DOFade(1f, speed).AsyncWaitForCompletion();

    /// <summary>
    /// �X�N���[�����]
    /// </summary>
    private Task FadeInScreen(Image screen, float speed = 1f) => screen.DOFade(0f, speed).AsyncWaitForCompletion();
}
