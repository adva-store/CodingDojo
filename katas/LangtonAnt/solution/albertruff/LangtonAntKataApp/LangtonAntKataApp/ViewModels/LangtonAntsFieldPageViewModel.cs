using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LangtonAntKataApp.ViewModels
{
    public  class LangtonAntsFieldPageViewModel : BaseViewModel, IQueryAttributable
    {
        private int moveSpeed;
        public int Field
        {
            set { SetProperty(ref moveSpeed, value); }
            get { return moveSpeed; }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            string name = HttpUtility.UrlDecode(query["name"].ToString());
       
        }
    }
}
