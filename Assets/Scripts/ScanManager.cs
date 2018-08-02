using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class ScanManager : MonoBehaviour, IInputClickHandler
{
    public GameObject PlaceablePrefab;

    // Use this for initialization
    void Start () {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        SpatialUnderstanding.Instance.RequestBeginScanning();
        SpatialUnderstanding.Instance.ScanStateChanged += ScanStateChanged;
    }

    private void ScanStateChanged()
    {
        if (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Done)
        {
            CreateShapes();
            InstantiateObjectOnPlaceable();
        }
    }

    private void OnDestroy()
    {
        SpatialUnderstanding.Instance.ScanStateChanged -= ScanStateChanged;
    }

    public void CreateShapes()
    {
        // Create definitions and analyze
        CreateCustomShapeDefinitions();
        SpatialUnderstandingDllShapes.ActivateShapeAnalysis();
    }

    private void CreateCustomShapeDefinitions()
    {
        if (!SpatialUnderstanding.Instance.AllowSpatialUnderstanding)
        {
            return;
        }

        List<SpatialUnderstandingDllShapes.ShapeComponent> shapeComponents;
        List<SpatialUnderstandingDllShapes.ShapeConstraint> shapeConstraints;

        //add any shape components and constraints here and use AddShape to name it and add it to the list of shapes

        //placeable
        shapeComponents = new List<SpatialUnderstandingDllShapes.ShapeComponent>()
        {
            new SpatialUnderstandingDllShapes.ShapeComponent(
                new List<SpatialUnderstandingDllShapes.ShapeComponentConstraint>()
                {
                    SpatialUnderstandingDllShapes.ShapeComponentConstraint.Create_SurfaceHeight_Between(0.3f, 1.6f),
                    SpatialUnderstandingDllShapes.ShapeComponentConstraint.Create_SurfaceCount_Min(1),
                    SpatialUnderstandingDllShapes.ShapeComponentConstraint.Create_SurfaceArea_Min(0.1f),
                }),
        };

        IntPtr shapeComponentsPtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(shapeComponents.ToArray());
        SpatialUnderstandingDllShapes.AddShape("Placeable", shapeComponents.Count, shapeComponentsPtr, 0, IntPtr.Zero);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (SpatialUnderstanding.Instance.ScanState != SpatialUnderstanding.ScanStates.Done)
        {
            SpatialUnderstanding.Instance.RequestFinishScan();
        }
    }

    public void InstantiateObjectOnPlaceable()
    {
        const int QueryResultMaxCount = 512;
        SpatialUnderstandingDllShapes.ShapeResult[] resultsShape = new SpatialUnderstandingDllShapes.ShapeResult[QueryResultMaxCount];

        // Pin managed object memory going to native code
        IntPtr resultsShapePtr = SpatialUnderstanding.Instance.UnderstandingDLL.PinObject(resultsShape);

        // Find the half dimensions of "Sittable" objects via the DLL
        int shapeCount = SpatialUnderstandingDllShapes.QueryShape_FindShapeHalfDims("Placeable", resultsShape.Length, resultsShapePtr);

        for (int i = 0; i < shapeCount; i++)
        {
            Instantiate(PlaceablePrefab, resultsShape[i].position, Quaternion.identity);
        }
    }
}
