using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 touchStart;
    private new Camera camera;
    [SerializeField] private float minZoom = 1f;
    [SerializeField] private float maxZoom = 8f;
    [SerializeField] private float zoomSize = 3f;
    [SerializeField] private float zoomSpeed = 10f;
    private float targetZoom;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        Vector3 direction;
        Touch touchZero;
        Touch touchOne;
        Vector2 touchZeroPrewPos;
        Vector2 touchOnePrewPos;
        float prevMagnitude;
        float currentMagnitude;
        float difference;

        //for computers
        if (Input.GetMouseButtonDown(0))
        {

            touchStart = camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0))
        {
            direction = touchStart - camera.ScreenToWorldPoint(Input.mousePosition);
            camera.transform.position += direction;
        }
        //for phones
        if (Input.touchCount == 2)
        {
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);
            touchZeroPrewPos = touchZero.position - touchZero.deltaPosition;
            touchOnePrewPos = touchOne.position - touchOne.deltaPosition;
            prevMagnitude = (touchZeroPrewPos - touchOnePrewPos).magnitude;
            currentMagnitude = (touchZero.position - touchOne.position).magnitude;
            difference = currentMagnitude - prevMagnitude;
            zoom(difference * 0.01f);
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void zoom(float increment)
    {
        targetZoom -= increment * zoomSize;
        targetZoom = Mathf.Clamp(camera.orthographicSize - increment, minZoom, maxZoom);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
        zoomEffect(camera.orthographicSize, targetZoom, zoomSpeed);
    }
    IEnumerator zoomEffect(float source, float target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            camera.orthographicSize = Mathf.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        camera.orthographicSize = target;
    }
}
























































/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    private new Camera camera;
    private float targetZoom;
    [SerializeField] private float zoomSize = 3f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;

    private void Awake()
    {
        camera = Camera.main;
        targetZoom = camera.orthographicSize;
    }

    void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollData * zoomSize;
        targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }
}
*/