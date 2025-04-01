using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // ����Unity��UI��

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;       // Ѫ����ɫͼ��
    public Image healthDelayImage;      // �ӳ�Ѫ����ɫͼ��
    public Image powerImage;        // ��������ɫͼ��
    private void Update()
    {
        // �ú�ɫѪ��������ɫѪ�����٣�ʵ�ֽ����Ч��
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }
    public void OnHealthChange(float persentage)        // ��Ѫ���ٷֱȴ��ݵ�Ѫ����
    {
        // ��persentage��ֵ���ø�fillAmount
        healthImage.fillAmount = persentage;
    }
}
