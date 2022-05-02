using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject target;
    [SerializeField] GameObject elbowTarget;
    [SerializeField] Transform leftShoulder;
    [SerializeField] Transform leftFoot;
    [SerializeField] Transform rightFoot;
    [SerializeField] float lhDistanceModifier;
    [SerializeField] float legDistanceModifier;
    [SerializeField] Vector3 leftHandRotationOffset;
    [Range(0f, 1f)]
    [SerializeField] float weightSlider;
    [Range(0f, 1f)]
    [SerializeField] float elbowWeightSlider;
    [Range(0f, 1f)]
    [SerializeField] float lhWeightSlider;
    [Range(0f, 1f)]
    [SerializeField] float footWeightSlider;
    [Range(0f, 1f)]
    [SerializeField] float footOffset;
    RaycastHit hit;

    List<RaycastHit> hits;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        hits = new List<RaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        hits.Clear();
        anim.SetIKPosition(AvatarIKGoal.RightHand, target.transform.position);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, weightSlider);
        //anim.SetIKRotation(AvatarIKGoal.RightHand, target.transform.rotation);
        //anim.SetIKRotationWeight(AvatarIKGoal.RightHand, weightSlider);
        anim.SetIKHintPosition(AvatarIKHint.RightElbow, elbowTarget.transform.position);
        anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, elbowWeightSlider);

        if (Physics.Raycast(leftShoulder.position, -transform.right, out hit, lhDistanceModifier))
        {
            hits.Add(hit);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, hit.point);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, lhWeightSlider);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Euler(hit.normal + leftHandRotationOffset));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        }
        if (Physics.Raycast(leftFoot.position + leftFoot.transform.right, -transform.up, out hit, legDistanceModifier))
        {
            hits.Add(hit);
            anim.SetIKPosition(AvatarIKGoal.LeftFoot, hit.point + Vector3.up * footOffset);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, footWeightSlider);
            Vector3 rotationAngle = Vector3.Cross(Vector3.up, hit.normal);
            float angle = Vector3.Angle(Vector3.up, hit.normal);
            Quaternion newRot = Quaternion.AngleAxis(angle, rotationAngle);
            anim.SetIKRotation(AvatarIKGoal.LeftFoot, newRot * anim.GetIKRotation(AvatarIKGoal.LeftFoot));
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        }
        if (Physics.Raycast(rightFoot.position + rightFoot.transform.right, -transform.up, out hit, legDistanceModifier))
        {
            hits.Add(hit);
            anim.SetIKPosition(AvatarIKGoal.RightFoot, hit.point + Vector3.up * footOffset);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, footWeightSlider);
            Vector3 rotationAngle = Vector3.Cross(Vector3.up, hit.normal);
            float angle = Vector3.Angle(Vector3.up, hit.normal);
            Quaternion newRot = Quaternion.AngleAxis(angle, rotationAngle);
            anim.SetIKRotation(AvatarIKGoal.RightFoot, newRot * anim.GetIKRotation(AvatarIKGoal.RightFoot));
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        foreach (RaycastHit h in hits)
        {
            Gizmos.DrawWireSphere(h.point, 0.1f);
        }
    }
}
