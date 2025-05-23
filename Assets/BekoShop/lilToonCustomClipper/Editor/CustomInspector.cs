#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace lilToon
{
    public class lilCustomClipperInspector : lilToonInspector
    {
        MaterialProperty _ClipEdgeColor;
        MaterialProperty _ClipEdgeColor2;
        MaterialProperty _ClipEdgeColor3;
        MaterialProperty _ClipEdgeColor4;
        MaterialProperty _ClipEdgeWidth;
        MaterialProperty _ClipEdgeBlur;
        MaterialProperty _ClipEdgeBlendMode;
        MaterialProperty _ClipShapePositionX;
        MaterialProperty _ClipShapePositionY;
        MaterialProperty _ClipShapeScaleX;
        MaterialProperty _ClipShapeScaleY;
        MaterialProperty _ClipShapeRotation;
        MaterialProperty _ClipShapes;
        MaterialProperty _Clip_N_Shape;
        MaterialProperty _Asteroid_Shape;
        MaterialProperty _Clip2;
        MaterialProperty _Clip3;
        MaterialProperty _Clip4;
        private static bool isShowCustomProperties;
        private const string shaderName = "lilCustomClipper";

        protected override void LoadCustomProperties(MaterialProperty[] props, Material material)
        {
            isCustomShader = true;

            // If you want to change rendering modes in the editor, specify the shader here
            ReplaceToCustomShaders();
            isShowRenderMode = !material.shader.name.Contains("Optional");

            // If not, set isShowRenderMode to false
            //isShowRenderMode = false;

            //LoadCustomLanguage("");
            _ClipEdgeColor = FindProperty("_ClipEdgeColor", props);
            _ClipEdgeColor2 = FindProperty("_ClipEdgeColor2", props);
            _ClipEdgeColor3 = FindProperty("_ClipEdgeColor3", props);
            _ClipEdgeColor4 = FindProperty("_ClipEdgeColor4", props);
            _ClipEdgeWidth = FindProperty("_ClipEdgeWidth", props);
            _ClipEdgeBlur = FindProperty("_ClipEdgeBlur", props);
            _ClipEdgeBlendMode = FindProperty("_ClipEdgeBlendMode", props);
            _ClipShapePositionX = FindProperty("_ClipShapePositionX", props);
            _ClipShapePositionY = FindProperty("_ClipShapePositionY", props);
            _ClipShapeScaleX = FindProperty("_ClipShapeScaleX", props);
            _ClipShapeScaleY = FindProperty("_ClipShapeScaleY", props);
            _ClipShapeRotation = FindProperty("_ClipShapeRotation", props);
            _ClipShapes = FindProperty("_ClipShapes", props);
            _Clip_N_Shape = FindProperty("_Clip_N_Shape", props);
            _Asteroid_Shape = FindProperty("_Asteroid_Shape", props);
            _Clip2 = FindProperty("_Clip2", props);
            _Clip3 = FindProperty("_Clip3", props);
            _Clip4 = FindProperty("_Clip4", props);
        }



        protected override void DrawCustomProperties(Material material)
        {
            // GUIStyles Name   Description
            // ---------------- ------------------------------------
            // boxOuter         outer box
            // boxInnerHalf     inner box
            // boxInner         inner box without label
            // customBox        box (similar to unity default box)
            // customToggleFont label for box

            isShowCustomProperties = Foldout("Clipping Properties", "Cipping Menu", isShowCustomProperties);

            var width = EditorGUIUtility.currentViewWidth;
            if(isShowCustomProperties)
            {
                Vector4 posXVector = _ClipShapePositionX.vectorValue;
                Vector4 posYVector = _ClipShapePositionY.vectorValue;
                Vector4 scaleXVector = _ClipShapeScaleX.vectorValue;
                Vector4 scaleYVector = _ClipShapeScaleY.vectorValue;
                Vector4 rotationVector = _ClipShapeRotation.vectorValue;
                Vector4 clipShapesVector = _ClipShapes.vectorValue;
                Vector4 NShapeVector = _Clip_N_Shape.vectorValue;
                Vector4 asteroidVector = _Asteroid_Shape.vectorValue;
                Vector4 edgeWidthVector = _ClipEdgeWidth.vectorValue;
                Vector4 edgeBlurVector = _ClipEdgeBlur.vectorValue;
                Vector4 edgeBlendModeVector = _ClipEdgeBlendMode.vectorValue;

                EditorGUILayout.BeginVertical(boxOuter);
                EditorGUILayout.LabelField(GetLoc("Cipping 1"), customToggleFont);
                EditorGUILayout.BeginVertical(boxInnerHalf);

                EditorGUI.BeginChangeCheck();

                DrawLine();
                clipShapesVector.x = EditorGUILayout.IntPopup("Clip Shapes", (int)clipShapesVector.x, new[] {"Circle", "Square", "Diamond", "Heart", "Asteroid", "Star"}, new[] {0, 1, 2, 3, 4, 5});

                EditorGUILayout.LabelField("Position X,Y");
                using (new EditorGUILayout.HorizontalScope())
                {
                    posXVector.x = EditorGUILayout.Slider("", posXVector.x, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    posYVector.x = EditorGUILayout.Slider("", posYVector.x, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                }
                EditorGUILayout.LabelField("Scale X,Y");
                using (new EditorGUILayout.HorizontalScope())
                {
                    scaleXVector.x = EditorGUILayout.Slider("", scaleXVector.x, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    scaleYVector.x = EditorGUILayout.Slider("", scaleYVector.x, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                }
                rotationVector.x = EditorGUILayout.Slider("Rotation", rotationVector.x, -1.0f, 1.0f);

                if (clipShapesVector.x == 4.0f)
                {
                    asteroidVector.x = EditorGUILayout.Slider("Asteroid Shape", asteroidVector.x, 0.0f, 1.0f);
                }
                if (clipShapesVector.x == 5.0f)
                {
                    NShapeVector.x = EditorGUILayout.IntSlider("N Shapes", (int)NShapeVector.x, 4, 10);
                }
                DrawLine();
                m_MaterialEditor.ShaderProperty(_ClipEdgeColor, "Edge Color");
                edgeWidthVector.x = EditorGUILayout.Slider("Edge Width", edgeWidthVector.x, 0.0f, 1.0f);
                edgeBlurVector.x = EditorGUILayout.Slider("Edge Blur", edgeBlurVector.x, 0.0f, 1.0f);
                edgeBlendModeVector.x = EditorGUILayout.IntPopup("Edge Blend Mode", (int)edgeBlendModeVector.x, new[] {"Normal", "Add", "Screen", "Multiply"}, new[] {0, 1, 2, 3});

                //m_MaterialEditor.ShaderProperty(customVariable, "Custom Variable");
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(boxOuter);
                using (new EditorGUILayout.HorizontalScope())
                {
                    _Clip2.floatValue = EditorGUILayout.Toggle(_Clip2.floatValue == 1.0f, GUILayout.Width(20)) ? 1.0f : 0.0f;
                    EditorGUILayout.LabelField("Clipping 2", customToggleFont);
                }
                if (_Clip2.floatValue == 1.0f)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    if (GUILayout.Button("Copy Clipping 1 Parameters"))
                    {
                        posXVector.y = posXVector.x;
                        posYVector.y = posYVector.x;
                        scaleXVector.y = scaleXVector.x;
                        scaleYVector.y = scaleYVector.x;
                        rotationVector.y = rotationVector.x;
                        clipShapesVector.y = clipShapesVector.x;
                        NShapeVector.y = NShapeVector.x;
                        asteroidVector.y = asteroidVector.x;
                        edgeWidthVector.y = edgeWidthVector.x;
                        edgeBlurVector.y = edgeBlurVector.x;
                        edgeBlendModeVector.y = edgeBlendModeVector.x;
                        _ClipEdgeColor2.colorValue = _ClipEdgeColor.colorValue;
                    }
                    DrawLine();
                    clipShapesVector.y = EditorGUILayout.IntPopup("Clip Shapes", (int)clipShapesVector.y, new[] {"Circle", "Square", "Diamond", "Heart", "Asteroid", "Star"}, new[] {0, 1, 2, 3, 4, 5});

                    EditorGUILayout.LabelField("Position X,Y");
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        posXVector.y = EditorGUILayout.Slider("", posXVector.y, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                        posYVector.y = EditorGUILayout.Slider("", posYVector.y, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    }
                    EditorGUILayout.LabelField("Scale X,Y");
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        scaleXVector.y = EditorGUILayout.Slider("", scaleXVector.y, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                        scaleYVector.y = EditorGUILayout.Slider("", scaleYVector.y, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    }
                    rotationVector.y = EditorGUILayout.Slider("Rotation", rotationVector.y, -1.0f, 1.0f);

                    if (clipShapesVector.y == 4.0f)
                    {
                        asteroidVector.y = EditorGUILayout.Slider("Asteroid Shape", asteroidVector.y, 0.0f, 1.0f);
                    }
                    if (clipShapesVector.y == 5.0f)
                    {
                        NShapeVector.y = EditorGUILayout.IntSlider("N Shapes", (int)NShapeVector.y, 4, 10);
                    }
                    DrawLine();
                    m_MaterialEditor.ShaderProperty(_ClipEdgeColor2, "Edge Color");
                    edgeWidthVector.y = EditorGUILayout.Slider("Edge Width", edgeWidthVector.y, 0.0f, 1.0f);
                    edgeBlurVector.y = EditorGUILayout.Slider("Edge Blur", edgeBlurVector.y, 0.0f, 1.0f);
                    edgeBlendModeVector.y = EditorGUILayout.IntPopup("Edge Blend Mode", (int)edgeBlendModeVector.y, new[] {"Normal", "Add", "Screen", "Multiply"}, new[] {0, 1, 2, 3});
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(boxOuter);
                using (new EditorGUILayout.HorizontalScope())
                {
                    _Clip3.floatValue = EditorGUILayout.Toggle(_Clip3.floatValue == 1.0f, GUILayout.Width(20)) ? 1.0f : 0.0f;
                    EditorGUILayout.LabelField("Clipping 3", customToggleFont);
                }
                if (_Clip3.floatValue == 1.0f)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    if (GUILayout.Button("Copy Clipping 1 Parameters"))
                    {
                        posXVector.z = posXVector.x;
                        posYVector.z = posYVector.x;
                        scaleXVector.z = scaleXVector.x;
                        scaleYVector.z = scaleYVector.x;
                        rotationVector.z = rotationVector.x;
                        clipShapesVector.z = clipShapesVector.x;
                        NShapeVector.z = NShapeVector.x;
                        asteroidVector.z = asteroidVector.x;
                        edgeWidthVector.z = edgeWidthVector.x;
                        edgeBlurVector.z = edgeBlurVector.x;
                        edgeBlendModeVector.z = edgeBlendModeVector.x;
                        _ClipEdgeColor3.colorValue = _ClipEdgeColor.colorValue;
                    }
                    if (GUILayout.Button("Copy Clipping 2 Parameters"))
                    {
                        posXVector.z = posXVector.y;
                        posYVector.z = posYVector.y;
                        scaleXVector.z = scaleXVector.y;
                        scaleYVector.z = scaleYVector.y;
                        rotationVector.z = rotationVector.y;
                        clipShapesVector.z = clipShapesVector.y;
                        NShapeVector.z = NShapeVector.y;
                        asteroidVector.z = asteroidVector.y;
                        edgeWidthVector.z = edgeWidthVector.y;
                        edgeBlurVector.z = edgeBlurVector.y;
                        edgeBlendModeVector.z = edgeBlendModeVector.y;
                        _ClipEdgeColor3.colorValue = _ClipEdgeColor2.colorValue;
                    }
                    DrawLine();
                    clipShapesVector.z = EditorGUILayout.IntPopup("Clip Shapes", (int)clipShapesVector.z, new[] {"Circle", "Square", "Diamond", "Heart", "Asteroid", "Star"}, new[] {0, 1, 2, 3, 4, 5});

                    EditorGUILayout.LabelField("Position X,Y");
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        posXVector.z = EditorGUILayout.Slider("", posXVector.z, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                        posYVector.z = EditorGUILayout.Slider("", posYVector.z, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    }
                    EditorGUILayout.LabelField("Scale X,Y");
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        scaleXVector.z = EditorGUILayout.Slider("", scaleXVector.z, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                        scaleYVector.z = EditorGUILayout.Slider("", scaleYVector.z, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    }
                    rotationVector.z = EditorGUILayout.Slider("Rotation", rotationVector.z, -1.0f, 1.0f);

                    if (clipShapesVector.z == 4.0f)
                    {
                        asteroidVector.z = EditorGUILayout.Slider("Asteroid Shape", asteroidVector.z, 0.0f, 1.0f);
                    }
                    if (clipShapesVector.z == 5.0f)
                    {
                        NShapeVector.z = EditorGUILayout.Slider("N Shapes", NShapeVector.z, 4, 10);
                    }
                    DrawLine();
                    m_MaterialEditor.ShaderProperty(_ClipEdgeColor3, "Edge Color");
                    edgeWidthVector.z = EditorGUILayout.Slider("Edge Width", edgeWidthVector.z, 0.0f, 1.0f);
                    edgeBlurVector.z = EditorGUILayout.Slider("Edge Blur", edgeBlurVector.z, 0.0f, 1.0f);
                    edgeBlendModeVector.z = EditorGUILayout.IntPopup("Edge Blend Mode", (int)edgeBlendModeVector.z, new[] {"Normal", "Add", "Screen", "Multiply"}, new[] {0, 1, 2, 3});
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(boxOuter);
                using (new EditorGUILayout.HorizontalScope())
                {
                    _Clip4.floatValue = EditorGUILayout.Toggle(_Clip4.floatValue == 1.0f, GUILayout.Width(20)) ? 1.0f : 0.0f;
                    EditorGUILayout.LabelField("Clipping 4", customToggleFont);
                }
                if (_Clip4.floatValue == 1.0f)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    if (GUILayout.Button("Copy Clipping 1 Parameters"))
                    {
                        posXVector.w = posXVector.x;
                        posYVector.w = posYVector.x;
                        scaleXVector.w = scaleXVector.x;
                        scaleYVector.w = scaleYVector.x;
                        rotationVector.w = rotationVector.x;
                        clipShapesVector.w = clipShapesVector.x;
                        NShapeVector.w = NShapeVector.x;
                        asteroidVector.w = asteroidVector.x;
                        edgeWidthVector.w = edgeWidthVector.x;
                        edgeBlurVector.w = edgeBlurVector.x;
                        edgeBlendModeVector.w = edgeBlendModeVector.x;
                        _ClipEdgeColor4.colorValue = _ClipEdgeColor.colorValue;
                    }
                    if (GUILayout.Button("Copy Clipping 2 Parameters"))
                    {
                        posXVector.w = posXVector.y;
                        posYVector.w = posYVector.y;
                        scaleXVector.w = scaleXVector.y;
                        scaleYVector.w = scaleYVector.y;
                        rotationVector.w = rotationVector.y;
                        clipShapesVector.w = clipShapesVector.y;
                        NShapeVector.w = NShapeVector.y;
                        asteroidVector.w = asteroidVector.y;
                        edgeWidthVector.w = edgeWidthVector.y;
                        edgeBlurVector.w = edgeBlurVector.y;
                        edgeBlendModeVector.w = edgeBlendModeVector.y;
                        _ClipEdgeColor4.colorValue = _ClipEdgeColor2.colorValue;
                    }
                    if (GUILayout.Button("Copy Clipping 3 Parameters"))
                    {
                        posXVector.w = posXVector.z;
                        posYVector.w = posYVector.z;
                        scaleXVector.w = scaleXVector.z;
                        scaleYVector.w = scaleYVector.z;
                        rotationVector.w = rotationVector.z;
                        clipShapesVector.w = clipShapesVector.z;
                        NShapeVector.w = NShapeVector.z;
                        asteroidVector.w = asteroidVector.z;
                        edgeWidthVector.w = edgeWidthVector.z;
                        edgeBlurVector.w = edgeBlurVector.z;
                        edgeBlendModeVector.w = edgeBlendModeVector.z;
                        _ClipEdgeColor4.colorValue = _ClipEdgeColor3.colorValue;
                    }
                    DrawLine();
                    clipShapesVector.w = EditorGUILayout.IntPopup("Clip Shapes", (int)clipShapesVector.w, new[] {"Circle", "Square", "Diamond", "Heart", "Asteroid", "Star"}, new[] {0, 1, 2, 3, 4, 5});

                    EditorGUILayout.LabelField("Position X,Y");
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        posXVector.w = EditorGUILayout.Slider("", posXVector.w, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                        posYVector.w = EditorGUILayout.Slider("", posYVector.w, -1.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    }
                    EditorGUILayout.LabelField("Scale X,Y");
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        scaleXVector.w = EditorGUILayout.Slider("", scaleXVector.w, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                        scaleYVector.w = EditorGUILayout.Slider("", scaleYVector.w, 0.0f, 1.0f, GUILayout.Width(width / 2 - 25));
                    }
                    rotationVector.w = EditorGUILayout.Slider("Rotation", rotationVector.w, -1.0f, 1.0f);

                    if (clipShapesVector.w == 4.0f)
                    {
                        asteroidVector.w = EditorGUILayout.Slider("Asteroid Shape", asteroidVector.w, 0.0f, 1.0f);
                    }
                    if (clipShapesVector.w == 5.0f)
                    {
                        NShapeVector.w = EditorGUILayout.IntSlider("N Shapes", (int)NShapeVector.w, 4, 10);
                    }
                    DrawLine();
                    m_MaterialEditor.ShaderProperty(_ClipEdgeColor4, "Edge Color");
                    edgeWidthVector.w = EditorGUILayout.Slider("Edge Width", edgeWidthVector.w, 0.0f, 1.0f);
                    edgeBlurVector.w = EditorGUILayout.Slider("Edge Blur", edgeBlurVector.w, 0.0f, 1.0f);
                    edgeBlendModeVector.w = EditorGUILayout.IntPopup("Edge Blend Mode", (int)edgeBlendModeVector.w, new[] {"Normal", "Add", "Screen", "Multiply"}, new[] {0, 1, 2, 3});
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.EndVertical();
                if (EditorGUI.EndChangeCheck())
                {
                    _ClipShapePositionX.vectorValue = posXVector;
                    _ClipShapePositionY.vectorValue = posYVector;
                    _ClipShapeScaleX.vectorValue = scaleXVector;
                    _ClipShapeScaleY.vectorValue = scaleYVector;
                    _ClipShapeRotation.vectorValue = rotationVector;
                    _ClipShapes.vectorValue = clipShapesVector;
                    _Clip_N_Shape.vectorValue = NShapeVector;
                    _Asteroid_Shape.vectorValue = asteroidVector;
                    _ClipEdgeWidth.vectorValue = edgeWidthVector;
                    _ClipEdgeBlur.vectorValue = edgeBlurVector;
                    _ClipEdgeBlendMode.vectorValue = edgeBlendModeVector;
                }
            }
        }

        protected override void ReplaceToCustomShaders()
        {
            lts         = Shader.Find(shaderName + "/lilToon");
            ltsc        = Shader.Find("Hidden/" + shaderName + "/Cutout");
            ltst        = Shader.Find("Hidden/" + shaderName + "/Transparent");
            ltsot       = Shader.Find("Hidden/" + shaderName + "/OnePassTransparent");
            ltstt       = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparent");

            ltso        = Shader.Find("Hidden/" + shaderName + "/OpaqueOutline");
            ltsco       = Shader.Find("Hidden/" + shaderName + "/CutoutOutline");
            ltsto       = Shader.Find("Hidden/" + shaderName + "/TransparentOutline");
            ltsoto      = Shader.Find("Hidden/" + shaderName + "/OnePassTransparentOutline");
            ltstto      = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparentOutline");

            ltsoo       = Shader.Find(shaderName + "/[Optional] OutlineOnly/Opaque");
            ltscoo      = Shader.Find(shaderName + "/[Optional] OutlineOnly/Cutout");
            ltstoo      = Shader.Find(shaderName + "/[Optional] OutlineOnly/Transparent");

            ltstess     = Shader.Find("Hidden/" + shaderName + "/Tessellation/Opaque");
            ltstessc    = Shader.Find("Hidden/" + shaderName + "/Tessellation/Cutout");
            ltstesst    = Shader.Find("Hidden/" + shaderName + "/Tessellation/Transparent");
            ltstessot   = Shader.Find("Hidden/" + shaderName + "/Tessellation/OnePassTransparent");
            ltstesstt   = Shader.Find("Hidden/" + shaderName + "/Tessellation/TwoPassTransparent");

            ltstesso    = Shader.Find("Hidden/" + shaderName + "/Tessellation/OpaqueOutline");
            ltstessco   = Shader.Find("Hidden/" + shaderName + "/Tessellation/CutoutOutline");
            ltstessto   = Shader.Find("Hidden/" + shaderName + "/Tessellation/TransparentOutline");
            ltstessoto  = Shader.Find("Hidden/" + shaderName + "/Tessellation/OnePassTransparentOutline");
            ltstesstto  = Shader.Find("Hidden/" + shaderName + "/Tessellation/TwoPassTransparentOutline");

            ltsl        = Shader.Find(shaderName + "/lilToonLite");
            ltslc       = Shader.Find("Hidden/" + shaderName + "/Lite/Cutout");
            ltslt       = Shader.Find("Hidden/" + shaderName + "/Lite/Transparent");
            ltslot      = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparent");
            ltsltt      = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparent");

            ltslo       = Shader.Find("Hidden/" + shaderName + "/Lite/OpaqueOutline");
            ltslco      = Shader.Find("Hidden/" + shaderName + "/Lite/CutoutOutline");
            ltslto      = Shader.Find("Hidden/" + shaderName + "/Lite/TransparentOutline");
            ltsloto     = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparentOutline");
            ltsltto     = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparentOutline");

            ltsref      = Shader.Find("Hidden/" + shaderName + "/Refraction");
            ltsrefb     = Shader.Find("Hidden/" + shaderName + "/RefractionBlur");
            ltsfur      = Shader.Find("Hidden/" + shaderName + "/Fur");
            ltsfurc     = Shader.Find("Hidden/" + shaderName + "/FurCutout");
            ltsfurtwo   = Shader.Find("Hidden/" + shaderName + "/FurTwoPass");
            ltsfuro     = Shader.Find(shaderName + "/[Optional] FurOnly/Transparent");
            ltsfuroc    = Shader.Find(shaderName + "/[Optional] FurOnly/Cutout");
            ltsfurotwo  = Shader.Find(shaderName + "/[Optional] FurOnly/TwoPass");
            ltsgem      = Shader.Find("Hidden/" + shaderName + "/Gem");
            ltsfs       = Shader.Find(shaderName + "/[Optional] FakeShadow");

            ltsover     = Shader.Find(shaderName + "/[Optional] Overlay");
            ltsoover    = Shader.Find(shaderName + "/[Optional] OverlayOnePass");
            ltslover    = Shader.Find(shaderName + "/[Optional] LiteOverlay");
            ltsloover   = Shader.Find(shaderName + "/[Optional] LiteOverlayOnePass");

            ltsm        = Shader.Find(shaderName + "/lilToonMulti");
            ltsmo       = Shader.Find("Hidden/" + shaderName + "/MultiOutline");
            ltsmref     = Shader.Find("Hidden/" + shaderName + "/MultiRefraction");
            ltsmfur     = Shader.Find("Hidden/" + shaderName + "/MultiFur");
            ltsmgem     = Shader.Find("Hidden/" + shaderName + "/MultiGem");
        }

        // You can create a menu like this
        /*
        [MenuItem("Assets/TemplateFull/Convert material to custom shader", false, 1100)]
        private static void ConvertMaterialToCustomShaderMenu()
        {
            if(Selection.objects.Length == 0) return;
            TemplateFullInspector inspector = new TemplateFullInspector();
            for(int i = 0; i < Selection.objects.Length; i++)
            {
                if(Selection.objects[i] is Material)
                {
                    inspector.ConvertMaterialToCustomShader((Material)Selection.objects[i]);
                }
            }
        }
        */
    }
}
#endif