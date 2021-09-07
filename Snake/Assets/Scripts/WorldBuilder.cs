using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject platformPrefab;
    [SerializeField]
    ObjectPool pool;

    private Material actualColor;

    private int actColorIndex;
    private Material subColor;
    private int subColorIndex;
    [SerializeField]
    Platform startPlatform;

    Vector3 startPlatformBuildPos;

    Queue<GameObject> activePlatforms = new Queue<GameObject>();

    int platformCount = 0;
    private ObjectPooling<ObjForPool> bombsPool;
    private ObjectPooling<ObjForPool> gemsPool;
    private ObjectPooling<ObjForPool> humansPool;
    private ObjectPooling<ObjForPool> platformPool;

    private Colors colorsComponent;

    private void Awake()
    {
        colorsComponent = GetComponent<Colors>();
    }

    private void Start()
    {
        CheckColors();
        bombsPool = pool.GetBombsPool();
        gemsPool = pool.GetGemsPool();
        humansPool = pool.GetHumansPool();
        platformPool = pool.GetPlatformPool();
        startPlatformBuildPos = startPlatform.nextPos.position;
        for (int i = 0; i < 4; i++)
        {
            CreatePlatform();
        }
    }

    public void CreatePlatform()
    {
        ObjForPool res = platformPool.GetFreeElement();
        activePlatforms.Enqueue(res.gameObject);
        res.GetComponent<ObjForPool>().ActivateObject(startPlatformBuildPos);
        startPlatformBuildPos = res.GetComponent<Platform>().nextPos.position;
        platformCount++;
        if (activePlatforms.Count > 20)
        {
            activePlatforms.Peek().GetComponent<Platform>().DeactivatePlatform();
            activePlatforms.Dequeue();
        }
        if (platformCount > 0 && platformCount <= 2)
        {
            res.GetComponent<Platform>().GenerateHumans(actualColor, actColorIndex, subColor, subColorIndex, humansPool);
        }
        else if (platformCount > 2 && platformCount <= 4)
        {
            res.GetComponent<Platform>().GenerateBombsAndGems(bombsPool, gemsPool);
        }
        else if (platformCount == 5)
        {
            CheckColors();
            List<int> colorIndexes = colorsComponent.GetColorIndexes();
            actColorIndex = colorIndexes[0];
            subColorIndex = colorIndexes[1];
            res.GetComponent<Platform>().ActivateChengeColorCheckPoint(actualColor, actColorIndex);
            platformCount = 0;
        }
    }

    private void CheckColors()
    {
        List<Material> newColors = colorsComponent.SetActualColor();
        actualColor = newColors[0];
        subColor = newColors[1];
        List<int> colorIndexes = colorsComponent.GetColorIndexes();
        actColorIndex = colorIndexes[0];
        subColorIndex = colorIndexes[1];
    }
}
