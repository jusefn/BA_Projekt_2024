using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf.interfaces
{
    public interface IContentProvider
    {
        string GetContent();
        void SetContent(string content);
    }
}
