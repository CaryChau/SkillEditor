using System;

namespace GameFramework.Console
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConsoleAttribute : Attribute
    {
        /// <summary>
        /// 标题名称
        /// </summary>
        public string Title;

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Order;

        public ConsoleAttribute(string title, int order)
        {
            Title = title;
            Order = order;
        }
    }
}
