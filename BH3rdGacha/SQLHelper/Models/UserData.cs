using SqlSugar;

namespace SQLHelper.Models
{
    /// <summary>
    /// 用户数据
    /// </summary>
    [SugarTable("UserData")]
    public class UserData
    {
        /// <summary>
        /// QQ隶属的群号
        /// </summary>
        public long fromgroup { get; set; }
        /// <summary>
        /// 用户QQ号
        /// </summary>
        public long qq { get; set; }
        /// <summary>
        /// 签到时间戳
        /// </summary>
        public long? timestamp { get; set; }
        /// <summary>
        /// 废弃字段
        /// </summary>
        public int? sign { get; set; }
        /// <summary>
        /// 拥有的水晶
        /// </summary>
        public int? diamond { get; set; }
        /// <summary>
        /// 总消费水晶
        /// </summary>
        public long? total_diamond { get; set; }
        /// <summary>
        /// 抽卡次数
        /// </summary>
        public int? gacha_count { get; set; }
        /// <summary>
        /// 签到次数
        /// </summary>
        public int? sign_count { get; set; }
        /// <summary>
        /// 出货次数
        /// </summary>
        public int? purple_count { get; set; }
    }
}