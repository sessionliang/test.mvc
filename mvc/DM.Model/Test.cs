using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Model
{
    public class Test
    {
        /// <summary>
        /// ID
        /// </summary>
        [DisplayName("ID")]
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名是必须的")]
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [DisplayName("价格")]
        [Range(0, 99999, ErrorMessage = "价格只能在0-99999之间")]
        public decimal Price { get; set; }
    }
}
