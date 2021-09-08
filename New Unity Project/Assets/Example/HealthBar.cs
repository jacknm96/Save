using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] CanvasGroup group;
    static int count;
    public int myCount;
    float playerHealth = 100f;
    float maxHealth = 100f;
    Coroutine[] coroutines = new Coroutine[2];
    Coroutine visibility;
    int i = 0;
    Color startColor;
    Color hurtColor = Color.blue;
    Color poisonColor = Color.green;
    [SerializeField] AnimationCurve curve;
    
    // Start is called before the first frame update
    void Start()
    {
        myCount = count;
        count++;
        startColor = healthBar.color;
        coroutines[0] = StartCoroutine(PoisonDamage());
        //coroutines[1] = StartCoroutine(PoisonDamage());
    }

    [ContextMenu("Stop Poison")]
    public void StopPoison()
    {
        if (i < 2)
        {
            StopCoroutine(coroutines[i]);
            i++;
        }
    }

    // Update is called once per frame
    float HealthRatio()
    {
        return playerHealth / maxHealth;
    }

    IEnumerator PoisonDamage()
    {
        healthBar.color = poisonColor;
        float startTime = Time.time;
        int i = 0;
        while (Time.time - startTime < 10f)
        {
            if (i % 5 == 0)
            {
                healthBar.color = Color.Lerp(startColor, poisonColor, curve.Evaluate((Time.time - startTime) / 10));
                playerHealth -= 6 * Time.deltaTime;
                healthBar.fillAmount = HealthRatio();
            }
            i++;
            yield return null;
        }
        healthBar.color = startColor;
        visibility = StartCoroutine(FadeCanvas());
    }

    IEnumerator FadeCanvas()
    {
        yield return new WaitForSeconds(3);
        float startTime = Time.time;
        while (Time.time - startTime < 2f)
        {
            group.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / 2);
            yield return null;
        }
        group.alpha = 0;
    }

    [ContextMenu("Hit")]
    public void TakeDamage()
    {
        StopCoroutine(visibility);
        group.alpha = 1;
    }
}
