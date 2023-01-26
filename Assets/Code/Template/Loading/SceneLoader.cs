using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace alicewithalex
{
    public static class SceneLoader
    {
        private const float UNITY_ASYNC_OPERATION_THRESHOLD = 0.9f;

        private static CancellationTokenSource _tokenSource;

        public static async void LoadScene(int buildIndex, Action onComplete = null)
        {
            if (_tokenSource is not null)
            {
                _tokenSource.Cancel();
                _tokenSource = null;
            }

            _tokenSource = new CancellationTokenSource();

            try
            {
                await LoadSceneInternal(buildIndex, onComplete);
            }
            catch (OperationCanceledException exception)
            {
#if UNITY_EDITOR
                Debug.Log("Loading was canceled.");
#endif
            }
            finally
            {
                _tokenSource.Dispose();
                _tokenSource = null;
            }
        }

        private static async Task LoadSceneInternal(int buildIndex, Action onComplete)
        {
            _tokenSource.Token.ThrowIfCancellationRequested();
            if (_tokenSource.Token.IsCancellationRequested)
                return;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
            asyncOperation.allowSceneActivation = false;
            while (true)
            {
                _tokenSource.Token.ThrowIfCancellationRequested();
                if (_tokenSource.Token.IsCancellationRequested)
                    return;

                if (asyncOperation.progress >= UNITY_ASYNC_OPERATION_THRESHOLD)
                    break;

                await Task.Yield();
            }

            asyncOperation.allowSceneActivation = true;

            //Extra safe loop to be sured that scene if fully loaded
            while (true)
            {
                if (asyncOperation.isDone)
                    break;

                await Task.Yield();
            }

            onComplete?.Invoke();
        }
    }
}