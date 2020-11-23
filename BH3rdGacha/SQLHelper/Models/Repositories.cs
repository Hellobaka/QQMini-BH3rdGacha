using SqlSugar;

namespace SQLHelper.Models
{
    /// <summary>
    /// 保存用户仓库数据的表
    /// </summary>
    public class Repositories
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
        /// 项目类型（武器、圣痕等）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 角色阶级
        /// </summary>
        public string class_ { get; set; }
        /// <summary>
        /// 项目登记
        /// </summary>
        public long? level { get; set; }
        /// <summary>
        /// 项目价值
        /// </summary>
        public long? value { get; set; }
        /// <summary>
        /// 紫色或者是蓝色
        /// </summary>
        public long? quality { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public long? count { get; set; }
        /// <summary>
        /// 获得日期
        /// </summary>
        public string date { get; set; }
    }
}