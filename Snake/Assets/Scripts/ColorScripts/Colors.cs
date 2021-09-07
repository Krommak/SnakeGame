using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    [SerializeField]
    Material [] colors;
    Material actualColor;
    private int actColorIndex;
    Material prevActualColor;
    private int prevColorIndex;
    private Material subColor;
    private int subColorIndex;

    public List<Material> SetActualColor()
    {
        List<Material> newColors = new List<Material>();
        actColorIndex = Random.Range(0, colors.Length);
        subColorIndex = Random.Range(0, colors.Length);
        newColors.Add(colors[actColorIndex]);
        newColors.Add(colors[subColorIndex]);
        if(newColors[0] == newColors[1] || newColors[0] == prevActualColor)
        {
            newColors = SetActualColor();
        }
        
        prevActualColor = newColors[0];
        actualColor = newColors[0];
        return newColors;
    }

    public List<int> GetColorIndexes()
    {
        List<int> colorIndexes = new List<int>();
        colorIndexes.Add(actColorIndex);
        colorIndexes.Add(subColorIndex);

        return colorIndexes;
    }

    public Material GetActualColor()
    {
        return actualColor;
    }
}
