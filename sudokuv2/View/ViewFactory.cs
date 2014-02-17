using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuv2.View
{
    public static class ViewFactory
    {
        public static void Register(string key, Func<IView> func)
        {
            views.Add(key, func);
        }

        public static IView GetView(string key)
        {
            return views[key]();
        }

        private static Dictionary<string, Func<IView>> views = new Dictionary<string, Func<IView>>();
    }
}
