//
// Kino/Motion - Motion blur effect
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

// Suppress "assigned but never used" warning
#pragma warning disable 414

// Show fancy graphs
#define SHOW_GRAPHS

// Show advanced options (not useful in most cases)
// #define ADVANCED_OPTIONS

using UnityEngine;
using UnityEditor;

namespace Kino
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Motion))]
    public class MotionEditor : Editor
    {
        MotionGraphDrawer _graph;

        SerializedProperty _shutterAngle;
        SerializedProperty _sampleCount;
        SerializedProperty _maxBlurRadius;
        SerializedProperty _frameBlending;
        SerializedProperty _debugMode;

        [SerializeField] Texture2D _blendingIcon;

        static GUIContent _textStrength = new GUIContent("Strength");

        void OnEnable()
        {
            _shutterAngle = serializedObject.FindProperty("_shutterAngle");
            _sampleCount = serializedObject.FindProperty("_sampleCount");
            _maxBlurRadius = serializedObject.FindProperty("_maxBlurRadius");
            _frameBlending = serializedObject.FindProperty("_frameBlending");
            _debugMode = serializedObject.FindProperty("_debugMode");
        }

        public override void OnInspectorGUI()
        {
            if (_graph == null) _graph = new MotionGraphDrawer(_blendingIcon);

            serializedObject.Update();

            EditorGUILayout.LabelField("Shutter Speed Simulation", EditorStyles.boldLabel);

            #if SHOW_GRAPHS
            _graph.DrawShutterGraph(_shutterAngle.floatValue);
            #endif

            EditorGUILayout.PropertyField(_shutterAngle);
            EditorGUILayout.PropertyField(_sampleCount);

            #if ADVANCED_OPTIONS
            EditorGUILayout.PropertyField(_maxBlurRadius);
            #endif

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Multi Frame Blending", EditorStyles.boldLabel);

            #if SHOW_GRAPHS
            _graph.DrawBlendingGraph(_frameBlending.floatValue);
            #endif

            EditorGUILayout.PropertyField(_frameBlending, _textStrength);

            #if ADVANCED_OPTIONS
            EditorGUILayout.PropertyField(_debugMode);
            #endif

            serializedObject.ApplyModifiedProperties();
        }
    }
}
