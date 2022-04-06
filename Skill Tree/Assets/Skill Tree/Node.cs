using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Node : MonoBehaviour
{
    [SerializeField] AudioSource soundEffect;
    [SerializeField] ParticleSystem visualEffect;
    [SerializeField] Image activeImage;
    [SerializeField] Image availableImage;

    [SerializeField] List<Node> children;

    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text abiltyText;

    [SerializeField] int cost;
    [SerializeField] Abilities linkedAbility;

    [SerializeField] UI playerExperience;
    [SerializeField] SkillTree tree;

    [SerializeField] Edge edgePrefab;

    int numParents = 0;

    public bool childrenPrimed = false;

    List<Edge> edges;

    public bool isAvailable;
    public bool isActive;
    
    // Start is called before the first frame update
    void Start()
    {
        if (visualEffect != null)
        {
            visualEffect.Pause();
        }
        costText.text = cost.ToString();
        abiltyText.text = linkedAbility.name;
        edges = new List<Edge>();
        foreach (Node child in children)
        {
            Edge edge = Instantiate(edgePrefab, tree.transform);
            edge.SetNodes(this, child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrimeParent()
    {
        numParents++;
    }

    public void MakeAvailable()
    {
        numParents--;
        isAvailable = numParents <= 0;
        if (isAvailable)
        {
            availableImage.gameObject.SetActive(true);
        }
    }

    public int GetCost()
    {
        return cost;
    }

    public List<Node> GetChildren()
    {
        return children;
    }

    public Abilities GetLinkedAbility()
    {
        return linkedAbility;
    }

    public void Activate()
    {
        if (isAvailable && !isActive && playerExperience.CanAfford(cost))
        {
            isActive = true;
            linkedAbility.isActive = true;
            if (soundEffect != null)
            {
                soundEffect.Play();
            }
            if (visualEffect != null)
            {
                visualEffect.Play();
            }
            activeImage.gameObject.SetActive(true);
            foreach (Node child in children)
            {
                child.MakeAvailable();
            }
            playerExperience.DeductCost(cost);
        }
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
