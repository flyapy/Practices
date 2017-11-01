using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Practices.HttpService.ViewModels
{
    class MainWindowViewModel: Prism.Mvvm.BindableBase
    {
        private string rawText;

        public string RawText
        {
            get { return rawText; }
            set
            {
                rawText = value;
                RaisePropertyChanged(nameof(RawText));
            }
        }

        public Prism.Commands.DelegateCommand UrlEncodeCommand { get; set; }

        public MainWindowViewModel()
        {
            UrlEncodeCommand = new Prism.Commands.DelegateCommand(() => 
            {
                string encodedResult = UrlEncode(RawText);
                System.Windows.MessageBox.Show(encodedResult);
            });

            rawText = "/=";
        }

        private string UrlEncode(string str)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in str)
            {
                if (HttpUtility.UrlEncode(c.ToString()).Length > 1)
                {
                    builder.Append(HttpUtility.UrlEncode(c.ToString()).ToUpper());
                }
                else
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
        }

    }
}
