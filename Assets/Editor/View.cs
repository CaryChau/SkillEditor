﻿using UnityEngine;
using UnityEditor;

namespace ZZBLib
{
    public abstract class View
    {
        public abstract string title { get; }
        public virtual bool useAre { get; } = true;
        public EditorWindow popWindow { get; set; }
        public bool isPop => popWindow != null;
        public Rect localRect { get; protected set; }
        public Rect rect => new Rect(win.position.position + localRect.position, localRect.size);

        public ActionEditorWindow win { get; set; }
        public virtual bool checkConfig { get; } = true;

        public void Draw(Rect rect)
        {
            this.localRect = rect;
            Rect contentRect = rect;

            if(!string.IsNullOrEmpty(title))
            {
                float titleHeight = 16f;
                Rect titleRect = new Rect(rect.x, rect.y, rect.width, titleHeight);
                contentRect = new Rect(rect.x, rect.y + titleHeight, rect.width, rect.height - titleHeight);

                GUILayout.BeginArea(titleRect);
                GUILayout.BeginHorizontal();
                GUILayout.Label(title, AEStyles.view_head);

                OnHeaderDraw();

                if(!isPop)
                {
                    if(GUILayout.Button("W", AEStyles.view_head, GUILayout.Width(20)))
                    {
                        GUI.FocusControl(null);
                        ShowPopWindow();
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.EndArea();
            }

            GUI.Box(contentRect, GUIContent.none, AEStyles.view_bg);

            if (!checkConfig)
            {

            }
        }

        protected abstract void OnGUI(Rect rect);
        public abstract void OnUpdate();

        protected virtual void OnHeaderDraw()
        {
            
        }

        public void ShowPopWindow()
        {
            if (popWindow != null)
            {
                popWindow.Focus();
                return;
            }
            ViewWindow.Show(this, rect);
        }
        public void HidePopWindow()
        {
            if (popWindow == null)
            {
                return;
            }
            popWindow.Close();
            popWindow = null;

        }

        public virtual void OnDestroy()
        {
            HidePopWindow();
        }

        public void OnPopDestroy()
        {
            popWindow = null;
        }

    }

}
