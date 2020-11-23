using SqlSugar;

namespace SQLHelper.Models
{
    /// <summary>
    /// �û�����
    /// </summary>
    [SugarTable("UserData")]
    public class UserData
    {
        /// <summary>
        /// QQ������Ⱥ��
        /// </summary>
        public long fromgroup { get; set; }
        /// <summary>
        /// �û�QQ��
        /// </summary>
        public long qq { get; set; }
        /// <summary>
        /// ǩ��ʱ���
        /// </summary>
        public long? timestamp { get; set; }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public int? sign { get; set; }
        /// <summary>
        /// ӵ�е�ˮ��
        /// </summary>
        public int? diamond { get; set; }
        /// <summary>
        /// ������ˮ��
        /// </summary>
        public long? total_diamond { get; set; }
        /// <summary>
        /// �鿨����
        /// </summary>
        public int? gacha_count { get; set; }
        /// <summary>
        /// ǩ������
        /// </summary>
        public int? sign_count { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public int? purple_count { get; set; }
    }
}