using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class ScanManager : MonoBehaviour, IInputClickHandler
{
    public TextMesh InstructionTextMesh;
    public GameObject PlaceablePrefab;

    // Use this for initialization
    void Start () {
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        SpatialUnderstanding.Instance.RequestBeginScanning();
        SpatialUnderstanding.Instance.ScanStateChanged += ScanStateChanged;
    }

    private void ScanStateChanged()
    {
        if (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Scanning)
        {
            LogSurfaceState();
        }
        else if (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Done)
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

    // Update is called once per frame
    void Update () {
        switch (SpatialUnderstanding.Instance.ScanState)
        {
            case SpatialUnderstanding.ScanStates.None:
                break;
            case SpatialUnderstanding.ScanStates.ReadyToScan:
                break;
            case SpatialUnderstanding.ScanStates.Scanning:
                //InstructionTextMesh.text = "Scanning in progress.\nWhen ready, tap anywhere to finish scan.";
                break;
            case SpatialUnderstanding.ScanStates.Finishing:
                //this.InstructionTextMesh.text = "State: Finishing Scan";
                break;
            case SpatialUnderstanding.ScanStates.Done:
                //InstructionTextMesh.text = "State: Scan Finished\nTap a sphere to place box and blocks test";
                break;
            default:
                break;
        }
    }

    private void LogSurfaceState()
    {
        IntPtr statsPtr = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStatsPtr();
        if (SpatialUnderstandingDll.Imports.QueryPlayspaceStats(statsPtr) != 0)
        {
            var stats = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticPlayspaceStats();
            this.InstructionTextMesh.text = string.Format("TotalSurfaceArea: {0:0.##}\nWallSurfaceArea: {1:0.##}\nHorizSurfaceArea: {2:0.##}\nWhen ready, tap anywhere to finish scan.", stats.TotalSurfaceArea, stats.WallSurfaceArea, stats.HorizSurfaceArea);
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (SpatialUnderstanding.Instance.ScanState != SpatialUnderstanding.ScanStates.Done)
        {
            this.InstructionTextMesh.text = "Requested Finish Scan";

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

        // Process found results.
        for (int i = 0; i < shapeCount; i++)
        {
            // Create a Beacon at each "Placeable" location.
            Instantiate(PlaceablePrefab, resultsShape[i].position, Quaternion.identity);
        }
    }
}
