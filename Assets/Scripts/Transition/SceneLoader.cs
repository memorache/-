using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;      // �������ط���

public class SceneLoader : MonoBehaviour
{
    public GameSceneSO firstScene;      // ѡ����صĵ�һ������
    public Vector3 firstPlayerTrans;    // ��һ�������������λ��
    private GameSceneSO currentLoadedScene;     // ��ǰ���صĳ���
    // ������ʱ�������ڴ��泡���е���ʱ����
    private GameSceneSO tempScene;
    private Vector3 tempPosition;
    private bool tempFade;
    public float fadeDuration;           // ���뽥����ʱ��
    public Transform playerTrans;        // ת���������������λ��
    private bool isLoading;              // �Ƿ����ڼ��س���
    [Header("���뵭���¼��㲥")]
    public FadeEventSO fadeEvent;        // ���뽥���¼��㲥
    [Header("�¼�����")]
    public SceneLoadEventSO loadEventSO;    // �������¼�
    [Header("�������غ��¼�")]
    public VoidEventSO afterSceneLoadedEvent;      // ����������ɺ���¼�
    private void Awake()
    {
        // �÷����򳡾���һ�μ���ʱ���ᴥ��LoadNewScene()������������ʱ����
        // ʹ��AddressabelAssets���첽���س����ķ���
        // ��һ������Ϊ��Ҫ���صĳ������ڶ�������Ϊ���ط�ʽ
        // ���ط�ʽ�е�singleΪĬ�ϼ��ط�ʽ������Ϊ���ظó���ʱж��������ĳ���
        // ��һ��AdditiveΪ�ڸó������ڵĻ������������һ������
        // ���ﲻʹ�øü��ط�������Ϊ������صĳ�����currentLoadedScene = firstScene;���ݺ�
        // firstScene�������˵���currentLoadedSceneû�б�����
        // Addressables.LoadSceneAsync(firstScene.sceneReference, LoadSceneMode.Additive);
        // ����ʹ������ķ������ȸ����ٽ��м���
        // ȷ�ϵ�ǰ����
        // currentLoadedScene = firstScene;
        // ���ص�ǰ����
        // currentLoadedScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
    private void Start()
    {
        // ��ʼ��Ϸʱִ��NewGame()����
        NewGame();
    }
    private void OnEnable()
    {
        // �����¼�����
        loadEventSO.LoadRequestScene += OnLoadRequestScene;
    }
    private void OnDisable()
    {
        // ע���¼�����
        loadEventSO.LoadRequestScene -= OnLoadRequestScene;
    }
    private void NewGame()      // ��Ϸ��ʼʱִ�еķ���
    {
        // �õ�ǰ���صĳ������ڵ�һ������
        tempScene = firstScene;
        // ���ݵ�һ����������ҿ�ʼ��λ�ã��Լ��Ƿ���
        OnLoadRequestScene(tempScene, firstPlayerTrans, true);
    }
    private void OnLoadRequestScene(GameSceneSO sceneToGo, Vector3 positionToGo, bool fadeScreen)       // �������������¼�
    {
        // ʹ��isLoading��ֹ��ҷ���������͵㵼�³���Ƶ���л�
        if (isLoading)
        {
            return;
        }
        // ȷ�����ڼ��س���
        isLoading = true;
        // ��Ҫ��ȥ��ǰһ�������ټ����³���������������ʹ�ñ���ȥ�洢��ǰ����ʱ����
        tempScene = sceneToGo;
        tempPosition = positionToGo;
        tempFade = fadeScreen;
        // ȷ��currentLoadedScene��Ϊ��ʱ��ʹ��Я�̺���ж�س���
        if (currentLoadedScene != null)
        {
            StartCoroutine(UnLoadPreviousScene());
        }
        // ���Ϊ�գ�֤���ǵ�һ�μ��س�����ֱ�Ӽ����³���
        else
        {
            LoadNewScene();
        }
    }
    private IEnumerator UnLoadPreviousScene()       // ʹ��Я��ж��ǰһ�������������³���
    {
        if (tempFade)
        {
            // TODO:ʵ�ֽ���
            fadeEvent.FadeIn(fadeDuration);
        }
        // �ڵȴ����뽥����ʱ�����ж��ǰһ������
        yield return new WaitForSeconds(fadeDuration);
        // ʹ��AddressabelAssets���첽ж�س����ķ���
        // ж�س�������һ��д��������ʹ��Addressablesǰ׺����ж�س���
        // ʹ��Я��ȷ������ж����ɺ��ټ����³���
        yield return currentLoadedScene.sceneReference.UnLoadScene();
        // �����³���
        LoadNewScene();
    }
    private void LoadNewScene()         // �����³���
    {
        // �ڼ��س���ʱ���������
        playerTrans.gameObject.SetActive(false);
        // ���ش��ݹ������³���������ֵ��loadingOption
        var loadingOption = tempScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        // �첽������ɺ󴥷������¼�
        // CompletedΪ������ɺ���¼�,destroyedΪ������
        loadingOption.Completed += OnLoadingCompleted;
    }
    private void OnLoadingCompleted(AsyncOperationHandle<SceneInstance> handle)     // �������������¼�
    {
        // �����صĳ�����ֵ��currentLoadedScene
        currentLoadedScene = tempScene;
        // �����ﴫ�͵�ָ��λ��
        playerTrans.position = tempPosition;
        // ִ�н���
        if (tempFade)
        {
            // TODO:����
            fadeEvent.FadeOut(fadeDuration);
        }
        // ȷ�ϼ������
        isLoading = false;
        // ������ɺ���ʾ���
        playerTrans.gameObject.SetActive(true);
        // ��������������ɺ���¼�
        afterSceneLoadedEvent.RaiseEvent();
    }
}
