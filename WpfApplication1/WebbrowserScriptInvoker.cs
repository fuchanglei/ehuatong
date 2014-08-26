using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WebBrowserEXT;

namespace Code.WebbrowserInteropr
{
  public class WebbrowserScriptInvoker
    {
      private System.Windows.Forms.WebBrowser _webbrowser;
        public WebbrowserScriptInvoker(System.Windows.Forms.WebBrowser webbrowser)
        {
            this._webbrowser = webbrowser;
        }
       
        public Object InvokeScript(string funcName,params Object[] args ){
            return this._webbrowser.Document.InvokeScript(funcName, args);
        }
        private void Delay(int Millisecond) //延迟系统时间，但系统又能同时能执行其它任务；
        {

            DateTime current = DateTime.Now;

            while (current.AddMilliseconds(Millisecond) > DateTime.Now)
            {

                Application.DoEvents();//转让控制权            

            }

            return;

        }
       public bool WaitWebPageLoad()
        {

            int i = 0;

            string sUrl;

            while (true)
            {

                Delay(50);  //系统延迟50毫秒，够少了吧！             

                if (_webbrowser.ReadyState == WebBrowserReadyState.Complete) //先判断是否发生完成事件。
                {

                    if (!_webbrowser.IsBusy) //再判断是浏览器是否繁忙                  
                    {

                        i = i + 1;

                        if (i == 2)   //为什么 是2呢？因为每次加载frame完成时就会置IsBusy为false,未完成就就置IsBusy为false，你想一想，加载一次，然后再一次，再一次...... 最后一次.......
                        {

                            sUrl = _webbrowser.Url.ToString();

                            if (sUrl.Contains("res")) //这是判断没有网络的情况下                           
                            {

                                return false;

                            }

                            else
                            {

                                return true;

                            }

                        }

                        continue;

                    }

                    i = 0;

                }

            }

        }//当然你也可以加上超时的情况，那就让你自己解决了。
    }
}
