using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DM.Web.Controllers
{
    /// <summary>
    /// Controller基类
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 登录用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 登录用户租户ID
        /// </summary>
        public int ConsignorId { get; set; }

    }
}