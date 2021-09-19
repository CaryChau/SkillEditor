using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

namespace ZZBLib
{
    [Serializable]
    public class ActionListView : DataView
    {
        public override string title => "动作列表";

        public override bool useAre => true;

        private Vector2 scrollPos;


        public override object CopyData()
        {
            throw new NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new NotImplementedException();
        }

        public override void PasteData(object data)
        {
            throw new NotImplementedException();
        }

        protected override void OnGUI(Rect rect)
        {
            List<object> configs;
        }
    }

}
