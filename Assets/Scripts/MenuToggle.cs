using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuToggle : MonoBehaviour
{
    [SerializeField] InputActionReference menuToggleReference;
    [SerializeField] GameObject menuObject;

    void Awake()
    {
        menuToggleReference.action.performed += OnToggle;
    }

    void OnEnable() { menuToggleReference.action.Enable(); }
    void OnDisable() { menuToggleReference.action.Disable(); }

    void OnToggle(InputAction.CallbackContext context)
    {
        menuObject.SetActive(!menuObject.activeSelf);
    }
}
