using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class ConsoleGUI
    {
        private static bool _initGlobalStyle = false;

        public static GUIStyle HorizontalScrollbarStyle { private set; get; }
        public static GUIStyle HorizontalScrollbarThumbStyle { private set; get; }
        public static GUIStyle VerticalScrollbarStyle { private set; get; }
        public static GUIStyle VerticalScrollbarThumbStyle { private set; get; }
        public static GUIStyle XStyle { private set; get; }
        public static GUIStyle ToolbarStyle { private set; get; }
        public static GUIStyle ButtonStyle { private set; get; }
        public static GUIStyle TogleStyle1 { private set; get; }
        public static GUIStyle TogleStyle2 { private set; get; }
        public static GUIStyle TextFieldStyle { private set; get; }
        public static GUIStyle LableStyle { private set; get; }
        public static GUIStyle RichLabelStyle { private set; get; }
        public static int RichLabelFontSize { private set; get; }

        private static GUIStyle _CacheHorizontalScrollbarThumb;
        private static GUIStyle _CacheVerticalScrollbarThumb;

        internal static void InitGlobalStyle()
        {
            if(_initGlobalStyle == false)
            {
                _initGlobalStyle = true;

                float scale;
                if(Screen.height > Screen.width)
                {
                    scale = Screen.width / 1080f;
                }
                else
                {
                    scale = Screen.width / 1920f;
                }

                HorizontalScrollbarStyle = new GUIStyle(GUI.skin.horizontalScrollbar);
                HorizontalScrollbarStyle.fixedHeight = (int)(30 * scale);
                HorizontalScrollbarThumbStyle = new GUIStyle(GUI.skin.horizontalScrollbarThumb);
                HorizontalScrollbarThumbStyle.fixedHeight = (int)(30 * scale);

                VerticalScrollbarStyle = new GUIStyle(GUI.skin.verticalScrollbar);
                VerticalScrollbarStyle.fixedWidth = (int)(30 * scale);
                VerticalScrollbarThumbStyle = new GUIStyle(GUI.skin.verticalScrollbarThumb);
                VerticalScrollbarThumbStyle.fixedWidth = (int)(30 * scale);

                XStyle = new GUIStyle(GUI.skin.button);
                XStyle.fontSize = (int)(38 * scale);
                XStyle.fixedWidth = (int)(40 * scale);
                XStyle.fixedHeight = (int)(40 * scale);

                ToolbarStyle = new GUIStyle(GUI.skin.button);
                ToolbarStyle.fontSize = (int)(28 * scale);
                ToolbarStyle.fixedHeight = (int)(40 * scale);

                ButtonStyle = new GUIStyle(GUI.skin.button);
                ButtonStyle.fontSize = (int)(28 * scale);
                ButtonStyle.fixedHeight = (int)(40 * scale);

                TogleStyle1 = new GUIStyle(GUI.skin.button);
                TogleStyle1.fontSize = (int)(26 * scale);
                TogleStyle1.fixedHeight = (int)(35 * scale);

                TogleStyle2 = new GUIStyle(GUI.skin.box);
                TogleStyle2.fontSize = (int)(26 * scale);
                TogleStyle2.fixedHeight = (int)(35 * scale);

                TextFieldStyle = new GUIStyle(GUI.skin.textField);
                TextFieldStyle.fontSize = (int)(22 * scale);
                TextFieldStyle.fixedHeight = (int)(30 * scale);

                LableStyle = new GUIStyle(GUI.skin.label);
                LableStyle.fontSize = (int)(24 * scale);

                RichLabelStyle = GUIStyle.none;
                RichLabelStyle.richText = true;
                RichLabelFontSize = (int)(24 * scale);
            }
        }

        public static Vector2 BeginScrollView(Vector2 pos, float offset = 0f)
        {
            _CacheHorizontalScrollbarThumb = GUI.skin.horizontalScrollbarThumb;
            _CacheVerticalScrollbarThumb = GUI.skin.verticalScrollbarThumb;
            GUI.skin.horizontalScrollbarThumb = HorizontalScrollbarThumbStyle;
            GUI.skin.verticalScrollbarThumb = VerticalScrollbarThumbStyle;

            float scrollWidth = Screen.safeArea.width - VerticalScrollbarStyle.fixedWidth;
            float scrollHeight = Screen.safeArea.height - ToolbarStyle.fixedHeight * 2 - offset;
            return GUILayout.BeginScrollView(pos, HorizontalScrollbarStyle, VerticalScrollbarStyle, GUILayout.Width(scrollWidth), GUILayout.Height(scrollHeight));
        }

        public static void EndScrollView()
        {
            GUILayout.EndScrollView();

            GUI.skin.horizontalScrollbarThumb = _CacheHorizontalScrollbarThumb;
            GUI.skin.verticalScrollbarThumb = _CacheVerticalScrollbarThumb;
        }

        public static bool Toggle(string name, bool checkFlag)
        {
            GUIStyle style = checkFlag ? TogleStyle1 : TogleStyle2;
            if (GUILayout.Button(name, style))
            {
                checkFlag = !checkFlag;
            }
            return checkFlag;
        }

        public static void Label(string text)
        {
            GUILayout.Label($"<size={RichLabelFontSize}><color=white>{text}</color></size>", RichLabelStyle);

        }
        public static void RedLabel(string text)
        {
            GUILayout.Label($"<size={RichLabelFontSize}><color=red>{text}</color></size>", RichLabelStyle);

        }

        public static void YellowLabel(string text)
        {
            GUILayout.Label($"<size={RichLabelFontSize}><color=yellow>{text}</color></size>", RichLabelStyle);

        }
    }
}