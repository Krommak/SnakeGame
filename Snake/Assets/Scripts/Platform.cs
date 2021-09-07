using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    public Transform nextPos;
    [SerializeField]
    GameObject checkPoint;
    GameObject humanPrefab, bombPrefab, gemsPrefab;

    [SerializeField]
    Transform[] dotsForHumans, dotsForGemsAndBombs;

    List<Human> humansList = new List<Human>();
    List<ObjForPool> bombsAndGemsList = new List<ObjForPool>();

    public void GenerateHumans(Material color, int actColorIndex, Material subColor, int subColorIndex, ObjectPooling<ObjForPool> humansPool)
    {
        for (int i = 0; i < dotsForHumans.Length; i += 2)
        {
            int randomI = Random.Range(0, 2);
            int randomColor = Random.Range(0, 2);
            for (int j = 0; j < 2; j++)
            {
                int randomCount = Random.Range(3, 6);
                Vector3 pos = new Vector3(dotsForHumans[i + j].position.x, 0, dotsForHumans[i + j].position.z + Random.Range(-1f, 1f));
                for (int k = 0; k < randomCount; k++)
                {
                    ObjForPool res = humansPool.GetFreeElement();
                    Vector3 personalPosition = new Vector3(pos.x + Random.Range(-1f, 1f), 0, pos.z + Random.Range(-1f, 1f));
                    if (randomColor == j)
                    {
                        res.GetComponent<Human>().ActivateObject(personalPosition, color, actColorIndex, this.gameObject.transform);
                    }
                    else
                    {
                        res.GetComponent<Human>().ActivateObject(personalPosition, subColor, subColorIndex, this.gameObject.transform);
                    }
                    humansList.Add(res.GetComponent<Human>());
                }
            }
        }
    }

    public void GenerateBombsAndGems(ObjectPooling<ObjForPool> bombsPool, ObjectPooling<ObjForPool> gemsPool)
    {
        ObjectPooling<ObjForPool> [] pool = new ObjectPooling<ObjForPool>[2] { bombsPool, gemsPool};
        int firstIndex = Random.Range(0, 2);

        for (int i = 0; i < dotsForGemsAndBombs.Length; i++)
        {   
            ObjForPool res;
            if(firstIndex == 0 && i%2 == 0 || firstIndex == 1 && i%2 == 1)
            {
                res = bombsPool.GetFreeElement();
            }
            else 
            {
                res = gemsPool.GetFreeElement();
            }

            bombsAndGemsList.Add(res);
            res.ActivateObject(dotsForGemsAndBombs[i].position, this.transform);
        }
    }

    public void ActivateChengeColorCheckPoint(Material newActualColor, int colorIndex)
    {
        checkPoint.SetActive(true);
        checkPoint.GetComponent<MeshRenderer>().material = newActualColor;
        checkPoint.GetComponentInChildren<ChengeSnakeColor>().SetColorIndex(colorIndex);
    }

    public void DeactivatePlatform()
    {
        foreach (Human obj in humansList)
        {
            if (this.transform == obj.transform.parent)
                obj.DeactivateObject();
        }
        humansList.Clear();
        foreach (ObjForPool obj in bombsAndGemsList)
        {
            if (this.transform == obj.transform.parent)
                obj.DeactivateObject();
        }
        bombsAndGemsList.Clear();
        this.gameObject.SetActive(false);
        this.checkPoint.SetActive(false);
    }
}
