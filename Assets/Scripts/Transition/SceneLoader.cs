using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;      // 场景加载方法

public class SceneLoader : MonoBehaviour
{
    public GameSceneSO firstScene;      // 选择加载的第一个场景
    public Vector3 firstPlayerTrans;    // 第一个场景中人物的位置
    private GameSceneSO currentLoadedScene;     // 当前加载的场景
    // 三个临时变量用于储存场景中的临时参数
    private GameSceneSO tempScene;
    private Vector3 tempPosition;
    private bool tempFade;
    public float fadeDuration;           // 渐入渐出的时间
    public Transform playerTrans;        // 转换场景后传送人物的位置
    private bool isLoading;              // 是否正在加载场景
    [Header("淡入淡出事件广播")]
    public FadeEventSO fadeEvent;        // 渐入渐出事件广播
    [Header("事件监听")]
    public SceneLoadEventSO loadEventSO;    // 监听该事件
    [Header("场景加载后事件")]
    public VoidEventSO afterSceneLoadedEvent;      // 场景加载完成后的事件
    private void Awake()
    {
        // 该方法因场景第一次加载时不会触发LoadNewScene()方法，所以暂时废弃
        // 使用AddressabelAssets中异步加载场景的方法
        // 第一个变量为需要加载的场景，第二个变量为加载方式
        // 加载方式中的single为默认加载方式，作用为加载该场景时卸载其它活动的场景
        // 另一个Additive为在该场景存在的基础上再添加另一个场景
        // 这里不使用该加载方法，因为这里加载的场景被currentLoadedScene = firstScene;传递后
        // firstScene被加载了但是currentLoadedScene没有被加载
        // Addressables.LoadSceneAsync(firstScene.sceneReference, LoadSceneMode.Additive);
        // 所以使用下面的方法，先复制再进行加载
        // 确认当前场景
        // currentLoadedScene = firstScene;
        // 加载当前场景
        // currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
    private void Start()
    {
        // 开始游戏时执行NewGame()方法
        NewGame();
    }
    private void OnEnable()
    {
        // 创建事件监听
        loadEventSO.LoadRequestScene += OnLoadRequestScene;
    }
    private void OnDisable()
    {
        // 注销事件监听
        loadEventSO.LoadRequestScene -= OnLoadRequestScene;
    }
    private void NewGame()      // 游戏开始时执行的方法
    {
        // 让当前加载的场景等于第一个场景
        tempScene = firstScene;
        // 传递第一个场景，玩家开始的位置，以及是否渐入
        OnLoadRequestScene(tempScene, firstPlayerTrans, true);
    }
    private void OnLoadRequestScene(GameSceneSO sceneToGo, Vector3 positionToGo, bool fadeScreen)       // 场景加载请求事件
    {
        // 使用isLoading防止玩家反复点击传送点导致场景频繁切换
        if (isLoading)
        {
            return;
        }
        // 确认正在加载场景
        isLoading = true;
        // 需要先去除前一个场景再加载新场景，所以这里先使用变量去存储当前的临时参数
        tempScene = sceneToGo;
        tempPosition = positionToGo;
        tempFade = fadeScreen;
        // 确定currentLoadedScene不为空时，使用携程函数卸载场景
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        // 如果为空，证明是第一次加载场景，直接加载新场景
        else
        {
            LoadNewScene();
        }
    }
    private IEnumerator UnLoadPreviousScene()       // 使用携程卸载前一个场景并加载新场景
    {
        if (tempFade)
        {
            // TODO:实现渐入
            fadeEvent.FadeIn(fadeDuration);
        }
        // 在等待渐入渐出的时间后，再卸载前一个场景
        yield return new WaitForSeconds(fadeDuration);
        // 使用AddressabelAssets中异步卸载场景的方法
        // 卸载场景的另一个写法，不用使用Addressables前缀即可卸载场景
        // 使用携程确保场景卸载完成后再加载新场景
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        // 加载新场景
        LoadNewScene();
    }
    private void LoadNewScene()         // 加载新场景
    {
        // 在加载场景时将玩家隐藏
        playerTrans.gameObject.SetActive(false);
        // 加载传递过来的新场景，并赋值给loadingOption
        var loadingOption = tempScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        // 异步加载完成后触发后续事件
        // Completed为加载完成后的事件,destroyed为（？）
        loadingOption.Completed += OnLoadingCompleted;
    }
    private void OnLoadingCompleted(AsyncOperationHandle<SceneInstance> handle)     // 场景加载完后的事件
    {
        // 将加载的场景赋值给currentLoadedScene
        currentLoadedScene = tempScene;
        // 将人物传送到指定位置
        playerTrans.position = tempPosition;
        // 执行渐出
        if (tempFade)
        {
            // TODO:渐出
            fadeEvent.FadeOut(fadeDuration);
        }
        // 确认加载完成
        isLoading = false;
        // 加载完成后显示玩家
        playerTrans.gameObject.SetActive(true);
        // 触发场景加载完成后的事件
        afterSceneLoadedEvent.RaiseEvent();
    }
}
