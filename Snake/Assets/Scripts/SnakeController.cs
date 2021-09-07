using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField]
    ObjectPool pool;
    [SerializeField]
    UI uI;
    [SerializeField]
    private GameObject snakeHead;
    [SerializeField]
    private Transform snakeBase;
    [SerializeField]
    private GameObject cubePrefab;
    [SerializeField]
    private float distanceBetweenCubes;
    [SerializeField]
    private float bodyCount = 2;
    [SerializeField]
    float leftRightSpeed = 0.1f;
    [SerializeField]
    float snakeSpeed = 1;
    float snakeSpeedForPause;
    [SerializeField]
    float feverSnakeSpeed = 3;

    [SerializeField]
    GameObject cam;
    Vector3 camPos;
    private Vector3 mousePos;

    private List<Transform> snakeCubes = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();
    [SerializeField]
    private Colors colorsComponent;
    [SerializeField]
    private Eating eatingComponent;

    private Material snakeColor;
    private int snakeColorIndex;

    private bool isFever = false;

    private float[] prevBoxSize = new float[2];
    ObjectPooling<ObjForPool> bombsPool;
    public bool gameStart = false;


    float halfRoadWidth = 3.5f;

    private void Start()
    {
        bombsPool = pool.GetBombsPool();
        positions.Add(snakeHead.transform.position);
        snakeHead.GetComponent<MeshRenderer>().material = colorsComponent.GetActualColor();
        snakeColorIndex = colorsComponent.GetColorIndexes()[0];
        camPos = cam.transform.position;
        prevBoxSize = eatingComponent.GetBoxSize();
        for (int i = 0; i < bodyCount; i++)
        {
            AddElement();
        }
    }
    void Update()
    {
        TailMoving();

        if (!isFever && gameStart)
        {
            Touch[] touchesArray = Input.touches;
            foreach (Touch touch in touchesArray)
            {
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    float targetPosX = Camera.main.ScreenToViewportPoint(touch.position).x;
                    snakeHead.transform.position = Vector3.Lerp(new Vector3(targetPosX * halfRoadWidth * 2, 0, snakeHead.transform.position.z), snakeHead.transform.position, targetPosX * 0.1f);
                }
            }
        }

        if (isFever)
        {
            Vector3 targetPos = new Vector3(halfRoadWidth, 0, snakeHead.transform.position.z);
            float distance = Vector3.Distance(snakeHead.transform.position, targetPos);
            snakeHead.transform.position = Vector3.Lerp(transform.position, targetPos, leftRightSpeed);
        }
    }

    private void TailMoving()
    {
        float distance = ((Vector3)snakeHead.transform.position - positions[0]).magnitude;

        if (distance > distanceBetweenCubes)
        {
            Vector3 direction = ((Vector3)snakeHead.transform.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * distanceBetweenCubes);
            positions.RemoveAt(positions.Count - 1);

            distance -= distanceBetweenCubes;
        }

        for (int i = 0; i < snakeCubes.Count; i++)
        {
            snakeCubes[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / distanceBetweenCubes);
        }
    }

    private void FixedUpdate()
    {
        snakeHead.transform.Translate(snakeHead.transform.forward * snakeSpeed * Time.deltaTime);

        camPos = new Vector3(4.5f, 10, snakeHead.transform.position.z - 7);

        cam.transform.position = camPos;
    }

    public void AddElement()
    {
        if (positions.Count < 8)
        {
            Vector3 pos = positions[positions.Count - 1];
            GameObject res = Instantiate(cubePrefab, new Vector3(pos.x, pos.y, pos.z - distanceBetweenCubes), Quaternion.identity, snakeBase);
            res.GetComponent<MeshRenderer>().material = snakeHead.GetComponent<MeshRenderer>().material;
            snakeCubes.Add(res.transform);
            positions.Add(res.transform.position);
        }
    }

    public IEnumerator StartFever()
    {
        isFever = true;
        eatingComponent.SetBoxSize(5f, 10f);
        Time.timeScale = feverSnakeSpeed;
        yield return new WaitForSeconds(5f);
        isFever = false;
        Time.timeScale = 1f;
        eatingComponent.SetBoxSize(prevBoxSize[0], prevBoxSize[1]);
    }

    public bool GetIsFever()
    {
        return isFever;
    }

    public int GetSnakeColor()
    {
        return snakeColorIndex;
    }

    public void SetSnakeColor(int index)
    {
        snakeColorIndex = index;
    }

    public void StopGame()
    {
        gameStart = false;
        snakeSpeedForPause = snakeSpeed;
        snakeSpeed = 0;
    }

    public void StartGame()
    {
        gameStart = true;
        snakeSpeed = snakeSpeedForPause;
    }

    public void EndGame(bool deathByBomb)
    {
        gameStart = false;
        uI.EndGame(deathByBomb);
    }
}
