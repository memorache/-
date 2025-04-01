using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;      // 调用命名空间

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;        // 获取Cinemachine引用类型变量
    public CinemachineImpulseSource impulseSource;      // 获取组件中的CinemachineImpulseSource
    public VoidEventSO cameraShakeEvent;        // 对广播事件VoidEventSO进行监听
    public VoidEventSO afterSceneLoadedEvent;       // 对广播事件VoidEventSO进行监听
    private void Awake()
    {
        // 初始化confiner2D对象，用于后续改变bound
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        // 注册OnCameraShakeEvent事件
        // 为什么你在这里正常了在UImanager中就不能用啊啊啊啊啊啊啊
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
        // 注册afterSceneLoadedEvent事件
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        // 注销其它事件
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }
    private void OnAfterSceneLoadedEvent()      // 使用监听获取每个场景中的边界
    {
        GetNewCameraBound();
    }
    private void GetNewCameraBound()        // 用于改变约束图层的函数
    {
        // 采用FindGameObjectWithTag让标签为bound的图层赋值给变量obj
        var obj = GameObject.FindGameObjectWithTag("bound");
        // 如果没有图层就结束函数
        if (obj == null)
        {
            return;
        }
        var collider = obj.GetComponent<Collider2D>();
        // 将bound图层具有Collider2D的物体传递给BoundingShape2D
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        // 清除先前bound图层的缓存用于切换场景时更换边界
        confiner2D.InvalidateCache();
    }
    private void OnCameraShakeEvent()
    {
        // 事件订阅成功后开始执行震动
        impulseSource.GenerateImpulse();
    }
}
