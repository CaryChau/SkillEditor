using UnityEngine;

namespace ZZBLib
{
    public abstract class DataView : View
    {
        public abstract object CopyData();
        public abstract void PasteData(object data);

        protected override void OnHeaderDraw()
        {

        }
    }

}
