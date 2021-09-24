using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace ZZBLib
{
    [Flags]
    public enum ViewType
    {
        None = 0b0000_0000,
        GlobalAction = 0b0000_0001,
        State = 0b0000_0010,
        StateSet = 0b0000_0100,
        Action = 0b0000_1000,
        Tool = 0b0001_0000,
        Other = 0b0010_0000,
        Frame = 0b0100_0000

    }

    /// <summary>
    /// 编辑器配置
    /// </summary>
    public partial class ActionEditorSetting
    {
        public int stateSelectIndex = -1;
        public int attackRangeSelectIndex = -1;
        public int bodyRangeSelectIndex = -1;
        public int actionSelectIndex = -1;
        public int globalActionSelectIndex = -1;
        public int frameSelectIndex = -1;
        public bool enableAllControl = false;
        public bool enableQuickKey = false;

        public ViewType viewType;

        public float frameRate => 0.033f;

        public Vector2 otherScrollViewPos = Vector2.zero;

        public float frameWidth = 40f;
        public float frameListViewRectHeight = 200f;
    }


    public class ActionEditorWindow : EditorWindow
    {
        [MenuItem("ZZBLib/动作编辑")]
        public static void ShowEditor()
        {
            EditorWindow.GetWindow<ActionEditorWindow>();

        }

        public static void ShowEditor(GameObject target, TextAsset textAsset)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Debug.LogWarning("编辑器不能在运行时打开");
                return;
            }

            var win = EditorWindow.GetWindow<ActionEditorWindow>();
            if (win.configAsset != null)
            {
                win.Focus();
                return;
            }

            win.UpdateTarget(target);
            win.UpdateConfig(config);

        }

        [NonSerialized] public readonly ActionListView actionListView;
        [NonSerialized] public readonly ActionSetView actionSetView;
        [NonSerialized] public readonly GlobalActionListView globalActionListView;
        [NonSerialized] public readonly GlobalActionSetView globalActionSetView;
        [NonSerialized] public readonly AttackRangeListView attackRangeListView;
        [NonSerialized] public readonly BodyRangeListView bodyRangeListView;
        [NonSerialized] public readonly FrameListView frameListView;
        [NonSerialized] public readonly StateListView stateListView;
        [NonSerialized] public readonly StateSetView stateSetView;
        [NonSerialized] public readonly MenuView menuView;
        [NonSerialized] public readonly ToolView toolView;

        public List<View> views { get; private set; }

        private readonly SceneGUIDrawer guiDrawer;
        private readonly QuickButtonHandler quickButtonHandler;

        #region style

        #endregion style

        #region data

        #region raw data

        public static string settingPath = "ZZBLib.ActionEditorWindow";
        public ActionEditorSetting setting = new ActionEditorSetting();
        public bool actionMachineDirty = false;

        public bool isRunning => EditorApplication.isPlaying;

        public string lastEditorTargetPath = null;
        public GameObject actionMachineObj = null;
        public TextAsset configAsset = null;


        #endregion

        public class ViewWindow : EditorWindow
        {
            protected View _view;
            protected ActionEditorWindow _win;
            protected string _viewTypeName;

            public View view
            {
                get
                {
                    if(_view != null && _win != null)
                    {
                        return _view;
                    }
                    Type viewType = Type.GetType(_viewTypeName, false);
                    if (viewType == null) { return null; }
                    // 判断是否有窗口实例
                    if (!HasOpenInstances<ActionEditorWindow>()) { return null; }

                    _win = GetWindow<ActionEditorWindow>();
                    _view = _win.views.Find(t => t.GetType() == viewType);
                    _view.popWindow = this;
                    return _view;
                }
                set
                {
                    _view = value;
                    _win = value.win;
                    _viewTypeName = value.GetType().FullName + "," + value.GetType().Assembly.FullName;
                    _view.popWindow = this;
                }
            }
            public static ViewWindow Show(View view, Rect rect)
            {
                var win = EditorWindow.CreateWindow<ViewWindow>(view.title);
                win.position = rect;
                win.view = view;
                win.Show();
                return win;
            }
            private void OnEnable()
            {
                autoRepaintOnSceneChange = true;
            }
            private void OnDisable()
            {
                
            }
            private void OnDestroy()
            {
                view?.OnPopDestroy();
            }
            private void OnGUI()
            {
                if (view == null)
                    return;
                Rect contentRect = new Rect(Vector2.zero, this.position.size);
                view.Draw(contentRect);

                Repaint();
            }
        }
    }

}
