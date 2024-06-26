﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;
//
//public class OnSelectedEvent : UnityEvent<Checker> { };
//
//public class Checker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
//{
//    [HideInInspector] public OnSelectedEvent OnSelected = new OnSelectedEvent();
//    [HideInInspector] public UnityEvent OnDead = new UnityEvent();
//    [HideInInspector] public UnityEvent OnRespawn = new UnityEvent();
//    [HideInInspector] public UnityEvent OnMouseExitFromChecker = new UnityEvent();
//    [HideInInspector] public UnityEvent OnMouseEnterInChecker = new UnityEvent();
//
//    [SerializeField] GameObject arrow;
//
//    public void Init(TypeOfCheckers typeOfCheckers)
//    {
//        if (!rb)
//        {
//            rb = gameObject.AddComponent<Rigidbody>();
//            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
//            this.typeOfCheckers = typeOfCheckers;
//        }
//
//        ResetForces();
//    }
//
//    public void FreezeRotation()
//    {
//        rb.freezeRotation = true;
//    }
//
//    public void UnfreezeRotation()
//    {
//        rb.freezeRotation = false;
//    }
//
//    public TypeOfCheckers GetTypeOfCheckers()
//    {
//        return typeOfCheckers;
//    }
//
//    public Transform GetArrowTransform()
//    {
//        return arrow.transform;
//    }
//
//    public void SetMaterials(Material[] materials)
//    {
//        this.materials = materials;
//    }
//
//    public Material[] GetMaterials()
//    {
//        return materials;
//    }
//
//    public Vector3 GetVelocity()
//    {
//        return rb.GetPointVelocity(transform.position);
//    }
//
//    public void ResetForces()
//    {
//        rb.velocity = Vector3.zero;
//        rb.angularVelocity = Vector3.zero;
//
//        rb.isKinematic = true;
//        rb.isKinematic = false;
//
//        rb.useGravity = true;
//
//        rb.drag = 0.1f;
//        rb.angularDrag = 0;
//        rb.mass = 1;
//    }
//
//    public void DisableArrow()
//    {
//        arrow.SetActive(false);
//    }
//
//    public void EnableArrow()
//    {
//        arrow.SetActive(true);
//    }
//
//    public void Enable()
//    {
//        gameObject.SetActive(true);
//        OnRespawn.Invoke();
//    }
//
//    public void Disable()
//    {
//        ResetForces();        
//        gameObject.SetActive(false);        
//        OnDead.Invoke();
//    }
//
//    public void Move(Vector3 direction, float procent)
//    {
//        direction.Normalize();
//        Vector3 force = MAX_FORCE;
//        force.Scale(direction);
//        
//        force.x *= procent;        
//        force.z *= procent;
//
//        rb.AddForce(force, ForceMode.VelocityChange);
//    }
//
//    public void CheckerSelected()
//    {
//        FreezeRotation();
//        EnableArrow();
//    }
//
//    public void CheckerUnselected()
//    {
//        UnfreezeRotation();
//        DisableArrow();        
//    }
//
//    public void SetIsPlayerCanSelect(bool isCanSelect)
//    {
//        this.isCanSelect = isCanSelect;
//    } 
//
//    #region private
//
//    Vector3 MAX_FORCE = new Vector3(400, 0, 400);
//    const string DEAD_ZONE = "dead zone";
//    
//    Rigidbody rb;
//    Material[] materials;
//    bool isCanSelect = false;
//    TypeOfCheckers typeOfCheckers;
//    private void OnMouseDown()
//    {
//        if (isCanSelect)
//            OnSelected.Invoke(this);
//    }
//    private void OnMouseExit()
//    {
//        if (isCanSelect)
//            OnMouseExitFromChecker.Invoke();
//    }
//  
//    public void OnPointerDown()
//    {
//        if(isCanSelect)
//            OnSelected.Invoke(this);
//    }
//
//    public void IPointerExitHandler()
//    {
//        if(isCanSelect)
//            OnMouseExitFromChecker.Invoke();
//    }
//
//    private void OnMouseEnter()
//    {
//        if(isCanSelect)
//            OnMouseEnterInChecker.Invoke();
//    }
//
//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        if (isCanSelect)
//            OnMouseEnterInChecker.Invoke();
//    }
//
//    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
//    {
//        if (isCanSelect)
//            OnMouseExitFromChecker.Invoke();
//    }
//
//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (isCanSelect)
//            OnSelected.Invoke(this);
//    }
//
//    #endregion
//}
//
//public enum TypeOfCheckers
//{
//    WHITE,
//    BLACK
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnSelectedEvent : UnityEvent<Checker> { };

