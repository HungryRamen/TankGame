using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform shakeCamera; //흔들을 카메라
    private Vector3 vOriginPos;   //shake전 시작 위치

    private bool bEnd;            //탄막 중복 처리및 무적 처리
    public bool IsEnd { get { return bEnd; } set { bEnd = value; } }
    // Use this for initialization
    void Start()
    {
        vOriginPos = shakeCamera.localPosition;
        bEnd = false;
    }

    public IEnumerator ShakeCamera(float duration = 0.05f, float magnitudePos = 0.03f)
    {
        float passTime = 0.0f;
        while (passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere * magnitudePos + vOriginPos; //랜덤값 * 흔들리는 정도 + 시작 위치
            shakeCamera.localPosition = shakePos;

            passTime += Time.deltaTime;
            yield return null;
        }
        bEnd = false;
        shakeCamera.localPosition = vOriginPos;  //끝나면 원래 위치로
        yield return null;
    }
}
