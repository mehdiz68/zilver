using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace zilver.ViewModels
{
    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            logo = string.Empty;
        }

        #region Properties


        public string logo { get; set; }


        #endregion

    }
}