public class Checker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [HideInInspector] public OnSelectedEvent OnSelected = new OnSelectedEvent();
    [HideInInspector] public UnityEvent OnDead = new UnityEvent();
    [HideInInspector] public UnityEvent OnRespawn = new UnityEvent();
    [HideInInspector] public UnityEvent OnMouseExitFromChecker = new UnityEvent();
    [HideInInspector] public UnityEvent OnMouseEnterInChecker = new UnityEvent();

    [SerializeField] GameObject arrow;

    public void Init(TypeOfCheckers typeOfCheckers)
    {
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            this.typeOfCheckers = typeOfCheckers;
        }

        ResetForces();
    }

    public void FreezeRotation()
    {
        rb.freezeRotation = true;
    }

    public void UnfreezeRotation()
    {
        rb.freezeRotation = false;
    }

    public TypeOfCheckers GetTypeOfCheckers()
    {
        return typeOfCheckers;
    }

    public Transform GetArrowTransform()
    {
        return arrow.transform;
    }

    public void SetMaterials(Material[] materials)
    {
        this.materials = materials;
    }

    public Material[] GetMaterials()
    {
        return materials;
    }

    public Vector3 GetVelocity()
    {
        return rb.GetPointVelocity(transform.position);
    }

    public void ResetForces()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        rb.isKinematic = false;

        rb.useGravity = true;

        rb.drag = 0.1f;
        rb.angularDrag = 0;
        rb.mass = 1;
    }

    public void DisableArrow()
    {
        arrow.SetActive(false);
    }

    public void EnableArrow()
    {
        arrow.SetActive(true);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        OnRespawn.Invoke();
    }

    public void Disable()
    {
        ResetForces();
        gameObject.SetActive(false);
        OnDead.Invoke();
    }

    public void Move(Vector3 direction, float procent)
    {
        direction.Normalize();
        Vector3 force = MAX_FORCE;
        force.Scale(direction);

        force.x *= procent;
        force.z *= procent;

        rb.AddForce(force, ForceMode.VelocityChange);
    }

    public void CheckerSelected()
    {
        FreezeRotation();
        EnableArrow();
    }

    public void CheckerUnselected()
    {
        UnfreezeRotation();
        DisableArrow();
    }

    public void SetIsPlayerCanSelect(bool isCanSelect)
    {
        this.isCanSelect = isCanSelect;
    }

    #region private

    Vector3 MAX_FORCE = new Vector3(400, 0, 400);
    const string DEAD_ZONE = "dead zone";

    Rigidbody rb;
    Material[] materials;
    bool isCanSelect = false;
    TypeOfCheckers typeOfCheckers;
    private void OnMouseDown()
    {
        if (isCanSelect)
            OnSelected.Invoke(this);
    }
    private void OnMouseExit()
    {
        if (isCanSelect)
            OnMouseExitFromChecker.Invoke();
    }

    public void OnPointerDown()
    {
        if (isCanSelect)
            OnSelected.Invoke(this);
    }

    public void IPointerExitHandler()
    {
        if (isCanSelect)
            OnMouseExitFromChecker.Invoke();
    }

    private void OnMouseEnter()
    {
        if (isCanSelect)
            OnMouseEnterInChecker.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isCanSelect)
            OnMouseEnterInChecker.Invoke();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (isCanSelect)
            OnMouseExitFromChecker.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCanSelect)
            OnSelected.Invoke(this);
    }

    #endregion
}

public enum TypeOfCheckers
{
    WHITE,
    BLACK
}
