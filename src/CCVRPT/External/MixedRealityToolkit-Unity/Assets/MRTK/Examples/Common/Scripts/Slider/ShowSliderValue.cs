//
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    [AddComponentMenu("Scripts/MRTK/Examples/ShowSliderValue")]
    public class ShowSliderValue : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro textMesh = null;

        public int Max = 1; // Added for CCVRPT

        public void OnSliderUpdated(SliderEventData eventData)
        {
            if (textMesh == null)
            {
                textMesh = GetComponent<TextMeshPro>();
            }

            if (textMesh != null)
            {
                // textMesh.text = $"{eventData.NewValue:F2}";
                textMesh.text = $"{( Mathf.Floor(eventData.NewValue*Max)):F0}";
            }
        }
    }
}
