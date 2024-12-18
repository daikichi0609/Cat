﻿namespace TsugamerLibrary
{
    using System;
    using System.Runtime.CompilerServices;

    ///---------------------------------------------------------------------------------
    /// <summary>
    /// C# シングルトンベースクラス (MonoBehaviourに非依存)
    /// </summary>
    ///---------------------------------------------------------------------------------
    public abstract class SingletonCS<T> : IDisposable
        where T : class, new()
    {
        ///---------------------------------------------------------------------------------
        /// <summary>
        /// シングルトンのインスタンス
        /// </summary>
        ///---------------------------------------------------------------------------------
        private static T ms_Instance = new T();

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        ///
        /// <returns>
        /// シングルトンのインスタンス <br>
        /// </returns>
        ///---------------------------------------------------------------------------------
        public static T CreateInstance()
        {
            return ms_Instance ?? (ms_Instance = new T());
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        ///
        /// <returns>
        /// シングルトンのインスタンス <br>
        /// </returns>
        ///---------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetInstance()
        {
            return ms_Instance;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// インスタンスを再生成する
        /// </summary>
        /// <returns> *以前の* インスタンス </returns>
        ///---------------------------------------------------------------------------------
        public static T RegenerateInstance()
        {
            var old = ms_Instance;
            ms_Instance = new T();
            return old;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// 確保しているインスタンスを開放する
        /// </summary>
        /// <returns> いままで格納していたインスタンス </returns>
        ///---------------------------------------------------------------------------------
        public static T ReleaseInstance()
        {
            var old = ms_Instance;
            ms_Instance = null;
            return old;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// 終了
        /// </summary>
        ///---------------------------------------------------------------------------------
        public void Dispose()
        {
            OnDispose();
            if (this == ms_Instance)
                ms_Instance = null;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// 終了コールバック
        /// </summary>
        ///---------------------------------------------------------------------------------
        protected abstract void OnDispose();
    }

    ///---------------------------------------------------------------------------------
    /// <summary>
    /// C# シングルトンベースクラス (MonoBehaviourに非依存)
    /// </summary>
    /// <typeparam name="T">継承先の型</typeparam>
    /// <typeparam name="TBase">抽象クラスとして取得する型</typeparam>
    ///---------------------------------------------------------------------------------
    public abstract class SingletonCS<T, TBase> : IDisposable
        where T : class, TBase, new()
    {
        ///---------------------------------------------------------------------------------
        /// <summary>
        /// シングルトンのインスタンス
        /// </summary>
        ///---------------------------------------------------------------------------------
        private static T ms_Instance = new T();

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// インスタンスの生成
        /// </summary>
        ///
        /// <returns>
        /// シングルトンのインスタンス <br>
        /// </returns>
        ///---------------------------------------------------------------------------------
        public static T CreateInstance()
        {
            return ms_Instance ?? (ms_Instance = new T());
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        ///
        /// <returns>
        /// シングルトンのインスタンス <br>
        /// </returns>
        ///---------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetInstance()
        {
            return ms_Instance;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        ///
        /// <returns>
        /// シングルトンのインスタンス <br>
        /// </returns>
        ///---------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TBase GetBase()
        {
            return ms_Instance;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// インスタンスを再生成する
        /// </summary>
        /// <returns> *以前の* インスタンス </returns>
        ///---------------------------------------------------------------------------------
        public static T RegenerateInstance()
        {
            var old = ms_Instance;
            ms_Instance = new T();
            return old;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// 確保しているインスタンスを開放する
        /// </summary>
        /// <returns> いままで格納していたインスタンス </returns>
        ///---------------------------------------------------------------------------------
        public static T ReleaseInstance()
        {
            var old = ms_Instance;
            ms_Instance = null;
            return old;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// 終了
        /// </summary>
        ///---------------------------------------------------------------------------------
        public void Dispose()
        {
            OnDispose();
            if (this == ms_Instance)
                ms_Instance = null;
        }

        ///---------------------------------------------------------------------------------
        /// <summary>
        /// 終了コールバック
        /// </summary>
        ///---------------------------------------------------------------------------------
        protected abstract void OnDispose();
    }
}