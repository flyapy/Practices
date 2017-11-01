using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practices.Serialization.Models
{
    [Serializable]
    public class LoginInfoVO
    {
        /// <summary>
        /// 插件ID
        /// </summary>
        public string PluginId { get; set; }

        /// <summary>
        /// 网点编号
        /// </summary>
        //[Newtonsoft.Json.JsonProperty(PropertyName = "compId")]
        public string CompId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 一半3Des密钥
        /// </summary>
        public string Half3DesPwd { get; set; }

        /// <summary>
        /// 是否强制登录
        /// </summary>
        public bool IsForceLogin { get; set; }

    }
}
