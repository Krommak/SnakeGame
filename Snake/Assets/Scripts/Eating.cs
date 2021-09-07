using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    [SerializeField]
    private Vector3 sphereCenter;
    [SerializeField]
    private float forwardBoxScale = 1.5f;
    [SerializeField]
    private float sideBoxScale = 1.5f;
    [SerializeField]
    private float viewingAngle = 45;

    [SerializeField]
    private SnakeController snakeController;
    [SerializeField]
    private Score scoreComponent;

    private int eatenHumansCount;

    private void Update()
    {
        if (snakeController.gameStart)
            Eat();
        if (eatenHumansCount > 5)
        {
            eatenHumansCount = 0;
            snakeController.AddElement();
        }
    }

    private void Eat()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(sideBoxScale, 0, forwardBoxScale), transform.rotation);
        foreach (var hitCollider in hitColliders)
        {
            Vector3 dir = hitCollider.transform.position - transform.position;

            float angle = Vector3.Angle(dir, Vector3.forward);
            if (snakeController.GetIsFever() && angle < viewingAngle)
            {
                if (hitCollider.gameObject.tag == "Human" || hitCollider.gameObject.tag == "Gem" || hitCollider.gameObject.tag == "Bomb")
                {
                    hitCollider.GetComponent<Collider>().enabled = false;
                    FoodAttraction foodAttraction = hitCollider.GetComponent<FoodAttraction>();
                    if (foodAttraction == null)
                    {
                        foodAttraction = hitCollider.GetComponentInParent<FoodAttraction>();
                    }
                    hitCollider.GetComponent<FoodAttraction>().StartAttraction(transform.position, forwardBoxScale / 3);
                    scoreComponent.AddHumanScore();
                    eatenHumansCount++;
                }
            }
            else
            {
                if (hitCollider.gameObject.tag == "Human" && angle < viewingAngle || hitCollider.gameObject.tag == "Gem" && angle < viewingAngle)
                {
                    hitCollider.GetComponent<FoodAttraction>().StartAttraction(transform.position, forwardBoxScale);
                    hitCollider.GetComponent<Collider>().enabled = false;
                    if (hitCollider.gameObject.tag == "Human")
                    {
                        if (hitCollider.GetComponent<Human>().GetColor() == snakeController.GetSnakeColor())
                        {
                            scoreComponent.AddHumanScore();
                            eatenHumansCount++;
                        }
                        else
                        {
                            if (snakeController.GetIsFever())
                            {
                                scoreComponent.AddHumanScore();
                            }
                            else
                            {
                                snakeController.EndGame(false);
                            }
                        }
                    }
                    if (hitCollider.gameObject.tag == "Gem" && !snakeController.GetIsFever())
                    {
                        bool isFever = scoreComponent.AddGemsScore();
                        if (isFever)
                        {
                            StartCoroutine(snakeController.StartFever());
                        }
                    }
                }
            }
        }
    }

    public float[] GetBoxSize()
    {
        float[] boxSize = new float[2] { forwardBoxScale, sideBoxScale };
        return boxSize;
    }

    public void SetBoxSize(float forwardScale, float sideScale)
    {
        sideBoxScale = sideScale;
        forwardBoxScale = forwardScale;
    }
}
