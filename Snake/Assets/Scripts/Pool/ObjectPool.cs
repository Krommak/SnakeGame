using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int bombsCount, gemsCount, humansCount, platformCount;

    [SerializeField] private ObjForPool bombsPrefab, gemsPrefab, humansPrefab, platformPrefab;

    private ObjectPooling<ObjForPool> bombsPool, gemsPool, humansPool, platformPool;

    private void Awake()
    {
        bombsPool = new ObjectPooling<ObjForPool>(bombsPrefab, bombsCount);
        gemsPool = new ObjectPooling<ObjForPool>(gemsPrefab, gemsCount);
        humansPool = new ObjectPooling<ObjForPool>(humansPrefab, humansCount);
        platformPool = new ObjectPooling<ObjForPool>(platformPrefab, platformCount);
    }

    public ObjectPooling<ObjForPool> GetBombsPool()
    {
        return bombsPool;
    }

    public ObjectPooling<ObjForPool> GetGemsPool()
    {
        return gemsPool;
    }

    public ObjectPooling<ObjForPool> GetHumansPool()
    {
        return humansPool;
    }

    public ObjectPooling<ObjForPool> GetPlatformPool()
    {
        return platformPool;
    }
}
