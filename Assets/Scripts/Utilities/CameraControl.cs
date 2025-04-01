using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;      // ���������ռ�

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;        // ��ȡCinemachine�������ͱ���
    public CinemachineImpulseSource impulseSource;      // ��ȡ����е�CinemachineImpulseSource
    public VoidEventSO cameraShakeEvent;        // �Թ㲥�¼�VoidEventSO���м���
    public VoidEventSO afterSceneLoadedEvent;       // �Թ㲥�¼�VoidEventSO���м���
    private void Awake()
    {
        // ��ʼ��confiner2D�������ں����ı�bound
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        // ע��OnCameraShakeEvent�¼�
        // Ϊʲô����������������UImanager�оͲ����ð�������������
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
        // ע��afterSceneLoadedEvent�¼�
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        // ע�������¼�
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }
    private void OnAfterSceneLoadedEvent()      // ʹ�ü�����ȡÿ�������еı߽�
    {
        GetNewCameraBound();
    }
    private void GetNewCameraBound()        // ���ڸı�Լ��ͼ��ĺ���
    {
        // ����FindGameObjectWithTag�ñ�ǩΪbound��ͼ�㸳ֵ������obj
        var obj = GameObject.FindGameObjectWithTag("bound");
        // ���û��ͼ��ͽ�������
        if (obj == null)
        {
            return;
        }
        var collider = obj.GetComponent<Collider2D>();
        // ��boundͼ�����Collider2D�����崫�ݸ�BoundingShape2D
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        // �����ǰboundͼ��Ļ��������л�����ʱ�����߽�
        confiner2D.InvalidateCache();
    }
    private void OnCameraShakeEvent()
    {
        // �¼����ĳɹ���ʼִ����
        impulseSource.GenerateImpulse();
    }
}
