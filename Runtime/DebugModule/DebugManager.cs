using Arkham.Onigiri.Events;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.DebugModule
{
    [CreateAssetMenu(menuName = "Managers/DebugManager")]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout, DrawHeader = false)]
    public class DebugManager : ScriptableObject
    {

        [Title("VARIABLES")]
        public VariablePack[] allVariables;
        public GameEvent[] allEvents;

        [Title("SIZING")]
        public bool show = true;
        public bool updateInEditor = false;
        [MinValue(0)] public float margeLeft = 5;
        [MinValue(0)] public float margeTop = 5;
        public float width = 150;
        public float itemHeight = 18;
        public float itemSpacing = 5;


        [Serializable]
        public class VariablePack
        {
            public ChangeableVariable variable;
            [ShowIf("isFloat")]
            public Vector2 minMax = new Vector2(-100f, 100f);
            [ShowIf("isFloat")]
            public string floatPrecision = "0.00";
            [ShowIf("isInt"), MinValue(1)]
            public int increment = 1;
            [ShowIf("isString"), MinValue(1)]
            public int lines = 1;


            public float GetHeigh(float iH, float iS)
            {
                float y = 0;
                switch (variable)
                {
                    case IntVariable i:
                        y += iH * 2;
                        break;
                    case FloatVariable f:
                        y += iH * 2;
                        break;
                    case StringVariable s:
                        y += iH * (lines + 1);
                        break;
                    case BoolVariable b:
                        y += iH;
                        break;
                    default:
                        break;
                }

                return y + iS;
            }

            public float DrawGUI(float w, float iH, float iS, float y)
            {
                switch (variable)
                {
                    case IntVariable i:
                        GUI.Label(new Rect(0, y, w, iH), i.name);
                        if (GUI.Button(new Rect(w - iH, y, iH, iH), "!")) variable.OnChange();
                        i.SetValue(int.Parse(GUI.TextField(new Rect(0, y + iH, w * .5f, iH), i.Value.ToString())));
                        if (GUI.Button(new Rect(w * .5f, y + iH, w * .25f, iH), "-")) i.ApplyChange(-increment);
                        if (GUI.Button(new Rect(w * .75f, y + iH, w * .25f, iH), "+")) i.ApplyChange(increment);
                        break;
                    case FloatVariable f:
                        GUI.Label(new Rect(0, y, w, iH), f.Value.ToString(floatPrecision) + " - " + f.name);
                        if (GUI.Button(new Rect(w - iH, y, iH, iH), "!")) variable.OnChange();
                        f.SetValue(GUI.HorizontalSlider(new Rect(0, y + iH, w, iH), f.Value, minMax.x, minMax.y));
                        break;
                    case StringVariable s:
                        GUI.Label(new Rect(0, y, w, iH), s.name);
                        if (GUI.Button(new Rect(w - iH, y, iH, iH), "!")) variable.OnChange();
                        s.SetValue(GUI.TextArea(new Rect(0, y + iH, w, iH * lines), s.Value));
                        break;
                    case BoolVariable b:
                        b.SetValue(GUI.Toggle(new Rect(0, y, w - iH, iH), b.Value, b.name));
                        if (GUI.Button(new Rect(w - iH, y, iH, iH), "!")) variable.OnChange();
                        break;
                    default:
                        break;
                }
                y += GetHeigh(iH, iS);
                return y;
            }


            private bool isFloat() => variable?.GetType() == typeof(FloatVariable);
            private bool isInt() => variable?.GetType() == typeof(IntVariable);
            private bool isString() => variable?.GetType() == typeof(StringVariable);
        }

        [ButtonGroup("toggle")]
        public void ToggleView()
        {
            show = !show;
        }
        [ButtonGroup("toggle")]
        public void ToggleUpdateInEditor()
        {
            updateInEditor = !updateInEditor;
        }


        public void Log(string _value) => Debug.Log(_value);
        public void Log(StringVariable _value) => Debug.Log(_value.Value);
        public void Log(IntVariable _value) => Debug.Log(_value.Value);
        public void Log(FloatVariable _value) => Debug.Log(_value.Value);
        public void Log(Component _a) => Debug.Log(_a);
        public void Log(Component _a, Component _b) => Debug.Log(_a + " & " + _b);
    }
}
