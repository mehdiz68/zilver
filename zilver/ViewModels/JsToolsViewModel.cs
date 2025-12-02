using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace zilver.ViewModels
{
    public class JsToolsViewModel
    {
        public JsToolsViewModel()
        {

            WebmasterVerification = string.Empty;

            AnalyticsVerification = string.Empty;
        }
        #region Properties

        

        public string WebmasterVerification { get; set; }

        public string AnalyticsVerification { get; set; }

        #endregion

    }
}