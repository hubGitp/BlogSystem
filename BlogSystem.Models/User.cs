using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Models
{

    public class User:BaseEntity
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required,StringLength(maximumLength:40),Column(TypeName ="varchar")]
        public string Email { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required, StringLength(maximumLength: 30), Column(TypeName = "varchar")]
        public string PassWord { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        [Required, StringLength(maximumLength: 300), Column(TypeName = "varchar")]
        public string ImagePath { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FunsCount { get; set; }
        /// <summary>
        /// 关注数
        /// </summary>
        public int FocusCount { get; set; }
        /// <summary>
        /// 网站名
        /// </summary>
        public string SiteName { get; set; }

    }
}
